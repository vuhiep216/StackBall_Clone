using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rings : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject ring;
    public Ring rings;
    public List<GameObject> ringList = new List<GameObject>();
    void Start()
    {
        ringSpawn(20);
        rings = Resources.Load<Ring>("Level");
    }

    // Update is called once per frame
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
