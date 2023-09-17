using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    bool isGameOver = false;
    bool playerWin = false;
    bool pausedGame = false;

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void GameOver(bool flag)
    {
        isGameOver = flag;
    }

    public bool HasPlayerWon()
    {
        return playerWin;
    }

    public bool IsPaused()
    {
        return pausedGame;
    }

    public void PauseGame(bool flag)
    {
        pausedGame = flag;
    }
}
