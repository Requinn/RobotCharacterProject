using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for objects that can be interacted with
/// </summary>
public class Interactable : MonoBehaviour
{
    [Header("Base Interactable Properties")]
    [SerializeField]
    private Material _unSelected, _selected;
    private bool _isSelected = false;
    private Renderer _renderer;

    private void Start() {
        _renderer = GetComponent<Renderer>();
    }

    /// <summary>
    /// Handles interactions for inherited classes
    /// </summary>
    public virtual void Interact() { Debug.Log("This wasn't implemented!"); }

    /// <summary>
    /// Change the material to indicate that this is the selected object
    /// </summary>
    public void ToggleHighlight() {
        _isSelected = true;
    }

    private void Update() {
        if (!_renderer) { return; }
        //if we are selected and our material is still unselected, change it
        if (_isSelected && _renderer.material == _unSelected) {
            _renderer.material = _selected;
        }
        else if(!_isSelected && _renderer == _selected) {
            _renderer.material = _unSelected;
        }
        //reset every frame in case we look away
        _isSelected = false;
    }
}
