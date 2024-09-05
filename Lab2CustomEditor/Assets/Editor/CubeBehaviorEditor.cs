using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CubeBehavior)), CanEditMultipleObjects]
public class CubeBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Update the serialized object by finding the property called cubeSize and creating a GUI field for it that will be seen in the inspector.
        // Then, apply the modifications, which also populates an undo operation under the edit drop down near the top left of the editor. 
        serializedObject.Update();
        var cubeSize = serializedObject.FindProperty("cubeSize");
        EditorGUILayout.PropertyField(cubeSize);
        serializedObject.ApplyModifiedProperties();

        // If the cube size inputted by the user is equal to or less than 0, display a warning message.
        if (cubeSize.floatValue <= 0)
        {
            EditorGUILayout.HelpBox("Cube size cannot be 0 or less! At that point does it even exist?", MessageType.Warning);
        }
        // Else, if the cube size is greater than 10, display a warning message that is as wide as the input box.
        else if (cubeSize.floatValue > 10)
        {
            EditorGUILayout.HelpBox("Cube cannot be bigger than 10!", MessageType.Warning, false);
        }

        // This renders the default GUI of the CubeBehavior script. Commenting this out allows us to manually set what the inspector GUI looks like.
        //base.OnInspectorGUI();

        // Use HorizontalScope so the two buttons are placed next to each other and fill out the horizontal space.
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Select all cubes"))
            {
                // Select all cubes button finds objects with the CubeBehavior script,
                // adds each one to an array, then selects all the objects in that array.
                var allCubeBehavior = GameObject.FindObjectsOfType<CubeBehavior>();
                var allCubeObjects = allCubeBehavior.Select(cube => cube.gameObject).ToArray();
                Selection.objects = allCubeObjects;
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
                // Create an undo action in the editor that users can click under edit to undo the diabling/enabling of the cubes.
                Undo.RecordObject(cube.gameObject, "Disable/Enable all cubes");

                // If the button on the cube says disable, set the cubes to not active.
                if (buttonName == "Disable all cubes")
                {
                    cube.gameObject.SetActive(false);
                }
                // If it doesn't say disable (most likely says enable), set the cubes to active.
                else
                {
                    cube.gameObject.SetActive(true);
                }
            }
        }
    }
}
