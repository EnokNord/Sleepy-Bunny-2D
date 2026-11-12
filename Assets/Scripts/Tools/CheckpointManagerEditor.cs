using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[CustomEditor(typeof(CheckpointManager))]
public class CheckpointManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CheckpointManager myTarget = (CheckpointManager)target;
        
            Checkpoint[] checkpointsInScene = FindObjectsByType<Checkpoint>();
        if (checkpointsInScene != null)
            {
            foreach (Checkpoint checkpoint in checkpointsInScene)
            {
                if (!myTarget.CheckpointList.Contains(checkpoint))
                {
                    myTarget.CheckpointList.Add(checkpoint);
                }
                
            }
            }
       
        var property = serializedObject.FindProperty("CheckpointList");
        EditorGUILayout.PropertyField(property, true);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif