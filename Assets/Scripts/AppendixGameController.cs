// Author(s): Paul Calande
// This is the controller for Appendix: The Game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    List<AppendixCube> cubes = new List<AppendixCube>();

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
        int gridWidth = Tuning.gridWidth;
        int gridHeight = Tuning.gridHeight;

        // Calculate the width and height of the cells.
        float cellWidth = gridContainer.rect.width / gridWidth;
        float cellHeight = gridContainer.rect.height / gridHeight;
        Vector2 center = Vector2.zero;

        // To keep the cells square-shaped, make their width and height match.
        if (cellWidth < cellHeight)
        {
            cellHeight = cellWidth;
        }
        if (cellHeight < cellWidth)
        {
            cellWidth = cellHeight;
        }

        // Calculate the position of the top-left corner of the grid.
        float cornerX = center.x - ((gridWidth - 1) * cellWidth * 0.5f);
        float cornerY = center.y + ((gridHeight - 1) * cellHeight * 0.5f);

        for (int column = 0; column < gridWidth; ++column)
        {
            for (int row = 0; row < gridHeight; ++row)
            {
                //Vector2 position = new Vector2(x * 2, y * 2);
                Vector2 position = new Vector2(
                    cornerX + column * cellWidth, cornerY - row * cellHeight);

                // Instantiate a cube using the cube prefab.
                GameObject cubeObj = Instantiate(prefabCube, gridContainer);

                // Make each cube half the size of its grid cell to create gaps between cubes.
                RectTransform cubeTransform = cubeObj.GetComponent<RectTransform>();
                cubeTransform.sizeDelta = new Vector2(cellWidth, cellHeight) * 0.5f;
                cubeTransform.localPosition = position;

                // Adjust some settings on the cube.
                AppendixCube cube = cubeObj.GetComponent<AppendixCube>();
                // Clicking the wrong cube will take points away.
                cube.Clicked += WrongCubeClicked;
                // Add the cube to the list.
                cubes.Add(cube);
            }
        }
        // Choose a random cube and make it the Appendix.
        AppendixCube appendix = UtilRandom.GetRandomElement(cubes);
        appendix.MakeIntoAppendix();
        // Clicking the appendix won't take points away.
        appendix.Clicked -= WrongCubeClicked;
        // Clicking the appendix will win the game.
        appendix.Clicked += WinGame;
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