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
    [SerializeField]
    private PauseController _pauseController;

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
        SceneManager.activeSceneChanged += UpdateReferences;
    }

    public bool GetPaused() {
        return _pauseController.Paused;
    }

    public void TogglePause() {
        _pauseController.TogglePause();
    }

    /// <summary>
    /// Update References
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    private void UpdateReferences(Scene arg0, Scene arg1) {
        _playerRef = FindObjectOfType<Character>();
        _pauseController = FindObjectOfType<PauseController>();
    }

    public Character GetPlayerReference() {
        return _playerRef;
    }

    public LevelLoader GetSceneLoader() {
        return _sceneLoader;
    }

    /// <summary>
    /// Sets the game over UI for either win or loss
    /// </summary>
    /// <param name="screenCode">1 = win, 2 = player death, 3 = castle death</param>
    public void GameOver(int screenCode) {
        //Pop up game over UI
        Debug.Log("GameOver");
    }

    public bool IsGameActive() {
        return _playerRef.isAlive();
    }

    public void QuitGame() {
        Application.Quit(0);
    }
}
