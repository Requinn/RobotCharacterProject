using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;
    private ColorCode _assignedColor;
    private Renderer _renderer;

    public void Start() {
        _renderer = GetComponent<Renderer>();
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Initialize this projectile's speed and color/type
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="cType"></param>
    public void Initialize(float speed, ColorCode cType) {
        _rb.velocity = transform.forward * speed;
        _assignedColor = cType;
        _renderer.material.SetColor("_OutlineColor", _assignedColor.shaderColor);
        _renderer.material.SetColor("_Color", _assignedColor.shaderColor);
    }

    /// <summary>
    /// Change the velocity of this projectile
    /// </summary>
    /// <param name="newDirection"></param>
    /// <param name="newSpeed"></param>
    public void ChangeVelocity(Vector3 newDirection, float newSpeed) {
        //could rotate the projectiles to face direction, but they're also just spheres right now so it doesn't matter really
        _rb.velocity = newDirection * newSpeed;
    }



}
