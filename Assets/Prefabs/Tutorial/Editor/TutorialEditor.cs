using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tutorial))]

public class TutorialEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Tutorial t = target as Tutorial;
        
        if (GUILayout.Button("Next waypoint"))
        {
            t.NextWaypoint();
        }
        
        if (GUILayout.Button("Previous waypoint"))
        {
            t.PreviousWaypoint();
        }
        
        if (GUILayout.Button("Repeat waypoint"))
        {
            t.RepeatWaypoint();
        }
        
    }
}


