using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetGUINameToObjectNameOnStart : MonoBehaviour
{
    private GameObject parent;
    [SerializeField]
    [Tooltip("The textcomponent which will change its text to the name of the gameobject. If blank, the component will try finding text in the child objects")]
    private TextMeshProUGUI textComponent;
    private string parentName;


    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject;
        parentName = parent.name;
        if (textComponent == null)
        {
            GetComponentInChildren<TextMeshProUGUI>().SetText(parentName);

        }
        else
        {
            textComponent.SetText(parentName);
        }
    }
}