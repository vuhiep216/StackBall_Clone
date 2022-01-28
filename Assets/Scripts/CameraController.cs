using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;

    private float minY;
    private Vector3 offset;
    private bool isFlw;

    private void Start()
    {
        var position = ball.transform.position;
        offset = transform.position - position;
        minY = position.y;

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
        if (ball.transform.position.y > minY)
        {
            isFlw = false;
        }

        if (!isFlw) return;
        var target = ball.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position,target,Time.deltaTime*4.5f);
        transform.LookAt(ball.transform);
    }
}
