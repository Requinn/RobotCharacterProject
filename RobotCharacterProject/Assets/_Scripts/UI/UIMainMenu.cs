using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles events for main menu UI
/// </summary>
public class UIMainMenu : MonoBehaviour
{
    [SerializeField]
    private LevelLoader _sceneLoader;

    /// <summary>
    /// Start normal game
    /// </summary>
    public void StartGame() {
        _sceneLoader.LoadLevel(1);
    }

    public void Quit() {
        Application.Quit();
    }
}
