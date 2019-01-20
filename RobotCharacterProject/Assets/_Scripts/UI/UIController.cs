using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles UI button events in a level
/// </summary>
public class UIController : MonoBehaviour {
    [SerializeField]
    private LevelLoader _sceneLoader;

    /// <summary>
    /// Reload the level
    /// </summary>
    public void RestartGame() {
        _sceneLoader.ReloadScene();
    }

    /// <summary>
    /// Load Main Menu
    /// </summary>
    public void MainMenu() {
        _sceneLoader.LoadMainMenu();
    }

    /// <summary>
    /// Quit to desktop
    /// </summary>
    public void Quit() {
        GameController.Instance.QuitGame();
    }

}
