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
    private AnimationHandler _animationComponent;

    private bool _isInZPlane = true; //are we moving along the z-axis
    private Vector3 _approximateCenterHeight; //since transform root is at feet, center of body is adjusted to a proper height
    private RaycastHit _hit;
    private bool _canInteract = false, _canMove = true;
    private Interactable _selectedInteractable = null;

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
        _animationComponent = GetComponent<AnimationHandler>();
        _approximateCenterHeight = new Vector3(0f, 0.75f, 0f);
    }

    float movement;
    private void Update() {
        if (_canMove) {
            //side to side movement
            movement = Input.GetAxis("Horizontal");
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
                _animationComponent.DoJump();
            }
            //punching aka interacting only when on ground
            if (Input.GetKeyDown(KeyCode.Space) && _movementComponent.IsGrounded()) {
                _animationComponent.DoPunch();
                StartCoroutine(Punch());
            }
        }
        //since we aren't moving, set movement to 0
        else {
            movement = 0;
        }
        //tell the animator our current movement value
        _animationComponent.SetSpeed(Mathf.Abs(movement));

        if (_movementComponent.IsGrounded()) {
            Physics.Raycast(transform.position + _approximateCenterHeight, transform.forward, out _hit, 2f);
            if (_hit.collider) {
                _selectedInteractable = _hit.collider.GetComponent<Interactable>();
                if(_selectedInteractable) _selectedInteractable.ToggleHighlight();
            }
            else {
                _selectedInteractable = null;
            }
        }else {
            _selectedInteractable = null;
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
    /// Interact with stuff by punching them, punch animation is slow, so it has the delay to match
    /// </summary>
    private IEnumerator Punch() {
        SetMovement(false);
        yield return new WaitForSeconds(1.25f);
        if (_selectedInteractable) {
            _selectedInteractable.Interact();
        }
        yield return new WaitForSeconds(0.1f);
        SetMovement(true);
        yield return null;
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
            transform.forward = transform.right;
            _isInZPlane = true;
        }
    }
}
