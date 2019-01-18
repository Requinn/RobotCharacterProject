using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle animations for the character
/// </summary>
public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Set the speed value in the animator
    /// </summary>
    /// <param name="speed"></param>
    public void SetSpeed(float speed) {
        _animator.SetFloat("Speed", speed);
    }

    /// <summary>
    /// Fire the Jump trigger
    /// </summary>
    public void DoJump() {
        _animator.SetTrigger("Jump");
    }

    /// <summary>
    /// Fire the punch trigger
    /// </summary>
    public void DoPunch() {
        _animator.SetTrigger("Punch");
    }
}
