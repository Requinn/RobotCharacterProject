using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles state of the game
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField]
    private Character _playerRef;
    [SerializeField]
    private LevelLoader _sceneLoader;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //_playerRef = FindObjectOfType<Character>(); //get player reference
        _sceneLoader = GetComponent<LevelLoader>();
        SceneManager.activeSceneChanged += UpdatePlayerReference;
    }

    /// <summary>
    /// Update the player ref whenever we experience a scene change
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    private void UpdatePlayerReference(Scene arg0, Scene arg1) {
        _playerRef = FindObjectOfType<Character>();
    }

    public Character GetPlayerReference() {
        return _playerRef;
    }

    public LevelLoader GetSceneLoader() {
        return _sceneLoader;
    }

    public void GameOver() {
        //Pop up game over UI
        Debug.Log("GameOver");
    }

    public bool IsGameActive() {
        return _playerRef.isAlive();
    }
}
