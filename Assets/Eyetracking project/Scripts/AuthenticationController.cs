using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the authentication of this user.
/// </summary>
public class AuthenticationController : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField, Tooltip("The overlay manager")]
    private OverlayManager overlayManager;

    [Header("Error messages and login details")]

    [SerializeField, Tooltip("The login details of the user")]
    private LoginDetails loginDetails;

    [SerializeField, Tooltip("The authentication dialogs")]
    private List<AuthenticationDialog> authDialogs;

    [Space(5),Header("The requests")]
    [SerializeField, Tooltip("The authentication request")]
    private AuthenticationRequest authenticationRequest;

    [SerializeField, Tooltip("The user server request")]
    private UserServerRequest userServerRequest;

    [Header("Debug fields")]
    [SerializeField, Tooltip("the token")]
    private JwtToken token;

    ///<inheritdoc>/>
    private void Start()
    {
        if (loginDetails.GetUsername() != "" && loginDetails.GetPassword() != "")
        {
            StartCoroutine(LoginToUser());
        }
        else {
            Debug.Log("The username and password needs to be filled out");
        }
    }

    /// <summary>
    /// Logs into the user and syncs the simulation setup.
    /// </summary>
    /// <returns>the enumerator</returns>
    private IEnumerator LoginToUser(){
        yield return authenticationRequest.SendLoginRequest(loginDetails);
        token = authenticationRequest.GetToken();
        if (token.GetToken() != "")
        {
            yield return userServerRequest.SendCurrentData(token.GetToken());
            GetComponent<SessionController>().GetSession().SetUser(userServerRequest.GetUser());
            yield return new WaitForSeconds(3);
            GetComponent<SimulationSetupController>().SendSimulationSetup();
        }
        else {
            Dialog dialog = authDialogs.Find(dialog => dialog.GetErrorCode() == authenticationRequest.GetHttpStatus());
            overlayManager.ShowDialog(dialog);
        }
    }

    /// <summary>
    /// Gets the token as string
    /// </summary>
    /// <returns>the token</returns>
    public string GetToken() {
        return token.GetToken();
    }

}
