using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;
    private ColorCode.ColorType _assignedColor;
    private Renderer _renderer;

    /// <summary>
    /// Get the type of color
    /// </summary>
    /// <returns></returns>
    public ColorCode.ColorType GetColorType() {
        return _assignedColor;
    }

    /// <summary>
    /// Initialize this projectile's speed and color/type
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="cType"></param>
    public void Initialize(float speed, ColorCode.ColorType cType) {
        _renderer = GetComponent<Renderer>();
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * speed;
        _assignedColor = cType;
        Color c = ColorCode.ColorCoder.GetColor(cType);
        _renderer.material.SetColor("_OutlineColor", c);
        _renderer.material.SetColor("_Color", c);
    }

    /// <summary>
    /// Change the velocity of this projectile
    /// </summary>
    /// <param name="newVelocity"></param>
    public void ChangeVelocity(Vector3 newVelocity) {
        //could rotate the projectiles to face direction, but they're also just spheres right now so it doesn't matter really
        _rb.velocity = newVelocity;
    }

    /// <summary>
    /// On colliding with the player, or with the base if the color isn't black, do damage
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") || (other.CompareTag("Base") && _assignedColor != ColorCode.ColorType.black)) {
            GameController.Instance.GetPlayerReference().TakeDamage();
            Destroy(gameObject);
        }else if((other.CompareTag("Base") && _assignedColor == ColorCode.ColorType.black)) {
            Destroy(gameObject);
        }
    }

}
