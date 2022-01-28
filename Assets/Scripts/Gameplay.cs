using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private Text lvThen;
    private GameObject ring;
    private GameObject core;
    public List<GameObject> ringList = new List<GameObject>();
    public int lv=1;

    private void Awake()
    {
        lvThen.text = ("Level "+PlayerPrefs.GetInt("Level"));
    }

    public void Start()
    {
        LoadLevel();
    }

    private void RingSpawn(int ringNum)
    {
        for (var i = 0; i < ringNum; i++)
        {
            var newRing = Instantiate(
                ring,
                new Vector3(0, i+0.05f, 0),
                Quaternion.Euler(0,(i+1)*8f,0),
                transform);
            newRing.GetComponent<Rings>().Init();
            ringList.Add(newRing);
        }

        var difficult = PlayerPrefs.GetInt("Level");
        switch (difficult)
        {
            case 1:
                //Test Fury
            break;
            case 2:
                ChallengeChange();
                break;
            case 3:
                ChallengeChange();
                break;
            case 4:
                ChallengeChange();
                break;
        }
    }

    public void LoadNextScene()
    {
        var nlv = lv+1;
        PlayerPrefs.SetInt("Level",nlv);
        SceneManager.LoadScene("GamePlay");
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadLevel()
    {
        for (var i = 0 ;i < 50;i++)
        {
            core = Resources.Load<GameObject>("Prefabs/Cores");
            var newCore = Instantiate(
                core,
                new Vector3(0, i, 0),
                Quaternion.identity,
                transform);
        }
        var check = PlayerPrefs.GetInt("Level");
        if (check ==0)
        {
            PlayerPrefs.SetInt("Level",1);
        }
        lvThen.text = ("Level "+PlayerPrefs.GetInt("Level"));
        lv=PlayerPrefs.GetInt("Level");
        Debug.Log("Level:"+lv);
        if (lv > 4) lv = 4;
        ring = Resources.Load<GameObject>("Prefabs/LV"+lv);
        RingSpawn(40);
    }
    
    private void ChallengeChange()
    {
        for (var i = 10; i < 30; i++)
        {
            var colorChange = GameObject.FindGameObjectWithTag("Point").GetComponent<MeshRenderer>();
            var nameChange = GameObject.FindGameObjectsWithTag("Point");
            colorChange.material.color = Color.black;
            //nameChange[].tag = "";

        }

    }
}
