using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private GameObject ring;
    public List<GameObject> ringList = new List<GameObject>();
    public int lv=1;


    public void Start()
    {
        var check = PlayerPrefs.GetInt("Level");
        if (check ==0)
        {
            PlayerPrefs.SetInt("Level",1);
        }
        lv=PlayerPrefs.GetInt("Level");
            Debug.Log(lv);
            if (lv > 2) lv = 2;
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

}
