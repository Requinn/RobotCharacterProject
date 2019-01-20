using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles UI button events in a level
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
        LevelLoader.Instance.ReloadScene();
    }

    /// <summary>
    /// Load Main Menu
    /// </summary>
    public void MainMenu() {
        LevelLoader.Instance.LoadMainMenu();
    }

    /// <summary>
    /// Quit to desktop
    /// </summary>
    public void Quit() {
        _controllerRef.QuitGame();
    }

}
