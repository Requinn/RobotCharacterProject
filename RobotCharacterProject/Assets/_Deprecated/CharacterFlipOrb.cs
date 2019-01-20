﻿using System;
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

    public override void Start() {
        base.Start();
        _adjustedPosition = transform.position;
        _characterRef = GameController.Instance.GetPlayerReference();
    }
    public override void Interact() {
        StartCoroutine(FlipRoutine());
    }

    /// <summary>
    /// Flip the player to the new plane
    /// </summary>
    /// <returns></returns>
    private IEnumerator FlipRoutine() {
        _characterRef.SetMovement(false);
        _characterRef.transform.position = transform.position - new Vector3(0,.75f,0);
        yield return _delay;
        //_characterRef.RotateForward();        
        _characterRef.SetMovement(true);
        yield return null;
    }
}