// Author(s): Paul Calande
// Tuning variables for Appendix: The Game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tuning
{
    // The length of the game, in seconds.
    public static float gameLength = 10.0f;
    // The time at which the timer font turns red.
    public static float redFontTimeThreshold = 5.0f;
    // The width of the grid.
    public static int gridWidth = 8;
    // The height of the grid.
    public static int gridHeight = 5;
    // The text that should appear on the main menu's start button.
    public static string textBtnMenuStart = "Start";
    // The text that should appear on the main menu's tuning variables button.
    public static string textBtnMenuTuning = "Tuning Variables";
}