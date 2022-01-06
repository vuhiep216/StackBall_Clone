using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    [SerializeField] private float jumpPwr;
    [SerializeField] private float dwnPwr;

    [SerializeField] private Rings rings;
    public GameObject ball;
    public GameObject ring;
    private bool click;


    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            click = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            click = false;
        }

        if (click)
        {
            if (rings.ringList.Count>0)
            {
                var ring1 = rings.ringList.Last();

                rb.velocity = Vector3.up *-dwnPwr;
                if (ball.transform.position.y < (ring1.transform.position.y+1f))
                {
                    foreach (var rbc in ring1.GetComponentsInChildren<Rigidbody>())
                    {
                        rbc.isKinematic = false;
                        rbc.AddForce(new Vector3(1,-1,1)*10000f);

                    }
                    rings.ringList.Remove(rings.ringList.Last());

                }
            }

        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        rb.velocity =Vector3.up *jumpPwr;
        //var parent = GameObject.FindGameObjectWithTag("Ring");
        if (click)
        {
            if (col.gameObject.CompareTag("Finish"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("Load scene");
            }

           else if (col.gameObject.CompareTag("Point"))
            {
                Destroy(col.transform.parent.gameObject);


            }

            else if (col.gameObject.CompareTag(("Ground")))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

    }
}
