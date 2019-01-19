using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fires a projectile and assigns it a color based on the current cannon color
/// </summary>
public class Cannon : MonoBehaviour
{
    [SerializeField]
    private Projectile _projectile;
    [SerializeField]
    private float _initialDelay;
    [SerializeField]
    private float _fireDelay; //time between shots
    [SerializeField]
    private float _fireDelayDeviation = 0.5f;
    [SerializeField]
    private float _projectileSpeed; //how fast does the projectile move
    [SerializeField]
    private GameObject _barrelPoint;

    private ColorCode.ColorType _currentType;
    //private WaitForSeconds _fireWait;

    private void Start() {
        StartCoroutine(FiringRoutine());
        //_fireWait = new WaitForSeconds(_fireDelay);
    }

    /// <summary>
    /// While the game is active, fire cannons
    /// </summary>
    /// <returns></returns>
    private IEnumerator FiringRoutine() {
        yield return new WaitForSeconds(_initialDelay);
        while (GameController.Instance.IsGameActive()) {
            AssignColor();
            yield return new WaitForSeconds(_fireDelay + UnityEngine.Random.Range(-_fireDelayDeviation / 2, _fireDelayDeviation));
            //could object pool, but not enough projectiles to warrant for now?
            Projectile p = Instantiate(_projectile, _barrelPoint.transform.position, _barrelPoint.transform.rotation);
            p.Initialize(_projectileSpeed, _currentType);
            yield return null;
        }
    }

    /// <summary>
    /// assigns a random color
    /// </summary>
    private void AssignColor() {
        int random = UnityEngine.Random.Range(0, 4);
        switch (random) {
            case 0:
                _currentType = ColorCode.ColorType.orange;
                break;
            case 1:
                _currentType = ColorCode.ColorType.blue;
                break;
            case 2:
                _currentType = ColorCode.ColorType.purple;
                break;
            case 3:
                _currentType = ColorCode.ColorType.black;
                break;
        }
    }
}
