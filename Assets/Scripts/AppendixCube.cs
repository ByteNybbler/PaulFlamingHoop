// Author(s): Paul Calande
// A script for one of many cubes in Appendix: The Game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppendixCube : MonoBehaviour
{
    public delegate void ClickedHandler();
    public event ClickedHandler Clicked;

    [SerializeField]
    [Tooltip("The image used to represent the cube.")]
    Image image;

    // Whether this cube is the appendix cube.
    bool isAppendix = false;
    // Whether this cube has been clicked yet.
    bool clicked = false;

    // Turns this cube into the Appendix.
    public void MakeIntoAppendix()
    {
        isAppendix = true;
    }

    // Click this cube.
    public void Click()
    {
        // If the cube has already been clicked, don't do anything.
        if (clicked)
        {
            return;
        }
        clicked = true;

        if (isAppendix)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.red;
        }

        OnClicked();
    }

    // This method should be called to disable the cube's functionality.
    public void Disable()
    {
        clicked = true;
    }

    private void OnClicked()
    {
        if (Clicked != null)
        {
            Clicked();
        }
    }
}