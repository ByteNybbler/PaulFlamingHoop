// Author(s): Paul Calande
// Adjusts tuning variables based on unity field entries.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTuningFromText : MonoBehaviour
{
    [SerializeField]
    InputField inputGameLength;
    [SerializeField]
    InputField inputRedFontTimeThreshold;
    [SerializeField]
    InputField inputGridWidth;
    [SerializeField]
    InputField inputGridHeight;
    [SerializeField]
    InputField inputTextBtnMenuStart;
    [SerializeField]
    InputField inputTextBtnMenuTuning;

    // Save the new tuning settings.
    public void Save()
    {
        Tuning.gameLength = float.Parse(inputGameLength.text);
        Tuning.redFontTimeThreshold = float.Parse(inputRedFontTimeThreshold.text);
        Tuning.gridWidth = int.Parse(inputGridWidth.text);
        Tuning.gridHeight = int.Parse(inputGridHeight.text);
        Tuning.textBtnMenuStart = inputTextBtnMenuStart.text;
        Tuning.textBtnMenuTuning = inputTextBtnMenuTuning.text;
    }
}