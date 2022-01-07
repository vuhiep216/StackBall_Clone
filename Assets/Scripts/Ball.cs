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
    [SerializeField] private Rings rings;

    public GameObject ball;
    public GameObject ring;
    public GameObject completelevelUI;
    public GameObject failLevelUI;

    private bool click;
    private bool gameIsP;
    private bool stop=false;

    private Rigidbody rb;

    // Start is called before the first frame update
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
            if (rings.ringList.Count>0)
            {
                var ring1 = rings.ringList.Last();
                rb.velocity = Vector3.up *-dwnPwr;
                if (ball.transform.position.y < (ring1.transform.position.y+1f))
                {
                    foreach (var rbc in ring1.GetComponentsInChildren<Rigidbody>())
                    {
                        rbc.isKinematic = false;
                        rbc.freezeRotation = true;
                        rbc.AddForce(new Vector3(0,-1,5)*500f);
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
                failLevelUI.SetActive(true);
                stop = true;
                Debug.Log("Load scene");
            }
            if (col.gameObject.CompareTag("Point"))
            {
                Destroy(col.transform.parent.gameObject);
            }

        }
        if (col.gameObject.CompareTag(("Ground")))
        {
            completelevelUI.SetActive(true);
            stop = true;
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
