using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CubeBehavior)), CanEditMultipleObjects]
public class CubeBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Select all cubes"))
            {
                var allCubeBehavior = GameObject.FindObjectsOfType<CubeBehavior>();
                var allCubeObjects = allCubeBehavior.Select(cube => cube.gameObject).ToArray();
                Selection.objects = allCubeObjects;
            }

            if (GUILayout.Button("Clear selection"))
            {
                Selection.objects = new Object[] { (target as CubeBehavior).gameObject };
            }
        }
    }
}
