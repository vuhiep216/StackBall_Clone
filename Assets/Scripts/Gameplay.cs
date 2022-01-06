using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    //public GameObject ring;

    // Start is called before the first frame update
    void Start()
    {
       // spawnRing(20);
    }

    private void Update()
    {


    }

    /**void spawnRing(int ringsNum)
    {
        for (int i = 0; i < ringsNum; i++)
        {

            GameObject newRing = Instantiate(ring, new Vector3(0, i * 1.2f, 0), Quaternion.Euler(0,i*10f,0),transform);


        }
    }**/
}
