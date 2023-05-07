using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialog
{
    [SerializeField, Tooltip("The title of the dialog.")]
    private string title;

    [SerializeField, Tooltip("The message of the error"), TextArea]
    private string message;

    /// <summary>
    /// Gets the title.
    /// </summary>
    /// <returns>the title</returns>
    public string GetTitle() => title;

    /// <summary>
    /// Gets the message.
    /// </summary>
    /// <returns>the message</returns>
    public string GetMessage() => message;

}
