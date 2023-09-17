using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void PauseTheMenu()
    {
        pauseMenu.SetActive(true);
        GameManager.Instance.PauseGame(true);
    }

    public void UnPauseTheMenu()
    {
        pauseMenu.SetActive(false);
        GameManager.Instance.PauseGame(false);
    }
}
