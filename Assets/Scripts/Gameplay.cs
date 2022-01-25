using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
