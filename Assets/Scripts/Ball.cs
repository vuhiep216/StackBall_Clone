using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class Ball : MonoBehaviour
{
    private enum State
    {
        Start,
        Play,
        Die,
        Finish,
    }

    private State state=State.Start;

    [SerializeField] private float bouncePower;
    [SerializeField] private Gameplay gamePlay;
    [SerializeField] private float speedDown;
    [SerializeField] private Text Score;
    [SerializeField] private Image furyProgressFill;
    [SerializeField] private Image furyProgressFillBackground;
    [SerializeField] private GameObject furyProgress;

    public GameObject ball;
    public GameObject completeLevelUI;
    public GameObject failLevelUI;

    private bool click;
    private bool gameIsP;
    private bool stop=false;
    private bool isFury;

    private int lvs;
    private int point=0;

    private Rigidbody rb;

    private const float speedLimit = 10f;
    private float furyTime;

    private void Awake()
    {
        rb = ball.GetComponent<Rigidbody>();

    }

    private void Start()
    {
        state = State.Start;
        Score.text = ("Score: "+point);
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
        ClickCheck();
        Move();
        FuryCheck();
        RingCheck();
        Score.text = ("Score: "+point);
    }

    private void DisableRigid()
    {
        rb.useGravity = false;
        var constraints = rb.constraints;
        constraints = RigidbodyConstraints.FreezeAll;
        constraints = ~RigidbodyConstraints.FreezePositionY;
        rb.constraints = constraints;
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (click)
        {
            if (col.gameObject.CompareTag("Finish"))
            {
                if (isFury)
                {
                    col.gameObject.tag = "Point";
                }
                if (!isFury)
                {
                    failLevelUI.SetActive(true);
                    stop = true;
                    Debug.Log("Load scene");
                }
            }
            if (col.gameObject.CompareTag("Point"))
            {
                col.gameObject.layer = 3;
            }
        }
        if (!click)
        {
            rb.velocity = new Vector3(0,bouncePower,0);
        }
        if (!col.gameObject.CompareTag("Ground")) return;
        completeLevelUI.SetActive(true);
        stop = true;
    }

    private void ClickCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            click = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            click = false;
        }
    }

    private void Move()
    {
        if (click&&Input.GetMouseButtonDown(0))
        {
            rb.velocity = new Vector3(0,-speedDown,0);
            ball.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
        }
        else
        {
            ball.transform.localScale = new Vector3(1f,1f,1f);
        }
        if (rb.velocity.y > speedLimit)
        {
            rb.velocity = new Vector3(0, speedLimit, 0);
        }
    }
    private void FuryCheck()
    {
        if (isFury)
        {
            furyTime -= Time.deltaTime*1.2f;
        }
        else
        {
            if (click)
                furyTime += Time.deltaTime*1.2f;
            else
                furyTime -= Time.deltaTime*1.2f;
        }

        if (furyTime >= 1f)
        {
            furyTime = 1;
            isFury = true;
            GameObject.FindGameObjectWithTag("Flame").GetComponent<ParticleSystem>().Play();
        }
        else if (furyTime <= 0 )
        {
            furyTime = 0;
            isFury = false;
            GameObject.FindGameObjectWithTag("Flame").GetComponent<ParticleSystem>().Stop();
        }

        if (!furyProgress.activeInHierarchy) return;
        furyProgressFill.fillAmount=furyTime;
        furyProgressFillBackground.fillAmount = furyTime;
    }
    private void RingCheck()
    {
        if (!click) return;
        if (gamePlay.ringList.Count <= 0) return;
        DisableRigid();
        var ring1 = gamePlay.ringList.Last();
        transform.Translate(Vector3.down*speedDown*Time.smoothDeltaTime);
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
        gamePlay.ringList.Remove(gamePlay.ringList.Last());
    }
}
