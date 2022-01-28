using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rings : MonoBehaviour
{
    [SerializeField] public float spinSpd;
    [SerializeField] private GameObject point;
    private void Update()
    {
        transform.Rotate((new Vector3(0f, spinSpd, 0f) * Time.deltaTime));
    }

    public void Init()
    {
        point.GetComponent<Renderer>().material.color=Color.black;
        point.tag = "Finish";
    }
}
