using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private float jumpPwr;
    [SerializeField] private float dwnPwr;
    [SerializeField] public Level ring;
    [SerializeField] private float spdDown;

    public GameObject ball;
    public GameObject completelevelUI;
    public GameObject failLevelUI;

    private bool click;
    private bool gameIsP;
    private bool stop=false;

    private int lvs;

    private Rigidbody rb;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        completelevelUI.SetActive(false);
        failLevelUI.SetActive(false);
    }

    private void Update()
    {
        Time.timeScale = 1;
        if (stop)
        {
            Time.timeScale = 0;
        }
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
           rb.AddForce(Vector3.down*spdDown);
            if (ring.ringList.Count>0)
            {
                //disableRigid();
                var ring1 = ring.ringList.Last();
                rb.velocity = Vector3.up *-dwnPwr;
                var rbb = GameObject.FindGameObjectWithTag("Ring").GetComponent<Rigidbody>();
                if (ball.transform.position.y < (ring1.transform.position.y+1f))
                {
                    foreach (var rbc in ring1.GetComponentsInChildren<Rigidbody>())
                    {
                        ring1.GetComponent<Rings>().spinSpd = 0;
                        rbc.isKinematic = false;
                        rbc.AddForce(new Vector3(0,1,0)*500f);
                        rbc.AddForce(new Vector3(0,0,10)*500f);
                        ring1.transform.parent = GameObject.FindGameObjectWithTag("brkRing").transform;
                    }
                    ring.ringList.Remove(ring.ringList.Last());

                }
            }

        }
    }

    void disableRigid()
    {
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.isKinematic = true;
    }
    private void OnCollisionEnter(Collision col)
    {
        rb.velocity =Vector3.up *jumpPwr;
        //var parent = GameObject.FindGameObjectWithTag("Ring");
        if (click)
        {
            if (col.gameObject.CompareTag("Finish"))
            {
                failLevelUI.SetActive(true);
                stop = true;
                Debug.Log("Load scene");
            }


        }
        if (col.gameObject.CompareTag(("Ground")))
        {
            completelevelUI.SetActive(true);
            stop = true;
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        gameIsP = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameIsP = false;
    }
}
