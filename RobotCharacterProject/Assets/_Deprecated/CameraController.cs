using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Character _character;
    private bool _isInZPlane = false;
    [SerializeField]
    private float _cameraDistance = 12f, _cameraHeight = 1.75f;
    // Start is called before the first frame update
    void Start()
    {
        _character = GameController.Instance.GetPlayerReference();
        //_character.OnRotation += RotateCamera;    
    }

    /// <summary>
    /// Apply rotation to the camera to match the player
    /// </summary>
    /// <param name="isZplane"></param>
    private void RotateCamera(bool isZplane) {
        Vector3 rotationToMake = Vector3.zero;
        //if in Z plane, rotate -90 around the player as axis
        if (isZplane) {
            rotationToMake.y = -90f;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else {
            rotationToMake.y = 90f;
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }

        //bring camera point in, rotate, then put it back out the original distance
        transform.position = Quaternion.Euler(rotationToMake) * (transform.position - _character.transform.position) + _character.transform.position;
        _isInZPlane = isZplane;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera's movement depents on which plane we are in
        if (!_isInZPlane) {
            transform.position = new Vector3(_character.transform.position.x + _cameraDistance, _character.transform.position.y + _cameraHeight, _character.transform.position.z);
        }
        else {
            transform.position = new Vector3(_character.transform.position.x, _character.transform.position.y + _cameraHeight, _character.transform.position.z + _cameraDistance);
        }
    }
}
