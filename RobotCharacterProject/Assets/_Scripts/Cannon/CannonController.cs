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

        _cannon2.GetComponent<Cannon>().enabled = false;
        _cannon2.transform.localPosition = new Vector3(_cannon2.transform.localPosition.x, _cannon2.transform.localPosition.y, _initialZPosition);

        _cannon3.GetComponent<Cannon>().enabled = false;
        _cannon3.transform.localPosition = new Vector3(_cannon3.transform.localPosition.x, _cannon3.transform.localPosition.y, _initialZPosition);
    }

    /// <summary>
    /// Check if its time to bring out another cannon as well
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        if (_currentStage == 0 && CurrentHealthPercent() <= _firstThreshold) {
            _currentStage++;
            StartCoroutine(BringOutCannon(_cannon2));

        }

        if (_currentStage == 1 && CurrentHealthPercent() <= _secondThreshold) {
            _currentStage++;
            StartCoroutine(BringOutCannon(_cannon3));
        }
    }

    /// <summary>
    /// move the cannon then enable it
    /// </summary>
    /// <param name="cannonToMove"></param>
    /// <returns></returns>
    private IEnumerator BringOutCannon(GameObject cannonToMove) {
        float time = 0f;
        Vector3 startPosition = cannonToMove.transform.localPosition;
        Vector3 finalPosition = new Vector3(startPosition.x, startPosition.y, _finalZPosition);
        while (cannonToMove.transform.localPosition.z != _finalZPosition) {
            cannonToMove.transform.localPosition = Vector3.Lerp(startPosition, finalPosition, time);
            time += Time.deltaTime * 2f;
            yield return null;
        }
        cannonToMove.GetComponent<Cannon>().enabled = true;
    }
}
