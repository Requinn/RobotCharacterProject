using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Accessed by Character to handle movement.
/// </summary>
public class Movement : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed, _jumpHeight;

    private float _centerToGround;
    private bool _canMove = true, _inAir = false;
    private Vector3 _movement;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _centerToGround = GetComponent<Collider>().bounds.extents.y; //get the height in the y direction
        if (!_controller) {
            gameObject.AddComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_canMove) { return; }
        //_movement = transform.TransformDirection(_movement);
        _controller.Move(_movement * Time.deltaTime);

        _inAir = IsGrounded();
        if (!_inAir) {
            _movement += Physics.gravity * Time.deltaTime;
        }
        //removed the built up gravitational force when landing and replace with a small downforce
        else if (_inAir) {
            _movement.y = -_controller.stepOffset / Time.deltaTime;
        }
    }

    /// <summary>
    /// Adds vertical movement.
    /// </summary>
    public void Jump() {
        _movement.y = 0f;
        _movement.y += _jumpHeight;
    }

    /// <summary>
    /// Move the character by movement speed in direction
    /// </summary>
    public void Move(float xDir, float zDir) {
        _movement.x = xDir * _speed;
        _movement.z = zDir * _speed;
    }

    /// <summary>
    /// Allows this Movement to update
    /// </summary>
    /// <param name="canMove"></param>
    public void SetMovement(bool canMove) {
        _canMove = canMove;
    }

    private RaycastHit _hitInfo;
    /// <summary>
    /// Check if the character is on the ground
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded() {
        /**
        Physics.Raycast(transform.position, Vector3.down, out _hitInfo, _centerToGround + .01f);
        if (_hitInfo.transform) {
            return _hitInfo.transform.CompareTag("Ground");
        }
        else {
            return false;
        }**/
        return _controller.isGrounded;
    }
}
