using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the cannons
/// </summary>
public class CannonController : Entity
{
    [SerializeField]
    private float _firstThreshold, _secondThreshold;
    [SerializeField]
    private GameObject _cannon2, _cannon3;
    [SerializeField]
    private float _initialZPosition, _finalZPosition;

    private int _currentStage = 0;

    public override void Start() {
        base.Start();

        _cannon2.SetActive(false);
        _cannon2.transform.position = new Vector3(_cannon2.transform.position.x, _cannon2.transform.position.y, _initialZPosition);

        _cannon3.SetActive(false);
        _cannon3.transform.position = new Vector3(_cannon3.transform.position.x, _cannon3.transform.position.y, _initialZPosition);
    }

    /// <summary>
    /// Check if its time to bring out another cannon as well
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        if (_currentStage == 0 && CurrentHealthPercent() <= _firstThreshold) {
            _currentStage++;
            //bring out second cannon

        }

        if (_currentStage == 1 && CurrentHealthPercent() <= _secondThreshold) {
            _currentStage++;
            //bring out third cannon
        }
    }
}
