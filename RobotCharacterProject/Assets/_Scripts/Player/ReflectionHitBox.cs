using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reflects a projectile that enter this box
/// </summary>
public class ReflectionHitBox : MonoBehaviour
{
    private Projectile p;
    private Character _owner;

    [SerializeField]
    private float _perfectRangeMin = 0.5f, _perfectRangeMax = 1f;

    private void Start() {
        _owner = GetComponentInParent<Character>();
    }
    private void OnTriggerEnter(Collider other) {
        p = other.GetComponent<Projectile>();
    }

    private void OnTriggerExit(Collider other) {
        if(other.GetComponent<Projectile>() == p) {
            p = null;
        }
    }

    /// <summary>
    /// reflect the projectile and return the distance you reflected at
    /// </summary>
    /// <returns>returns true on "perfect"</returns>
    public bool DoReflect() {
        //distance to player
        if (p) {
            float distance = Vector3.Distance(_owner.GetCenter(), p.transform.position);
            p.ChangeVelocity(-p.GetComponent<Rigidbody>().velocity);
            if(distance >= _perfectRangeMin && distance < _perfectRangeMax) {
                return true;
            }else {
                return false;
            }
        }else {
            return false;
        }
    }
}
