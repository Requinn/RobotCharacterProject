using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Class to help get color information from enum
/// </summary>
public class ColorCode : MonoBehaviour
{
    public static ColorCode ColorCoder;

    public enum ColorType {
        orange, blue, purple, black
    }
    
    private Color _orange = new Color(1f, 0.7f, 0f, 1f),
        _blue = new Color(0f, 1f, 1f, 1f),
        _purple = new Color(0.89f, 0.0f, 1.0f, 1f);


    public void Start() {
        ColorCoder = this;
    }

    /// <summary>
    /// Decode enum to color value
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public Color GetColor(ColorType c) {
        switch (c) {
            case ColorType.blue:
                return _blue;
            case ColorType.orange:
                return _orange;
            case ColorType.purple:
                return _purple;
            case ColorType.black:
                return Color.grey;
            default:
                return Color.grey;
        }
    }
}
