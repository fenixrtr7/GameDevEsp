using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameManager : Manager<GameManager>
{
    //public int currentLevel = 1;
    public enum GameState
    {
        MENU,
        RUNNING,
        PAUSED,
        GAMEOVER,
        COMBAT
    }

    public Events.EventGameState OnGameStateChanged;

    public GameObject player;

    GameState _currentGameState = GameState.MENU;
    //int numberScenes;

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set { _currentGameState = value; }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        // currentLevel = SceneManager.GetActiveScene().buildIndex;
        // numberScenes = SceneManager.sceneCountInBuildSettings - 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }

        if (Input.GetButtonDown("Fire1") && _currentGameState == GameState.MENU)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        player.GetComponent<Control>().CameraAnim.SetTrigger("intro");
        UI_Items.Instance.GetComponent<Animator>().SetTrigger("intro");
        UpdateState(GameState.RUNNING);
    }

    public void ButtonMechanciStart()
    {
        UpdateState(GameState.COMBAT);
        //StartCoroutine(Spawner.Instance.SpawnArrow());
        StartCoroutine(Spawner.Instance.SpawnArrowDuel());
    }


    public void GameOver()
    {
        UpdateState(GameState.GAMEOVER);
    }

    public void TogglePause()
    {
        UpdateState(_currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    public void GoMenu()
    {
        UpdateState(GameState.MENU);
    }

    // Cambiar estado de juego
    public void UpdateState(GameState state)
    {
        GameState previousGameState = _currentGameState;
        _currentGameState = state;

        switch (_currentGameState)
        {
            case GameState.MENU:
                Time.timeScale = 1.0f;
                break;

            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;

            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;

            case GameState.GAMEOVER:
                Time.timeScale = 1.0f;
                break;

            case GameState.COMBAT:
                Time.timeScale = 1.0f;
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ChangeScene(int num)
    {
        SceneManager.LoadScene(num);
    }

    // public void NextLevel()
    // {
    //     //Debug.Log("number Scenes: " + numberScenes + "current Level: " + currentLevel);

    //     if (numberScenes == currentLevel)
    //     {
    //         StartCoroutine(UIManager.Instance.WinPanel());
    //         // Win
    //         //Debug.Log("win");
    //         currentLevel = 0;
    //         GoMenu();
    //         ChangeScene(currentLevel);
    //         // On win and send menu
    //     }
    //     else
    //     {
    //         currentLevel++;
    //         ChangeScene(currentLevel);
    //     }

    // }

    public void ResetLevel()
    {
        ChangeScene(SceneManager.GetActiveScene().name);
    }
}

