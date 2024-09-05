using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SphereBehavior)), CanEditMultipleObjects]
public class SphereBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Use HorizontalScope so the two buttons are placed next to each other and fill out the horizontal space.
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Select all spheres"))
            {
                // Select all spheres button finds objects with the SphereBehavior script,
                // adds each one to an array, then selects all the objects in that array.
                var allSphereBehavior = GameObject.FindObjectsOfType<SphereBehavior>();
                var allSphereObjects = allSphereBehavior.Select(sphere => sphere.gameObject).ToArray();
                Selection.objects = allSphereObjects;
            }

            if (GUILayout.Button("Clear selection"))
            {
                // Clear selection button takes the current selection and sets it to null, removing all selections.
                Selection.objects = null;

                // Clear selection code provided in tutorial. I think the null that I have is better as it ensures 0 objects are selected.
                //Selection.objects = new Object[] { (target as CubeBehavior).gameObject };
            }
        }
        
        // String that allows for changing of the buttom name.
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
                // Create an undo action in the editor that users can click under edit to undo the diabling/enabling of the spheres.
                Undo.RecordObject(sphere.gameObject, "Disable/Enable all spheres");

                // If the button on the sphere says disable, set the spheres to not active.
                if (buttonName == "Disable all spheres")
                {
                    sphere.gameObject.SetActive(false);
                }
                // If it doesn't say disable (most likely says enable), set the spheres to active.
                else
                {
                    sphere.gameObject.SetActive(true);
                }
            }
        }
    }
}
