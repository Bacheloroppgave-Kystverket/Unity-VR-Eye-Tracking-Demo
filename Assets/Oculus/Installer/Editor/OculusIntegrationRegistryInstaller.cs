using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Oculus.Installer.ThirdParty.Semver;
using Oculus.Installer.ThirdParty.TinyJson;
using UnityEditor;
using UnityEngine;

namespace Oculus.Installer.Editor
{
    /// <summary>
    /// This class manages the installation of Oculus Integration registries to Unity Package Manager.
    /// </summary>
    public static class OculusIntegrationRegistryInstaller
    {
        public static readonly SemVersion UnityVersionMinimum = new SemVersion(2020, 3, 36);
        public static readonly SemVersion UnityVersionCurrent;

        /// <summary>
        /// The path of the manifest.json file in the current Unity Project.
        /// </summary>
        public static readonly string ManifestPath;

        /// <summary>
        /// The URL of the package registry server.
        /// </summary>
        public const string ServerUrl = "https://npm.developer.oculus.com";

        /// <summary>
        /// The registry scopes that should be included.
        /// </summary>
        public static readonly string[] ScopesToInstall =
        {
            "com.meta.xr.sdk",
        };

        /// <summary>
        /// A collection of old or incorrectly formatted urls that should be upgraded/modified to the newest and
        /// current server url.
        /// </summary>
        public static readonly string[] DeprecatedUrls =
            { };

        private const string RegistryNameKey = "name";

        private const string RegistryName = "Meta XR";

        private const string RegistryURLKey = "url";

        private const string ScopedRegistriesKey = "scopedRegistries";

        private const string RegistryScopesKey = "scopes";

        private const int RunningMinValue = 1000;

        private const int ErrorMinValue = 2000;

        public enum StatusCode
        {
            Idle = 0,
            Success = 1,

            Running = RunningMinValue,
            RunningWaitingForCompile,
            RunningAddingScopedRegistryToManifest,

            ErrorUnityVersionTooOld = ErrorMinValue,
            ErrorCouldNotCheckOutManifest,
            ErrorManifestIsReadOnly,
            ErrorUnexpectedException,
            ErrorInstallationCancelled
        }

        static OculusIntegrationRegistryInstaller()
        {
            // Retrieve the current Unity version
            var match = new Regex(@"(\d+)\.(\d+)\.(\d+)").Match(Application.unityVersion);
            if (!match.Success || match.Groups.Count != 4) return;

            UnityVersionCurrent = new SemVersion(int.Parse(match.Groups[1].Value),
                int.Parse(match.Groups[2].Value),
                int.Parse(match.Groups[3].Value));

            // Store the path to the package manifest file
            ManifestPath = Path.Combine(Directory.GetParent(Application.dataPath)!.FullName,
                "Packages",
                "manifest.json");
        }

        /// <summary>
        /// Returns the current status of the installation process, this can either be
        ///  - Idle:      When the process has not yet been started.  Represents neither error nor success
        ///  - Success:   When the process has completed and encountered no errors.
        ///  - Error_X:   When the process encountered an error of some kind during install
        ///  - Running_X: When the process is currently underway but has not yet completed
        /// </summary>
        public static StatusCode Status { get; private set; }

        /// <summary>
        /// Returns whether or not the process is in the Idle state
        /// </summary>
        public static bool IsIdle => Status == StatusCode.Idle;

        /// <summary>
        /// Returns whether or not the process is in the Success state
        /// </summary>
        public static bool IsSuccess => Status == StatusCode.Success;

        /// <summary>
        /// Returns whether or not the process is currently in any Running_X state
        /// </summary>
        public static bool IsRunning => !IsError && (int)Status >= RunningMinValue;

        /// <summary>
        /// Returns whether or not the process is currently in any Error_X state
        /// </summary>
        public static bool IsError => (int)Status >= ErrorMinValue;

        /// <summary>
        /// Returns a human-readable message that represents the current process state.
        /// </summary>
        public static string StatusMessage
        {
            get
            {
                return Status switch
                {
                    StatusCode.Idle => "Idle",
                    StatusCode.Success => "Everything is working normally!",
                    StatusCode.Running => "Performing install...",
                    StatusCode.RunningWaitingForCompile => "Waiting for compiling to finish...",
                    StatusCode.RunningAddingScopedRegistryToManifest =>
                        "Adding the scoped registry to the package manifest file...",
                    StatusCode.ErrorUnityVersionTooOld =>
                        $"The current version of Unity is too old.  The minimum version of Unity is {UnityVersionMinimum}",
                    StatusCode.ErrorCouldNotCheckOutManifest =>
                        "The manifest.json file could not be checked out from source control.  Make sure you have the rights to check out this file before proceeding.",
                    StatusCode.ErrorManifestIsReadOnly =>
                        "The manifest.json file is read-only.  If you are using Perforce source control make sure that the file is checked out and can be modified.",
                    StatusCode.ErrorUnexpectedException =>
                        "An unexpected error was encountered, check the console for more information and please submit a bug report.",
                    StatusCode.ErrorInstallationCancelled => "Installation was canceled.",
                    _ => "Unknown status code."
                };
            }
        }

