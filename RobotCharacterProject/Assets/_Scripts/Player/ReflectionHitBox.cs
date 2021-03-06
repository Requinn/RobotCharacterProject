﻿using System.Collections;
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
    [SerializeField]
    private AudioClip _perfectSound, _normalSound;

    private AudioSource _audioPlayer;

    private void Start() {
        _owner = GetComponentInParent<Character>();
        _audioPlayer = GetComponent<AudioSource>();
        //adjusted for sqrmag check later
        _perfectRangeMin *= _perfectRangeMin;
        _perfectRangeMax *= _perfectRangeMax;
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
    /// reflect the projectile and return true if we reflected successfully
    /// </summary>
    /// <returns>returns true on reflect</returns>
    public bool DoReflect(ColorCode.ColorType color, out bool isPerfect) {
        if (p && p.IsHostile && p.GetColorType() == color) {
            //super easy way to detect if projectile is in front of us by checking along the only axis we share with projectiles
            if (p.transform.position.z < _owner.transform.position.z) {
                isPerfect = false;
                return false;
            }
            //distance to player
            float distance = Vector3.SqrMagnitude(_owner.GetCenter() - p.transform.position);
            //sweet spot
            if(distance >= _perfectRangeMin && distance < _perfectRangeMax) {
                p.Reflect(true);
                _audioPlayer.PlayOneShot(_perfectSound);
                isPerfect = true;
                return true;
            }else {
                p.Reflect(false);
                _audioPlayer.PlayOneShot(_normalSound);
                isPerfect = false;
                return true;
            }
        }else {
            isPerfect = false;
            return false;
        }
    }
}
