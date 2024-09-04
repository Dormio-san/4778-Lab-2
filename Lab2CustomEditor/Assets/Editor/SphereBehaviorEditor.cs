using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SphereBehavior)), CanEditMultipleObjects]
public class SphereBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

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
                Selection.objects = new Object[] { (target as SphereBehavior).gameObject };
            }
        }

        // Change the color of the enable and disable button.
        //var originalColor = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;
        

        if (GUILayout.Button("Disable/Enable all spheres", GUILayout.Height(40), GUILayout.Width(215)))
        {
            foreach (var sphere in GameObject.FindObjectsOfType<SphereBehavior>(true))
            {
                Undo.RecordObject(sphere.gameObject, "Disable/Enable all spheres");
                sphere.gameObject.SetActive(!sphere.gameObject.activeSelf);
            }
        }
    }
}
