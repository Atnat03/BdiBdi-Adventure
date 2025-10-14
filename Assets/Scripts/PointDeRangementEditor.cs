using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PointDeRangement))]
public class PointDeRangementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PointDeRangement script = (PointDeRangement)target;

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemCanBePutHere"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("canPutAllItem"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rangementType"));

        if (script != null && script.GetType().GetField("rangementType", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(script).Equals(RangementType.Placer) == true)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("emplacementDeRangement"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}