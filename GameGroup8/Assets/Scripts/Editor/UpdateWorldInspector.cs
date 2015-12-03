using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(MapGeneratorScript))]

public class UpdateWorldInspector : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Regenerate"))
        {
            MapGeneratorScript mapGenerator = (MapGeneratorScript)target;
            mapGenerator.BuildMesh();
        }
    }
}
