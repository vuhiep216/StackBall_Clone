using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using Color = UnityEngine.Color;
using Random = System.Random;

public class Rings : MonoBehaviour
{
    [SerializeField] public float spinSpd;

    public List<GameObject> listRing;
    //[SerializeField] private GameObject point;
    private void Update()
    {
        transform.Rotate((new Vector3(0f, spinSpd, 0f) * Time.deltaTime));
    }

    public void Init()
    {
        int random;
        random = UnityEngine.Random.Range(0, listRing.Count);
        listRing[random].GetComponent<Renderer>().material.color=Color.black;
    }
}
