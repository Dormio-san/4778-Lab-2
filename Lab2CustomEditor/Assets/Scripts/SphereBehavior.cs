using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehavior : MonoBehaviour
{
    [Range(0.5f, 5f)]
    public float radius = 1f;

    void OnValidate()
    {
        // Update the size of the sphere when the radius variable changes
        transform.localScale = new Vector3(radius, radius, radius);
    }
}
