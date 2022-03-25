using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainTextureDetector))]
public class TerrainTexturDetectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TerrainTextureDetector terrainTextureDetector = (TerrainTextureDetector) target;
        if (GUILayout.Button("Check at owner's location"))
        {
            terrainTextureDetector.DebugAtOwnerLocation();
        }
    }
}
