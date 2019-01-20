using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;
    private ColorCode.ColorType _assignedColor;
    private Renderer _renderer;
    private int damage = 1;
    private bool _isHostile = true; //can this harm the player?
    public bool IsHostile { get { return _isHostile; } }
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
    /// reflecting the projectil and check if it was a perfect reflection
    /// </summary>
    /// <param name="newVelocity"></param>
    public void Reflect(bool isPerfect) {
        _isHostile = false;
        //could rotate the projectiles to face direction, but they're also just spheres right now so it doesn't matter really
        if (!isPerfect) {
            //plain reflections go normal speed and do 5 damage
            _rb.velocity *= -2;
            damage *= 5;
        }
        else {
            //perfects move double speed and do double damage
            _rb.velocity *= -4;
            damage *= 10;
        }
    }

    /// <summary>
    /// On colliding with the player, or with the base if the color isn't black, do damage
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        //game isn't running, projectile does nothing
        if (!GameController.Instance.GameActive) { Destroy(gameObject); return; }
        if (_isHostile && ( other.CompareTag("Player") || (other.CompareTag("Base") && _assignedColor != ColorCode.ColorType.black))) {
            GameController.Instance.GetPlayerReference().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if ((other.CompareTag("Cannon"))) {
            other.GetComponent<CannonController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
