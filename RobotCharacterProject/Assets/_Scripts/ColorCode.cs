using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType {
    orange, blue, purple, black
}

/// <summary>
/// Holds color information to assign to the shader
/// </summary>
public struct ColorCode
{
    public ColorType colorType;
    public Color shaderColor;

    public ColorCode(ColorType c) {
        colorType = c;
        shaderColor = Color.grey;

        switch (colorType) {
            case ColorType.blue:
                shaderColor = new Color(0.89f, 1.0f, 0f, 1f);
                break;
            case ColorType.orange:
                shaderColor = new Color(1.0f, 0.5f, 0f, 1f);
                break;
            case ColorType.purple:
                shaderColor = new Color(0.89f, 0.0f, 1.0f, 1f);
                break;
            case ColorType.black:
                shaderColor = Color.grey;
                break;
            default:
                shaderColor = Color.grey;
                break;
        }
    }
}
