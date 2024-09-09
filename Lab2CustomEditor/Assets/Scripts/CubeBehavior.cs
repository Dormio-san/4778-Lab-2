using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    [Range(0.1f, 5f)]
    public float size = 1f;

    void OnValidate()
    {
        // Update the size of the cube when the size variable changes
        transform.localScale = new Vector3(size, size, size);
    }
}