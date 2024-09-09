using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SphereBehavior)), CanEditMultipleObjects]
public class SphereBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw default inspector properties
        DrawDefaultInspector();

        // Get the SphereBehavior instance
        SphereBehavior sphereBehavior = (SphereBehavior)target;

        // Display warnings based on radius constraints
        if (sphereBehavior.radius > 2f)
        {
            EditorGUILayout.HelpBox("Warning: The spheres' radius cannot be bigger than 2!", MessageType.Warning);
        }
        else if (sphereBehavior.radius < 0.5f)
        {
            EditorGUILayout.HelpBox("Warning: The spheres' radius cannot be smaller than 0.5!", MessageType.Warning);
        }

        // Display radius property
        sphereBehavior.radius = EditorGUILayout.FloatField("Radius", sphereBehavior.radius);

        // Use HorizontalScope so the two buttons are placed next to each other and fill out the horizontal space.
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Select all spheres"))
            {
                var allSphereBehavior = GameObject.FindObjectsOfType<SphereBehavior>();
                var allSphereObjects = allSphereBehavior.Select(sphere => sphere.gameObject).ToArray();
                Selection.objects = allSphereObjects;
            }

            if (GUILayout.Button("Clear selection"))
            {
                Selection.objects = new Object[0];
            }
        }
        
        // String that allows for changing of the button name.
        string buttonName = "Disable/Enable all spheres";

        // Get a reference to the sphere to change the button color and text.
        var sphereRef = GameObject.FindObjectOfType<SphereBehavior>();

        // If the sphere can be found (it is not null), make the button red and display disable text.
        if (sphereRef != null)
        {
            GUI.backgroundColor = Color.red;
            buttonName = "Disable all spheres";
        }
        // If the sphere cannot be found, set the button color to green and display enable text.
        else
        {
            GUI.backgroundColor = Color.green;
            buttonName = "Enable all spheres";
        }

        if (GUILayout.Button(buttonName, GUILayout.Height(40)))
        {
            foreach (var sphere in GameObject.FindObjectsOfType<SphereBehavior>(true))
            {
                Undo.RecordObject(sphere.gameObject, "Disable/Enable all spheres");

                if (buttonName == "Disable all spheres")
                {
                    sphere.gameObject.SetActive(false);
                }
                else
                {
                    sphere.gameObject.SetActive(true);
                }
            }
        }

        // Reset button background color
        GUI.backgroundColor = Color.white;
    }
}