using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MenuState
{
    Playing,
    Pause,
    Main,
    Settings,
    Credits,
    WinScreen,
    LoseScreen
}
public class MenuManager : MonoBehaviour
{
    public static MenuManager instance { get; private set; }

    [SerializeField]
    private MenuState state = MenuState.Main;

    [Header("Menus")]
    public GameObject pauseMenu = null;
    public GameObject mainMenu = null;
    public GameObject settingsMenu = null;
    public GameObject creditsMenu = null;
    public GameObject winMenu = null;
    public GameObject loseMenu = null;

    //private GameObject currentMenu = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    /// <summary>
    /// Kills the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}