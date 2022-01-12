using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject grd;

    void grdSpwn(int numCell)
    {
        for(int i = 0; i < numCell; i++)
        {
            GameObject newGrd = Instantiate(grd,new Vector3(0,0,0),Quaternion.identity, transform);
        }
    }
}
