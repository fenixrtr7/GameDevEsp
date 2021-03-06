﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using Yarn.Unity;

public class GameManager : Manager<GameManager>
{
    //public int currentLevel = 1;
    public enum GameState
    {
        MENU,
        RUNNING,
        PAUSED,
        GAMEOVER,
        COMBAT,
        DIALOG
    }

    public Events.EventGameState OnGameStateChanged;

    public GameObject player;
    public DialogueUI dialogue;

    GameState _currentGameState = GameState.MENU;
    //int numberScenes;

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set { _currentGameState = value; }
    }

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);

        StartCoroutine(StartGameAudio());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }

        if (Input.GetButtonDown("Jump") && _currentGameState == GameState.MENU)
        {
            StartGame();
        }

        if (Input.GetButtonDown("Jump") && _currentGameState == GameState.DIALOG)
        {
            dialogue.MarkLineComplete();
            //EventManager.Instance.EndDialogCharacter();
        }
    }

    IEnumerator StartGameAudio()
    {
        yield return new WaitForEndOfFrame();
        AudioManager.Instance.PlayClipInSource("AmbienceAudioSource", null, 1);
        
    }

    public void StartGame()
    {
        player.GetComponent<PlayerController>().CameraAnim.SetTrigger("intro");
        UI_Items.Instance.GetComponent<Animator>().SetTrigger("intro");
        UpdateState(GameState.RUNNING);
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

            case GameState.DIALOG:
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

    public void ResetLevel()
    {
        ChangeScene(SceneManager.GetActiveScene().name);
    }
}

