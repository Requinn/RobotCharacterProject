using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character Controller class. Handles inputs for movement and interactions.
/// </summary>
public class Character : MonoBehaviour
{
    [SerializeField]
    private int _health = 5;
    [SerializeField]
    private ReflectionHitBox _reflectionHitBox;

    private Movement _movementComponent;
    private AnimationHandler _animationComponent;

    private Vector3 _approximateCenterHeight; //since transform root is at feet, center of body is adjusted to a proper height
    private RaycastHit _hit;
    private bool _canMove = true;

    //for when the player rotates
    public delegate void RotatedEvent(bool isZplane);
    public RotatedEvent OnRotation;

    private void Start() {
        _movementComponent = GetComponent<Movement>();
        _animationComponent = GetComponent<AnimationHandler>();
        _approximateCenterHeight = new Vector3(0f, 0.75f, 0f);
    }

    /// <summary>
    /// get the adjusted center of the player
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCenter() {
        return transform.position + _approximateCenterHeight;
    }

    public bool isAlive() {
        return _health > 0;
    }

    float movement;
    private void Update() {
        if (_canMove) {
            //side to side movement
            movement = Input.GetAxis("Horizontal");
            //determine facing direction
            UpdateFacingDirection(movement);

            //Do movements
            _movementComponent.Move(0, movement);

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
    }

    /// <summary>
    /// Change rotation of the player depending on on the direciton they are moving
    /// </summary>
    /// <param name="movement"></param>
    private void UpdateFacingDirection(float movement) {
        if (movement > 0) {
            //if we are moving right in the z plane
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movement < 0) {
            //if we are moving left in the z plane
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }

    /// <summary>
    /// Punch the air and maybe hit a projectile to deflect it
    /// </summary>
    private IEnumerator Punch() {
        SetMovement(false);
        yield return new WaitForSeconds(.55f);
        //activate a hitbox to deflect the projectile
        _reflectionHitBox.DoReflect();
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
    /// Remove health
    /// </summary>
    public void TakeDamage() {
        _health--;
        if(_health <= 0) {
            _health = 0;
            Kill();
        }
        //updateUI

    }

    /// <summary>
    /// Handle player death and relay to GameController
    /// </summary>
    public void Kill() {
        SetMovement(false);
        gameObject.SetActive(false);
        GameController.Instance.GameOver();
    }
}
