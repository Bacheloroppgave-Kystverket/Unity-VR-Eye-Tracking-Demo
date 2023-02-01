using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Oculus.Installer.Editor.Utils;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.UI;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Oculus.Installer.Editor
{
    public class OculusIntegrationInstallerWindow : EditorWindow
    {
        private enum ProgressStep : short
        {
            Idle = 0,
            ImportingRegistry = 1,
            ReloadingUpm = 2,
            SearchingPkg = 3,
            Finished = 4,
        }

        private const string LicenseURL = "https://developer.oculus.com/licenses/oculussdk";

        private const string SamplesURL = "https://github.com/oculus-samples";

        private const string FeedbackURL =
            "https://forums.oculusvr.com/t5/SDK-Feedback/bd-p/dev-sdk-tool-feedback";

        private static readonly Vector2 s_windowSize = new Vector2(450, 140);

        private static readonly StyleColor s_linkColor =
            new StyleColor(new Color32(29, 101, 193, 255));

        private static readonly StyleColor s_linkHoverColor =
            new StyleColor(new Color32(44, 132, 193, 255));

        private Button _installButton;
        private ProgressBar _progressBar;
        private Toggle _licenseCheckbox;
        private OculusIntegrationRegistryInstaller.StatusCode _backingStatus;
        private Button _licenseLink;
        private ProgressStep _step;
        private float _progress;

        private ProgressStep Step
        {
            get => _step;
            set
            {
                _step = value;
                _progress = (float)Step / Enum.GetValues(typeof(ProgressStep)).Cast<short>().Max();
                UpdateGUI();
            }
        }

        // Add a toolbar menu item
        [MenuItem("Oculus/Install Oculus Integration")]
        public static void ShowWindow()
        {
            var window =
                GetWindow<OculusIntegrationInstallerWindow>(
                    title: "Oculus Integration",
                    focus: true);
            window.minSize = s_windowSize;
            window.maxSize = s_windowSize;
            window.Show();
        }

        private void CreateGUI()
        {
            var container = new VisualElement
            {
                style =
                {
                    paddingLeft = 8,
                    paddingTop = 8,
                    paddingRight = 8,
                    paddingBottom = 8,
                    flexDirection = FlexDirection.Column,
                    justifyContent = Justify.FlexStart,
                    alignItems = Align.Stretch,
                    flexGrow = 1f,
                }
            };

            var logoImage = new Image
            {
                image = Resources.Load<Texture>("MetaLogo"),
                style =
                {
                    width = 128, height = 32, marginBottom = 4, alignSelf = Align.FlexStart
                },
            };
            container.Add(logoImage);

            // License agreement checkbox
            var licenseContainer = new VisualElement
            {
                style =
                {
                    marginTop = 4,
                    marginBottom = 4,
                    flexDirection = FlexDirection.Row,
                    alignItems = Align.Center,
                    justifyContent = Justify.FlexStart
                }
            };
            _licenseCheckbox = new Toggle
            {
                style =
                {
                    width = 24
                },
                text = null,
            };
            licenseContainer.Add(_licenseCheckbox);
            _licenseCheckbox.RegisterValueChangedCallback(evt => UpdateGUI());
            var textElem = new TextElement
            {
                text = "I have read and agree to the terms of the"
            };
            licenseContainer.Add(textElem);
            _licenseLink = CreateHyperlinkButton("Oculus SDK license", LicenseURL);
            licenseContainer.Add(_licenseLink);
            container.Add(licenseContainer);

            _installButton = new Button(OnInstallButtonClick)
            {
                text = "Add Oculus Integration to Package Manager",
                style =
                {
                    paddingLeft = 8,
                    paddingTop = 8,
                    paddingRight = 8,
                    paddingBottom = 8,
                    marginTop = 4,
                    fontSize = 14,
                    display = DisplayStyle.Flex,
                }
            };
            container.Add(_installButton);

            _progressBar = new ProgressBar
            {
                title = "Importing...",
                value = _progress,
                style =
                {
                    paddingLeft = 8,
                    paddingTop = 8,
                    paddingRight = 8,
                    paddingBottom = 8,
                    marginTop = 4,
                    fontSize = 14,
                    display = DisplayStyle.None
                }
            };
            container.Add(_progressBar);

            var footer = new VisualElement
            {
                style =
                {
                    height = 20,
                    marginTop = 4,
                    flexDirection = FlexDirection.Row,
                    alignContent = Align.Stretch,
                }
            };
            var footerLeft = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row
                }
            };
            var footerRight = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row, marginLeft = StyleKeyword.Auto,
                }
            };
            footer.Add(footerLeft);
            footer.Add(footerRight);

            var versionText = new TextElement
            {
                text = "Experimental",
                style =
                {
                    color = new Color(0.5f, 0.5f, 0.5f), height = 20,
                }
            };
            footerLeft.Add(versionText);

            var samplesLink = CreateHyperlinkButton("Samples", SamplesURL);
            samplesLink.style.height = 20;
            footerRight.Add(samplesLink);

            var delimiter = new TextElement
            {
                text = "⸱",
                style =
                {
                    width = 12, height = 20, unityTextAlign = TextAnchor.MiddleCenter,
                }
            };
            footerRight.Add(delimiter);

            var feedbackLink = CreateHyperlinkButton("Feedback", FeedbackURL);
            feedbackLink.style.height = 20;
            footerRight.Add(feedbackLink);

            container.Add(footer);
            rootVisualElement.Add(container);

            UpdateGUI();
        }

        private void OnGUI()
        {
            // Poll for installer status changes
            if (_backingStatus != OculusIntegrationRegistryInstaller.Status)
            {
                if (OculusIntegrationRegistryInstaller.IsRunning)
                {
                    Step = ProgressStep.ImportingRegistry;
                }
                if (OculusIntegrationRegistryInstaller.IsError)
                {
                    PromptError(OculusIntegrationRegistryInstaller.StatusMessage);
                }
                if (OculusIntegrationRegistryInstaller.IsSuccess)
                {
                    OnInstallSuccess().ContinueWith(
                        _ => Step = ProgressStep.Idle,
                        TaskScheduler.FromCurrentSynchronizationContext());
                }
                _backingStatus = OculusIntegrationRegistryInstaller.Status;
            }
        }

        private void UpdateGUI()
        {
            if (rootVisualElement == null)
            {
                return;
            }

            _licenseCheckbox.SetEnabled(
                !OculusIntegrationRegistryInstaller.IsRunning &&
                Step == ProgressStep.Idle);

            _installButton.SetEnabled(
                _licenseCheckbox is { value: true } &&
                !OculusIntegrationRegistryInstaller.IsRunning &&
                Step == ProgressStep.Idle);

            if (Step == ProgressStep.Idle)
            {
                // Off
                _progressBar.value = 0f;
                _progressBar.style.display = DisplayStyle.None;
                _installButton.style.display = DisplayStyle.Flex;
            }
            else
            {
                // On
                _progressBar.style.display = DisplayStyle.Flex;

                // ProgressBar value is by default based on [0-100]
                _progressBar.value = Mathf.Clamp01(_progress) * 100;
                _installButton.style.display = DisplayStyle.None;
            }
        }

        private async Task OnInstallSuccess()
        {
            // Force a resolve of Package Manager. If the registries were successfully installed, Unity will show a
            // corresponding prompt and bring up package registry settings.
            Step = ProgressStep.ReloadingUpm;
            Client.Resolve();

            // Load all packages and find ones belonging to the imported registry
            Step = ProgressStep.SearchingPkg;
            var searchRequest = Client.SearchAll();
            await AsyncUtils.WaitUntilAsync(
                CancellationToken.None,
                () => searchRequest.IsCompleted);
            Step = ProgressStep.Finished;

            if (searchRequest.Status == StatusCode.Failure)
            {
                PromptError(
                    "Package manager failed to search for packages. Please check your internet connection.");
                return;
            }

            if (searchRequest.Result == null || searchRequest.Result.Length == 0)
            {
                PromptError(
                    "No package found in package manager. Please check your Unity installation.");
                return;
            }

            var installerRegistryUrl = new Uri(OculusIntegrationRegistryInstaller.ServerUrl);
            var foundPackages =
                (from p in searchRequest.Result
                    let packageRegistryUrl = new Uri(p.registry.url)
                    where packageRegistryUrl.Equals(
                        installerRegistryUrl)
                    select p)
                .ToList();

            if (foundPackages.Count == 0)
            {
                PromptError(
                    $"No package found in registry. Please verify that the following is reachable:\n\t{OculusIntegrationRegistryInstaller.ServerUrl}");
                return;
            }

            string foundPackagesStr = foundPackages.Aggregate(
                "",
                (current, p) => current + "\t- " + p.name + "\n");

            // If package scope found: prompt to open the Package Manager window
            bool openPackageManager = EditorUtility.DisplayDialog(
                "Success",
                "Successfully imported Oculus Integration Package Registry. Please install needed packages in Package Manager.\nPackages available: \n\n" +
                foundPackagesStr,
                "Open Package Manager",
                "Dismiss");
            if (openPackageManager)
            {
                // Invoke a search request to make sure UPM has our package ready for display
                var request = Client.Search(foundPackages[0].name);
                await AsyncUtils.WaitUntilAsync(
                    CancellationToken.None,
                    () => request.IsCompleted);

                if (request.Status == StatusCode.Success && request.Result.Length > 0)
                {
                    Window.Open(request.Result[0].name);
                }
                else
                {
                    Debug.LogWarning(
                        "Unable to open package in Package Manager. Please locate it in \"Package Manager\" -> \"Packages: My Registries\"");
                }
            }
        }

        private static void OnInstallButtonClick()
        {
            OculusIntegrationRegistryInstaller.StartInstallation();
        }

        private static void PromptError(string message)
        {
            EditorUtility.DisplayDialog(
                "Failed to Import Oculus Integration SDKs",
                message,
                "Dismiss");
            Debug.LogError(
                "Failed to import Oculus Integration SDKs: " + message);
        }

        private static Button CreateHyperlinkButton(string label, string url)
        {
            var button = new Button(() => Application.OpenURL(url))
            {
                text = label,
                style =
                {
                    color = s_linkColor,
                    backgroundImage = null,
                    backgroundColor = Color.clear,
                    borderTopColor = Color.clear,
                    borderRightColor = Color.clear,
                    borderBottomColor = Color.clear,
                    borderLeftColor = Color.clear,
                    paddingLeft = 0,
                    paddingRight = 0
                },
                tooltip = url
            };
            button.RegisterCallback((MouseOverEvent _) =>
            {
                button.style.color = s_linkHoverColor;
            });
            button.RegisterCallback((MouseOutEvent _) =>
            {
                button.style.color = s_linkColor;
            });
            return button;
        }
    }
}
