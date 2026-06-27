using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Checkpoint))]
public class CheckpointIDCreator : Editor
{
    public override void OnInspectorGUI()
    {
        Checkpoint myTarget = (Checkpoint)target;
        if(myTarget.CheckpointID == 0)
        {
            Checkpoint[] checkpointsInScene = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);
            if (checkpointsInScene != null)
            {
                myTarget.CheckpointID = checkpointsInScene.Length;
                
            }
        }
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.LabelField("Checkpoint ID", myTarget.CheckpointID.ToString());
    }
}
