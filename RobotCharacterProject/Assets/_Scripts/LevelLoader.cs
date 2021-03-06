﻿using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

/// <summary>
/// Handles loading a level
/// </summary>
public class LevelLoader : MonoBehaviour
{
    public delegate void StartLoadEvent();
    public StartLoadEvent OnStartLoad;

    public delegate void EndLoadEvent();
    public EndLoadEvent OnEndLoad;

    /// <summary>
    /// Loads the next scene in the build settings
    /// </summary>
    public void LoadNextLevel() {
        int nextSceneID = SceneManager.GetActiveScene().buildIndex + 1;
        LoadLevel(nextSceneID);
    }

    /// <summary>
    /// Reloads the current scene
    /// </summary>
    public void ReloadScene() {
        int nextSceneID = SceneManager.GetActiveScene().buildIndex;
        LoadLevel(nextSceneID);
    }

    /// <summary>
    /// Load the main menu
    /// </summary>
    public void LoadMainMenu() {
        LoadLevel(0);
    }

    /// <summary>
    /// Load a specific scene
    /// </summary>
    /// <param name="index"></param>
    public void LoadLevel(int index) {
        StartCoroutine(LoadSceneAsync(index));
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Loads a scene using an async call, spinlocking while loading
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private IEnumerator LoadSceneAsync(int index) {
        if(OnStartLoad != null) OnStartLoad();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        asyncLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(0.5f);

        while(asyncLoad.progress < .89f) {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        if (OnEndLoad != null) OnEndLoad();
        asyncLoad.allowSceneActivation = true;
    }
}
