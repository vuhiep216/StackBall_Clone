using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private Text lvThen;
    private GameObject _ring;
    private GameObject _core;
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
                _ring,
                new Vector3(0, i+0.05f, 0),
                Quaternion.Euler(0,(i+1)*8f,0),
                transform);
            ringList.Add(newRing);
        }

        var difficult = PlayerPrefs.GetInt("Level");
        switch (difficult)
        {
            case 1:
                //Test Fury
            break;
            case 2:
                ChallengeChange1();
                break;
            case 3:
                ChallengeChange2();
                break;
            case 4:
                ChallengeChange2();
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
            _core = Resources.Load<GameObject>("Prefabs/Cores");
            var newCore = Instantiate(
                _core,
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
        _ring = Resources.Load<GameObject>("Prefabs/LV"+lv);
        RingSpawn(40);
    }

    private void ChallengeChange1()
    {
        for (var i = 10; i < 30; i++)
        {
            var no = UnityEngine.Random.Range(0f, 360f);
            if (i % 2 == 0)
            {
                ringList[i].transform.Rotate(0,60f,0);
            }
            else
            {
                ringList[i].transform.Rotate(0,90f,0);
            }
        }
    }

    private void ChallengeChange2()
    {
        for (var i = 10; i < 30; i++)
        {
            var no = UnityEngine.Random.Range(0f, 360f);
            var becomeGood = UnityEngine.Random.Range(10f, 30f);
            if (i % 2 == 0)
            {
                ringList[i].transform.Rotate(0,no,0);
            }
            else
            {
                ringList[i].transform.Rotate(0,no*2f,0);
            }
        }

    }
}
