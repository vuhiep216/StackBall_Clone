using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] public float spinSpd = -80f;

    private void Start()
    {

    }

    private void Update()
    {
        transform.Rotate((new Vector3(0f, spinSpd, 0f) * Time.deltaTime));

    }


}
