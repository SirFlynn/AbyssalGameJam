using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject priest;
    int spawnLevel = 13;
    bool isPriestThere = false;

    CharacterData[] characters;

    bool isGameOver = false;
    bool playerWin = false;
    bool pausedGame = false;

    private void Start()
    {
        characters = FindObjectsByType<CharacterData>(FindObjectsSortMode.None);
    }

    private void Update()
    {
        
        if (!isPriestThere)
        {
            int value = 0;
            foreach (CharacterData cd in characters)
            {
                value += cd.fearMeter;
            }
            if (value >= spawnLevel)
            {
                SpawnPriest();
                isPriestThere = true;
            }
        }
        
    }

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

    public void SpawnPriest()
    {
        priest.SetActive(true);
    }
}
