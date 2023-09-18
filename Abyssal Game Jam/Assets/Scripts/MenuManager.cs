using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MenuState
{
    Playing,
    Pause,
    Main,
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
    public GameObject creditsMenu = null;
    public GameObject winMenu = null;
    public GameObject loseMenu = null;

    private GameObject currentMenu = null;

    /// <summary>
    /// Kills the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    [Header("Fade Foreground")]
    public Image fadeForeground = null;
    public float fadeTime = 0.5f;

    // parameters for handling screen fade in and out
    private bool isFading = false;
    private float fadeTimer = 0;
    private bool fadeIn = false;

    // when the scene is change this parameter stored the scene beign loaded
    // fade out starts and this scene is loaded at end
    private int loadingScene = -1;

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

    // Start is called before the first frame update
    void Start()
    {
        StartFade(true);

        if (state == MenuState.Main)
        {
            currentMenu = mainMenu;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FadeUpdate();
    }

    /// <summary>
    /// Starts screen fade
    /// </summary>
    void StartFade(bool fadeIn)
    {
        if (fadeForeground != null)
        {
            // enable the overlay
            fadeForeground.gameObject.SetActive(true);
            // initialise overlay opacity
            Color newColor = fadeForeground.color;
            if (fadeIn)
            {
                newColor.a = 1.0f;
            }
            else
            {
                newColor.a = 0.0f;
            }
            fadeForeground.color = newColor;
        }
        // set fade flags and start timer
        this.fadeIn = fadeIn;
        isFading = true;
        fadeTimer = 0;

        // start music fade
        //AudioManager.Instance.StartMusicFade(fadeTime, !fadeIn);
    }

    /// <summary>
    /// The update for the screen fade, updated overlay opacity
    /// and loads scene at end if need be
    /// </summary>
    void FadeUpdate()
    {
        // only called when fading flag on
        if (isFading)
        {
            if (fadeForeground != null)
            {
                // update timer
                fadeTimer += Time.deltaTime;

                // set foreground opacity from fraction of timer complete
                Color newColor = fadeForeground.color;
                newColor.a = fadeTimer / fadeTime;
                if (fadeIn)
                {
                    newColor.a = 1.0f - newColor.a;
                }
                fadeForeground.color = newColor;

                // fade is over
                if (fadeTimer >= fadeTime)
                {
                    // turn off foreground if the opacity is zero
                    if (fadeIn)
                    {
                        fadeForeground.gameObject.SetActive(false);
                    }
                    isFading = false;
                    // load scene which triggered fade
                    if (loadingScene >= 0)
                    {
                        SceneManager.LoadScene(loadingScene);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Load a new scene. Triggers fade out and stores scene to load
    /// </summary>
    public void LoadScene(int scene)
    {
        loadingScene = scene;
        StartFade(false);
    }

    /// <summary>
    /// Wrapper for SetState using int argument, for Unity events.
    /// </summary>
    public void SetState(int newState)
    {
        SetState((MenuState)newState);
    }

    /// <summary>
    /// Transitions the UI state, for example, openeing the pause menu
    /// </summary>
    public void SetState(MenuState newState)
    {
        // store current state
        state = newState;
        // turn off currently active menu
        if (currentMenu != null)
        {
            currentMenu.SetActive(false);
        }
        // sets currentMenu to new menu
        switch (newState)
        {
            case MenuState.Playing:
                // unpause the game
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.PauseGame(false);
                }
                currentMenu = null;
                break;
            case MenuState.Pause:
                if (GameManager.Instance != null)
                {
                    if (!GameManager.Instance.IsPaused())
                    {
                        // pause the game if not paused
                        GameManager.Instance.PauseGame(true);
                    }
                    else
                    {
                        // go back if is paused
                        Back();
                        return;
                    }
                }
                currentMenu = pauseMenu;
                break;
            case MenuState.Main:
                currentMenu = mainMenu;
                break;
            case MenuState.Credits:
                currentMenu = creditsMenu;
                break;
            case MenuState.WinScreen:
                currentMenu = winMenu;
                break;
            case MenuState.LoseScreen:
                currentMenu = loseMenu;
                break;
        }

        // activate new menu
        if (currentMenu != null)
        {
            currentMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Gets current menu state
    /// </summary>
    public MenuState GetState()
    {
        return state;
    }

    /// <summary>
    /// Go back to the previous menu state
    /// </summary>
    public void Back()
    {
        // decides where to go back to depending on the current state
        switch (state)
        {
            case MenuState.Playing:
                SetState(MenuState.Pause);
                break;
            case MenuState.Pause:
                SetState(MenuState.Playing);
                break;
            case MenuState.Main:
                break;
                if (GameManager.Instance == null)
                {
                    // if null means we are in the main menu scene
                    SetState(MenuState.Main);
                }
                else
                {
                    // we are in the main game scene
                    SetState(MenuState.Pause);
                }
                break;

            case MenuState.Credits:
                if (GameManager.Instance == null)
                {
                    // if null means we are in the main menu scene
                    SetState(MenuState.Main);
                }
                else
                {
                    // we are in the main game scene, open different menus based on win state
                    if (GameManager.Instance.HasPlayerWon())
                    {
                        SetState(MenuState.WinScreen);
                    }
                    else
                    {
                        SetState(MenuState.LoseScreen);
                    }
                }
                break;
            case MenuState.WinScreen:
                // go back to main menu
                LoadScene(0);
                break;
            case MenuState.LoseScreen:
                // go back to main menu
                LoadScene(0);
                break;
        }
    }
}