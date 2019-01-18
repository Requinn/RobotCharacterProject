using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to flip the player into the other plane, and handle their new position afterwards
/// </summary>
public class CharacterFlipOrb : Interactable
{
    private Character _characterRef;
    private WaitForSeconds _delay = new WaitForSeconds(.1f);
    private Vector3 _adjustedPosition;

    private void Start() {
        _adjustedPosition = transform.position;
        _characterRef = GameController.Instance.GetPlayerReference();
    }
    public override void Interact() {
        StartCoroutine(FlipRoutine());
        Debug.Log(GameController.Instance.GetPlayerReference().transform.position + "  "+  transform.position);
    }

    private IEnumerator FlipRoutine() {
        _characterRef.SetMovement(false);
        _characterRef.transform.position = transform.position;
        yield return _delay;
        _characterRef.RotateForward();        
        _characterRef.SetMovement(true);
        yield return null;
    }
}
