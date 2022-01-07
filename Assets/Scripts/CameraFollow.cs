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

    private void FixedUpdate()
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
            var target = ball.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position,target,Time.deltaTime*4.5f);
            transform.LookAt(ball.transform);
        }
    }
}
