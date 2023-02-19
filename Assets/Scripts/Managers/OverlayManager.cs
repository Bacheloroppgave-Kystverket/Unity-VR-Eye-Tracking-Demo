using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class OverlayManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI feedbackText;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayFeedback(Feedback feedback) {
        feedbackText.text = feedback.GetFeedbackAsString();
        
    }
}
