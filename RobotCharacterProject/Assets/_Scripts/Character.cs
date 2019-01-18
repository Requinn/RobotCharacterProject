using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character Controller class. Handles inputs for movement and interactions.
/// </summary>
public class Character : MonoBehaviour
{
    private Movement _movementComponent;
    private bool _isInZPlane = true;
    private Vector3 _approximateCenterHeight; //since transform root is at feet, center of body is adjusted to a proper height
    private RaycastHit _hit;
    private bool _canInteract = false, _canMove = true;

    //for when the player rotates
    public delegate void RotatedEvent(bool isZplane);
    public RotatedEvent OnRotation;

    /// <summary>
    /// Is the player currently moving in the Z-plane?
    /// </summary>
    /// <returns></returns>
    public bool IsCurrentPlaneZ() {
        return _isInZPlane;
    }

    private void Start() {
        _movementComponent = GetComponent<Movement>();
        _approximateCenterHeight = new Vector3(0f, 0.75f, 0f);
    }

    private void Update() {
        if (_canMove) {
            //side to side movement
            float movement = Input.GetAxis("Horizontal");
            //determine facing direction
            UpdateFacingDirection(movement);

            //Do movements
            if (_isInZPlane) {
                _movementComponent.Move(0, movement);
            }
            else {
                _movementComponent.Move(-movement, 0);
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
    /// Change rotation of the player depending on on the direciton they are moving
    /// </summary>
    /// <param name="movement"></param>
    private void UpdateFacingDirection(float movement) {
        if (movement > 0) {
            //if we are moving right in the z plane
            if (_isInZPlane) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            //moving right in the x plane
            if (!_isInZPlane) {
                transform.rotation = Quaternion.Euler(0, -90f, 0);
            }
        }
        else if (movement < 0) {
            //if we are moving left in the z plane
            if (_isInZPlane) {
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            //moving left in the x plane
            if (!_isInZPlane) {
                transform.rotation = Quaternion.Euler(0, 90f, 0);
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
        OnRotation(_isInZPlane);
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
