﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles state of the game and holds references to important game things.
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private Character _playerRef;
    private PauseController _pauseController;

    public delegate void GameOverEvent();
    public GameOverEvent OnGameWin, OnPlayerDeath;

    private bool _isGameActive = true, _isGamePaused = false;
    public bool Paused { get { return _isGamePaused; } }
    public bool GameActive { get { return _isGameActive; } }

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.activeSceneChanged += UpdateReferences;
    }

    /// <summary>
    /// Update References
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    private void UpdateReferences(Scene arg0, Scene arg1) {
        //get ref to player, and their death event
        _playerRef = FindObjectOfType<Character>();
        _playerRef.OnDeath += GameLoss;
        //get the death event of the enemy
        FindObjectOfType<CannonController>().OnDeath += GameWon;

        _pauseController = FindObjectOfType<PauseController>();
        _pauseController.OnPause += UpdatePauseState;

        _isGameActive = true;
        _isGamePaused = false;
    }

    /// <summary>
    /// player died and game is over
    /// </summary>
    private void GameLoss() {
        if (OnPlayerDeath != null) OnPlayerDeath();
        _playerRef.SetMovement(false);
        _isGameActive = false;
    }

    /// <summary>
    /// Cannons are defeated
    /// </summary>
    private void GameWon() {
        if (OnGameWin != null) OnGameWin();
        _playerRef.SetMovement(false);
        _isGameActive = false;
    }

    private void UpdatePauseState(bool state) {
        _isGamePaused = state;
    }

    public Character GetPlayerReference() {
        return _playerRef;
    }

    public void QuitGame() {
        Application.Quit(0);
    }
}
