using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Member;
[CustomEditor(typeof(ToolTipController))]
public class TooltipEditor : Editor
{
    string[] customVariables = { "keyboardText", "controllerText", "keyboardTextAsImage", "controllerTextAsImage" };
    SerializedProperty[] variables = new SerializedProperty[4];
    Sprite keyboardImage;
    Sprite controllerImage;
    string keyboardText;
    string controllerText;
    void OnEnable()
    {
        // Setup the SerializedProperties
        for (int i = 0; i < customVariables.Length; i++)
        {
            variables[i] = serializedObject.FindProperty(customVariables[i]);
        }
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ToolTipController myTarget = (ToolTipController)target;
        DrawPropertiesExcluding(serializedObject, customVariables);

        if (myTarget.UsingImages)
        {
            keyboardImage = variables[2].objectReferenceValue as Sprite;
            keyboardImage = EditorGUILayout.ObjectField(keyboardImage, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Sprite;
            variables[2].objectReferenceValue = keyboardImage;

            controllerImage = variables[3].objectReferenceValue as Sprite;
            controllerImage = EditorGUILayout.ObjectField(controllerImage, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Sprite;
            variables[3].objectReferenceValue = controllerImage;
        }
        else
        {
            keyboardText = variables[0].stringValue;
            keyboardText = EditorGUILayout.TextField(keyboardText);
            variables[0].stringValue = keyboardText;
           
            controllerText = variables[1].stringValue;
            controllerText = EditorGUILayout.TextField(controllerText);
            variables[1].stringValue = controllerText;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
