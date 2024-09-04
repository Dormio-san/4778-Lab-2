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

        // Change the color of the enable and disable button.
        //var originalColor = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;


        if (GUILayout.Button("Disable/Enable all cubes", GUILayout.Height(40), GUILayout.Width(215)))
        {
            foreach (var cube in GameObject.FindObjectsOfType<CubeBehavior>(true))
            {
                Undo.RecordObject(cube.gameObject, "Disable/Enable all cubes");
                cube.gameObject.SetActive(!cube.gameObject.activeSelf);
            }
        }
    }
}
