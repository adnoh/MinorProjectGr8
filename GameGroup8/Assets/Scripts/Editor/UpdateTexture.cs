using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorldBuilderII))]

public class UpdateWorldBuilderII : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Regenerate"))
        {
            WorldBuilderII mapGenerator = (WorldBuilderII)target;
            mapGenerator.BuildTexture();
        }
    }
}
