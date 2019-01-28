// Author(s): Paul Calande
// This is the controller for Appendix: The Game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AppendixGameController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The prefab to use for spawning cubes.")]
    GameObject prefabCube;
    [SerializeField]
    [Tooltip("The text that displays the timer.")]
    Text textTimer;
    [SerializeField]
    [Tooltip("The container for the grid.")]
    RectTransform gridContainer;

    [SerializeField]
    [Tooltip("The parent GameObject of the game summary details.")]
    GameObject gameSummaryWindow;
    [SerializeField]
    [Tooltip("The game summary text that says whether the player won.")]
    Text textGameSummaryWinState;
    [SerializeField]
    [Tooltip("The game summary text that says the player's score.")]
    Text textGameSummaryScore;

    // The timer that ends the game when it runs out of time.
    Timer timerGame;
    // The score. Decreases when wrong cubes are clicked.
    int score = 1000;

    // The collection of all cubes in the grid.
    List<AppendixCube> cubes;

    void Start()
    {
        PopulateGrid();

        timerGame = new Timer(Tuning.gameLength, TimerGame_Finished, false);
        timerGame.Run();
    }

    private void FixedUpdate()
    {
        // Progress the timer.
        timerGame.Tick(Time.deltaTime);
        float secondsLeft = timerGame.GetSecondsRemaining();

        // Update the timer text.
        // "F1" keeps the float text rounded to 1 decimal place.
        textTimer.text = secondsLeft.ToString("F1");

        // Turn the timer text red past a certain threshold.
        if (secondsLeft <= Tuning.redFontTimeThreshold)
        {
            textTimer.color = Color.red;
        }
    }

    // Populates the grid of cubes.
    private void PopulateGrid()
    {
        // Clicking the wrong cube will take points away.
        Action<AppendixCube> cubeCallback = (x) => x.Clicked += WrongCubeClicked;
        // Instantiate the grid of cubes.
        cubes = UtilInstantiate.GridOfRectTransforms(
            Tuning.gridWidth, Tuning.gridHeight, prefabCube, true,
            gridContainer, 0.5f, cubeCallback);

        if (cubes.Count != 0)
        {
            // Choose a random cube and make it the Appendix.
            AppendixCube appendix = UtilRandom.GetRandomElement(cubes);
            appendix.MakeIntoAppendix();
            // Clicking the appendix won't take points away.
            appendix.Clicked -= WrongCubeClicked;
            // Clicking the appendix will win the game.
            appendix.Clicked += WinGame;
        }
    }

    private void WrongCubeClicked()
    {
        score -= 10;
    }

    // Activates the game summary screen.
    private void SummarizeGame(bool victory, int score)
    {
        // Disable the timer text.
        textTimer.gameObject.SetActive(false);
        // Notify all of the cubes that the game is over.
        // This will disable their functionality.
        foreach (AppendixCube cube in cubes)
        {
            cube.Disable();
        }

        gameSummaryWindow.SetActive(true);
        if (victory)
        {
            textGameSummaryWinState.text = "Victory!";
        }
        else
        {
            textGameSummaryWinState.text = "Defeat!";
        }
        textGameSummaryScore.text = "Score: " + score;
    }

    // Wins the game.
    private void WinGame()
    {
        timerGame.Stop();
        SummarizeGame(true, score);
    }

    // Callback method for when the timer finishes.
    private void TimerGame_Finished(float secondsOverflow)
    {
        SummarizeGame(false, 0);
    }
}