        /// <summary>
        /// Returns whether or not the manifest.json file of the current Unity Project is
        /// currently in a read-only state.  The file needs to be modified by the installation
        /// process and so a read-only file will trigger an error state.
        /// </summary>
        private static bool IsManifestReadOnly => new FileInfo(ManifestPath).IsReadOnly;

        private static CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Returns whether or not the current Unity version is supported
        /// by protokit in general.  Any version that is older than 2019.3
        /// is currently not supported.
        /// </summary>
        private static bool IsUnityVersionTooOld => UnityVersionCurrent < UnityVersionMinimum;

        /// <summary>
        /// If the install process is currently running calling this method will cancel it, otherwise
        /// calling it has no effect.  A canceled process will enter an error state.
        /// </summary>
        public static void CancelInstallation()
        {
            if (IsRunning)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        public static void StartInstallation()
        {
            if (IsRunning)
            {
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                StartInstallationInternal(_cancellationTokenSource.Token);
            }
            catch (InstallationException e)
            {
                Status = e.ErrorCode;
            }
            catch (OperationCanceledException)
            {
                Status = StatusCode.ErrorInstallationCancelled;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                Status = StatusCode.ErrorUnexpectedException;
            }
        }

        private static void StartInstallationInternal(CancellationToken cancellation)
        {
            Status = StatusCode.Running;

            if (IsUnityVersionTooOld)
            {
                throw Err(StatusCode.ErrorUnityVersionTooOld);
            }

            if (EditorApplication.isCompiling)
            {
                // No need to spin-wait for compiling to finish.  After compiling finishes our process
                // will stop anyway due to the domain reload.
                Status = StatusCode.RunningWaitingForCompile;
                return;
            }

            cancellation.ThrowIfCancellationRequested();

            // Already present - skipping
            if (IsRegistryWithPresentWithURL(ServerUrl))
            {
                Status = StatusCode.Success;
                return;
            }

            if (!AssetDatabase.MakeEditable(ManifestPath))
            {
                throw Err(StatusCode.ErrorCouldNotCheckOutManifest);
            }

            if (IsManifestReadOnly)
            {
                throw Err(StatusCode.ErrorManifestIsReadOnly);
            }

            cancellation.ThrowIfCancellationRequested();

            // First try upgrading existing urls to the current url
            foreach (var upgradableUrl in DeprecatedUrls)
            {
                UpgradeRegistry(upgradableUrl, ServerUrl);
            }

            cancellation.ThrowIfCancellationRequested();

            // If, despite attempts to upgrade, we still don't have the server url
            // install it directly with default scopes
            if (!IsRegistryWithPresentWithURL(ServerUrl))
            {
                Status = StatusCode.RunningAddingScopedRegistryToManifest;
                InstallRegistry(ServerUrl, ScopesToInstall);
            }

            Status = StatusCode.Success;
        }

        private static void UpgradeRegistry(string oldUrl, string newUrl)
        {
            var json = File.ReadAllText(ManifestPath).FromJson<Dictionary<string, object>>();
            if (!json.TryGetValue(ScopedRegistriesKey, out object element))
            {
                return;
            }

            if (!(element is List<object> registries))
            {
                return;
            }

            foreach (var registry in registries.OfType<Dictionary<string, object>>())
            {
                if (registry.TryGetValue(RegistryURLKey, out object rawUrl) &&
                    rawUrl is string url &&
                    url == oldUrl)
                {
                    registry[RegistryURLKey] = newUrl;
                }
            }

            File.WriteAllText(ManifestPath, json.ToString());
        }

        private static void InstallRegistry(string registryUrl, IList<string> scopesToInstall)
        {
            var json = File.ReadAllText(ManifestPath).FromJson<Dictionary<string, object>>();
            if (!json.TryGetValue(ScopedRegistriesKey, out object element))
            {
                element = new List<object>();
                json.Add(ScopedRegistriesKey, element);
            }

            var registry = new Dictionary<string, object>
            {
                [RegistryNameKey] = RegistryName,
                [RegistryURLKey] = registryUrl,
                [RegistryScopesKey] = scopesToInstall
            };

            if (element is List<object> registries)
            {
                registries.Add(registry);
            }

            File.WriteAllText(ManifestPath, json.ToJson());
        }

        /// <summary>
        /// Returns true if the current project manifest.json contains any scoped registry
        /// with the provided url.
        /// </summary>
        private static bool IsRegistryWithPresentWithURL(string targetUrl)
        {
            var json = File.ReadAllText(ManifestPath).FromJson<Dictionary<string, object>>();

            if (!json.TryGetValue(ScopedRegistriesKey, out object element))
            {
                return false;
            }

            if (!(element is List<object> registries))
            {
                return false;
            }

            foreach (var registry in registries.OfType<Dictionary<string, object>>())
            {
                if (registry.TryGetValue(RegistryURLKey, out object rawUrl) &&
                    rawUrl is string url &&
                    url == targetUrl)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Small helper for constructing a new exception from a status code
        /// </summary>
        private static Exception Err(StatusCode errorCode)
        {
            return new InstallationException(errorCode);
        }

        private class InstallationException : Exception
        {
            public readonly StatusCode ErrorCode;

            public InstallationException(StatusCode errorCode)
            {
                ErrorCode = errorCode;
            }
        }
    }
}
