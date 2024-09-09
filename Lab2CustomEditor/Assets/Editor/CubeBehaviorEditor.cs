using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CubeBehavior)), CanEditMultipleObjects]
public class CubeBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw default inspector properties
        DrawDefaultInspector();

        // Get the CubeBehavior instance
        CubeBehavior cubeBehavior = (CubeBehavior)target;

        // Display warnings based on size constraints
        if (cubeBehavior.size > 2f)
        {
            EditorGUILayout.HelpBox("Warning: The cubes' sizes cannot be bigger than 2!", MessageType.Warning);
        }
        else if (cubeBehavior.size < 0.1f)
        {
            EditorGUILayout.HelpBox("Warning: The cubes' size cannot be smaller than 0.1!", MessageType.Warning);
        }

        // Display size property
        cubeBehavior.size = EditorGUILayout.FloatField("Size", cubeBehavior.size);

        // Use HorizontalScope so the two buttons are placed next to each other and fill out the horizontal space.
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
                Selection.objects = new Object[0];
            }
        }

        // String that allows for changing of the button name.
        string buttonName = "Disable/Enable all cubes";

        // Get a reference to the cube to change the button color and text.
        var cubeRef = GameObject.FindObjectOfType<CubeBehavior>();

        // If the cube can be found (it is not null), make the button red and display disable text.
        if (cubeRef != null)
        {
            GUI.backgroundColor = Color.red;
            buttonName = "Disable all cubes";
        }
        // If the cube cannot be found, set the button color to green and display enable text.
        else
        {
            GUI.backgroundColor = Color.green;
            buttonName = "Enable all cubes";
        }

        if (GUILayout.Button(buttonName, GUILayout.Height(40)))
        {
            foreach (var cube in GameObject.FindObjectsOfType<CubeBehavior>(true))
            {
                Undo.RecordObject(cube.gameObject, "Disable/Enable all cubes");

                if (buttonName == "Disable all cubes")
                {
                    cube.gameObject.SetActive(false);
                }
                else
                {
                    cube.gameObject.SetActive(true);
                }
            }
        }

        // Reset button background color
        GUI.backgroundColor = Color.white;
    }
}
