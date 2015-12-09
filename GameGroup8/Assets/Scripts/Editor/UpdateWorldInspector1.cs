using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGeneratorScriptV1))]

public class UpdateWorldInspector1 : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Regenerate"))
        {
            MapGeneratorScriptV1 mapGenerator = (MapGeneratorScriptV1)target;
            mapGenerator.BuildMesh();
        }
    }
}
