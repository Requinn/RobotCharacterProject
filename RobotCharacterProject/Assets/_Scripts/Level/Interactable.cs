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
    private Color _selectedColor = new Color(1,0.7f,0,0.8f); //orange
    private float _selectedAlpha = 0.8f, _unselectedAlpha = 0f;
    public bool _isSelected = false;
    private Renderer _renderer;
    private Material _outlineShaderMat;

    public virtual void Start() {
        _renderer = GetComponent<Renderer>();
        _outlineShaderMat = _renderer.material; //whatever the second material is on top of the base material
    }

    /// <summary>
    /// Handles interactions for inherited classes
    /// </summary>
    public virtual void Interact() { Debug.Log("This wasn't implemented!"); }

    /// <summary>
    /// Change the material to indicate that this is the selected object
    /// </summary>
    public void ToggleHighlight() {
        Debug.Log("Toggling");
        _isSelected = true;
        _selectedColor.a = _selectedAlpha;
    }

    private void Update() {
        if (!_renderer) { return; }
        _outlineShaderMat.SetColor("_OutlineColor", _selectedColor);
        //reset every frame in case we look away
        if (!_isSelected) {
            _selectedColor.a = _unselectedAlpha;
        }
        _isSelected = false;
    }
}
