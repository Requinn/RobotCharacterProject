using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotate an Object when interacted with
/// </summary>
public class ObjectRotator : Interactable
{
    [SerializeField]
    private Vector3 _rotationToApply;
    [SerializeField]
    private float _rotateSpeed = 2f;
    [SerializeField]
    private GameObject _objectToRotate = null;

    [SerializeField]
    private bool _oneUse = true;
    
    private Quaternion _finalRotation, _startRotation;
    private float progress = 0;
    private bool _isActive = false;

    public override void Interact() {
        if (_objectToRotate ) {
            if (_oneUse) {
                _renderer.enabled = false;
                GetComponent<Collider>().enabled = false;
            }
            _startRotation = _objectToRotate.transform.rotation;
            _finalRotation = Quaternion.Euler(_objectToRotate.transform.rotation.eulerAngles + _rotationToApply);
            _isActive = true;
            //StartCoroutine(RotateObject());
        }    
    }

    public override void Update() {
        base.Update();
        if (_isActive && progress >= 0 && progress < 1) {
            progress += Time.deltaTime * _rotateSpeed;
            _objectToRotate.transform.rotation = Quaternion.Lerp(_startRotation, _finalRotation, progress);
        }
    }
}
