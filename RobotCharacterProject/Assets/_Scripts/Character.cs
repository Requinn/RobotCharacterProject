﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Movement _movementComponent;
    private bool _isInZPlane = true;
    private Vector3 _approximateCenterHeight; //since transform root is at feet, center of body is adjusted to a proper height
    private RaycastHit _hit;
    private bool _canInteract = false, _canMove = true;

    private void Start() {
        _movementComponent = GetComponent<Movement>();
        _approximateCenterHeight = new Vector3(0f, 0.75f, 0f);
    }

    private void Update() {
        if (_canMove) {
            //side to side movement
            if (_isInZPlane) {
                _movementComponent.Move(0, Input.GetAxis("Horizontal"));
            }
            else {
                _movementComponent.Move(Input.GetAxis("Horizontal"), 0);
            }

            //jumping, don't really need to get the vertical axis as this is impulse
            if ((Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.W))) && _movementComponent.IsGrounded()) {
                _movementComponent.Jump();
            }
        }
        //punching aka interacting
        if(Input.GetKeyDown(KeyCode.Space) && _movementComponent.IsGrounded() && _canInteract) {
            Punch();
        }
        //check front for a punchable object
        //raycast forward, check for grounded, then punch
        if (_movementComponent.IsGrounded()) {
            Debug.DrawLine(transform.position + _approximateCenterHeight, transform.position + _approximateCenterHeight + transform.forward * 1.5f, Color.red);
            Physics.Raycast(transform.position + _approximateCenterHeight, transform.forward, out _hit, 1f);
            if (_hit.collider && _hit.collider.CompareTag("Interactable")) {
                _canInteract = true;
            }
            else {
                _canInteract = false;
            }
        }
    }

    /// <summary>
    /// Interact with stuff
    /// </summary>
    private void Punch() {
        _hit.collider.GetComponent<Interactable>().Interact();
    }

    /// <summary>
    /// Set whether the player can move or not
    /// </summary>
    /// <param name="canMove"></param>
    public void SetMovement(bool canMove) {
        _canMove = canMove;
        _movementComponent.SetMovement(canMove);
    }

    /// <summary>
    /// Rotate the player to a new plane
    /// </summary>
    public void RotateForward() {
        if (_isInZPlane) {
            transform.forward = transform.right;
            _isInZPlane = false;
        }
        else {
            transform.forward = -transform.right;
            _isInZPlane = true;
        }
    }
}
