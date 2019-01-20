using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseScreen;

    public delegate void PausedEvent();
    public PausedEvent OnPause;

    private bool _isPaused = false;
    public bool Paused { get { return _isPaused; } }

    private void Start() {
        _isPaused = false;
        _pauseScreen.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    /// <summary>
    /// Toggle pause UI and timescale
    /// </summary>
    public void TogglePause() {
        if (_isPaused) {
            _isPaused = false;
            _pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
        else if (!_isPaused) {
            _isPaused = true;
            _pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

}
