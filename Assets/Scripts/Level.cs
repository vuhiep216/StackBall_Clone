using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private GameObject ring;
    public List<GameObject> ringList = new List<GameObject>();
    private int lv=1;

    public void Start()
    {

        ring = Resources.Load<GameObject>("Prefabs/LV"+lv);
        ringSpawn(20);
    }

    void ringSpawn(int ringNum)
    {
        //ringList.Add(ring);
        ring.transform.rotation = Quaternion.Euler(0,0, 0);
        for (int i = 0; i < ringNum; i++)
        {

            GameObject newRing = Instantiate(
                ring,
                new Vector3(0, i * 1.2f, 0),
                Quaternion.Euler(0,(i+1)*8f,0),
                transform);
            ringList.Add(newRing);
        }
    }
}
