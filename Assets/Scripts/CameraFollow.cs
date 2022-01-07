using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class CameraFollow : MonoBehaviour
{
    public GameObject ball;
    private float minY;
    private Vector3 offset;
    private bool isFlw;

    void Start()
    {
        offset = transform.position - ball.transform.position;
        minY = ball.transform.position.y;
    }

    private void Update()
    {
        if (minY > ball.transform.position.y)
        {
            minY = ball.transform.position.y;
        }
    }

    private void LateUpdate()
    {
        isFlw = true;
        if (ball.transform.position.y < minY)
        {
            isFlw = true;
        }
        if (ball.transform.position.y > minY)
        {
            isFlw = false;
        }
        if (isFlw)
        {
            transform.position = ball.transform.position + offset;
        }
    }
}
