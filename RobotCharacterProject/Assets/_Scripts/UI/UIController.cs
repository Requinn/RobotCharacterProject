using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles UI button events
/// </summary>
public class UIController : MonoBehaviour {

    private GameController _controllerRef;
    private void Start() {
        _controllerRef = GameController.Instance;
    }
    /// <summary>
    /// Reload the level
    /// </summary>
    public void RestartGame() {
        _controllerRef.GetSceneLoader().ReloadScene();
    }

    /// <summary>
    /// Load Main Menu
    /// </summary>
    public void MainMenu() {
        _controllerRef.GetSceneLoader().LoadMainMenu();
    }

    /// <summary>
    /// Quit to desktop
    /// </summary>
    public void Quit() {
        _controllerRef.QuitGame();
    }

    /// <summary>
    /// Start normal game
    /// </summary>
    public void StartGame() {
        _controllerRef.GetSceneLoader().LoadLevel(1);
    }

}
