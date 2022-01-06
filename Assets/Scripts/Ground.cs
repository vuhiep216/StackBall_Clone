using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject grd;
    void Start()
    {
       // grdSpwn(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void grdSpwn(int numCell)
    {
        for(int i = 0; i < numCell; i++)
        {
            GameObject newGrd = Instantiate(grd,new Vector3(0,0,0),Quaternion.identity, transform);
        }
    }
}
