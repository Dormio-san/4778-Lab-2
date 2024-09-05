using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehavior : MonoBehaviour
{
    [SerializeField] private float sphereSize = 1.0f;

    private void Start()
    {
        this.gameObject.transform.localScale = new Vector3(sphereSize, sphereSize, sphereSize);
    }
}
