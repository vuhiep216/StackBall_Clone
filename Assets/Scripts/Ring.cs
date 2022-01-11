using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private GameObject ring;
    public List<GameObject> ringList=new List<GameObject>();

    void Start()
    {
        ring = Resources.Load("Prefabs/LV1") as GameObject;
        ring.transform.parent = GameObject.FindGameObjectWithTag("Rings").transform;
    }
    void Update()
    {

    }

    void ringSpawn(int ringNum)
    {
        ringList.Add(ring);
        ring.transform.rotation = Quaternion.Euler(0,0, 0);
        for (int i = 0; i < ringNum; i++)
        {

            GameObject newRing = Instantiate(
                ring,
                new Vector3(0, (i+1) * 1.2f, 0),
                Quaternion.Euler(0,(i+1)*8f,0),
                transform);
            ringList.Add(newRing);
        }
    }
}
