using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

/// <summary>
/// Handles loading a level
/// </summary>
public class LevelLoader : MonoBehaviour
{
    /// <summary>
    /// Loads the next scene in the build settings
    /// </summary>
    public void LoadNextLevel() {
        int nextSceneID = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadSceneAsync(nextSceneID));
    }

    /// <summary>
    /// Loads a scene using an async call, spinlocking while loading
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private IEnumerator LoadSceneAsync(int index) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        asyncLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(0.5f);

        while(asyncLoad.progress < .89f) {
            yield return 0f;
        }

        yield return new WaitForSeconds(0.5f);
        asyncLoad.allowSceneActivation = true;
    }
}
