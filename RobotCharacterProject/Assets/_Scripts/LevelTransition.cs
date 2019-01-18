using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object that will go to the next level when the player enters it.
/// </summary>
public class LevelTransition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            GameController.Instance.GetSceneLoader().LoadNextLevel();
        }
    }
}
