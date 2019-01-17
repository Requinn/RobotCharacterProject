using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Movement _movementComponent;

    private void Start() {
        _movementComponent = GetComponent<Movement>();
    }

    private void Update() {
        //side to side movement
        _movementComponent.Move(Input.GetAxis("Horizontal"));

        //jumping, don't really need to get the vertical axis as this is impulse
        if ((Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.W))) && _movementComponent.IsGrounded()) {
            _movementComponent.Jump();
        }
        //punching aka interacting
        if(Input.GetKeyDown(KeyCode.Space) && _movementComponent.IsGrounded()) {
            Punch();
        }
        //check front for a punchable object
        //raycast
    }

    private void Punch() {
        //punch an object
    }
}
