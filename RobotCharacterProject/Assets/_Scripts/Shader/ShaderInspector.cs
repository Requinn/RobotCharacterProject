using UnityEditor;
using UnityEngine;

/// <summary>
/// custom shader inspector to show only the outline thickness and color and the main color for the shader
/// </summary>
public class ShaderInspector : ShaderGUI
{
    MaterialEditor editor;
    MaterialProperty[] properties;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties) {
        //base.OnGUI(materialEditor, properties);

        editor = materialEditor;
        this.properties = properties;

        //acquire properties
        MaterialProperty _mainColor = FindProperty("_Color", properties);
        MaterialProperty _outlineColor = FindProperty("_OutlineColor", properties);
        MaterialProperty _outlineThickness = FindProperty("_Outline", properties);

        //put in the ditor
        materialEditor.ShaderProperty(_mainColor, "Main Color");
        materialEditor.ShaderProperty(_outlineColor, "Outline Color");
        materialEditor.ShaderProperty(_outlineThickness, "Outline Thickness");
    }
}
