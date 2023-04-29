using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Changes the hue of an image
/// </summary>
public class HueChanger : MonoBehaviour
{
    [SerializeField]
    private float targetHue;
    [SerializeField]
    private float changeDuration;
    private float initialHue = 0;
    [SerializeField]
    private Image image;
    private int aimedAtAmount = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incrementAimedAtAmount() { aimedAtAmount++;}
    public void decrementAimedAtAmount() { aimedAtAmount--;}
}
