// Author(s): Paul Calande
// Changes text based on tuning.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextFromTuning : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text to change.")]
    Text textToChange;
    [SerializeField]
    [Tooltip("Whether this button is the start button or not.")]
    bool isStartButton;

    private void Start()
    {
        string newText;
        if (isStartButton)
        {
            newText = Tuning.textBtnMenuStart;
        }
        else
        {
            newText = Tuning.textBtnMenuTuning;
        }
        textToChange.text = newText;
    }
}