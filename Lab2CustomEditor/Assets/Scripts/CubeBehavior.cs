using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    [SerializeField] private float cubeSize = 1.0f;

    private void Start()
    {
        this.gameObject.transform.localScale = new Vector3 (cubeSize, cubeSize, cubeSize);
    }
}
