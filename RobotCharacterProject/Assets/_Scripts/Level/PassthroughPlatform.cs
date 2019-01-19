using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player being able to pass through the underside of a platform or drop through it. (i.e. smash bros. platforms)
/// </summary>
public class PassthroughPlatform : MonoBehaviour
{
    private Collider _platform;
    private Character _player;
    private float _reCheckDelay = 0.5f; //time to start checking player position relative to platform again
    private float _elapsedTime = 1f;

    private void Start() {
        _player = GameController.Instance.GetPlayerReference();
        _platform = GetComponent<Collider>();
    }

    private void Update() {
        if (_elapsedTime < _reCheckDelay) {
            _elapsedTime += Time.deltaTime;
        }
        else {
            //Check if the player is under or above the platform, if they are below disable collider, if they are above re-enable it
            //using raw position becasue it's located at the feet
            if (_player.transform.position.y < transform.position.y) {
                _platform.enabled = false;
            }
            else {
                _platform.enabled = true;
            }
        }
    }

    /// <summary>
    /// Turn off the collider to allow the player to pass through
    /// </summary>
    public void DisableCollider() {
        _platform.enabled = false;
        _elapsedTime = 0;
    }
}
