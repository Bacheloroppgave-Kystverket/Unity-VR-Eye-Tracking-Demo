using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LoginDetails
{
    [SerializeField, Tooltip("The username")]
    private string username;

    [SerializeField, Tooltip("The password")]
    private string password;
}
