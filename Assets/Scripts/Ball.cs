using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private float jumpPwr;
    [SerializeField] private Level ring;
    [SerializeField] private float spdDown;
    [SerializeField] private Text Score;

    public GameObject ball;
    public GameObject completeLevelUI;
    public GameObject failLevelUI;

    private bool click;
    private bool gameIsP;
    private bool stop=false;

    private int lvs;
    private int point=0;

    private Rigidbody rb;

    private float startTime;
    private float holdTime;
    private float endTime;


    private void Start()
    {
        GameObject.FindGameObjectWithTag("Flame").GetComponent<ParticleSystem>().enableEmission = false;
        Score.text = ("Score: "+point);
        rb = ball.GetComponent<Rigidbody>();
        completeLevelUI.SetActive(false);
        failLevelUI.SetActive(false);
    }

    private void Update()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        Time.timeScale = 1;
        if (stop)
        {
            Time.timeScale = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            click = true;
            startTime = Time.time;
        }
        holdTime = Time.time - startTime;
        Debug.Log(holdTime);
        if(Input.GetMouseButtonUp(0))
        {
            click = false;
            endTime = Time.time;
        }
        if(holdTime > 0.5f)
        {
            GameObject.FindGameObjectWithTag("Flame").GetComponent<ParticleSystem>().enableEmission = true;
        }
        if (!click) return;
        if (ring.ringList.Count <= 0) return;
        disableRigid();
        var ring1 = ring.ringList.Last();
        transform.Translate(Vector3.down*spdDown*Time.deltaTime);
        if (!(ball.transform.position.y < (ring1.transform.position.y))) return;
        point+=5;
        foreach (var rbc in ring1.GetComponentsInChildren<Rigidbody>())
        {
            ring1.GetComponent<Rings>().spinSpd = 0;
            ring1.transform.parent = GameObject.FindGameObjectWithTag("brkRing").transform;
            rbc.isKinematic = false;
            rbc.velocity = new Vector3(0,1,0)*50f;
            rbc.velocity = new Vector3(0,0,1)*50f;
        }
        ring.ringList.Remove(ring.ringList.Last());
        Score.text = ("Score: "+point);
        holdTime = 0;
    }

    void disableRigid()
    {
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints = ~RigidbodyConstraints.FreezePositionY;
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

            if (col.gameObject.CompareTag("Point"))
            {
                col.gameObject.layer = 3;
            }
        }

        if (!col.gameObject.CompareTag(("Ground"))) return;
        completeLevelUI.SetActive(true);
        stop = true;
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
