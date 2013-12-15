using UnityEngine;
using System.Collections;

public class Movement_Hero : MonoBehaviour
{
    private Vector2[] RayCheckStartPoints = new Vector2[2];
    private bool DaOneIsPerformed = true;
    private int MovementSpeed = 5;
    private int GravitySpeed = -1;
    private int JumpStepHeightMax = 24;
    private int JumpIntervalMultiplicator = 2;
    public bool Grounded = false;
    private bool keyPressed = false;

    private Vector2 MaxMoveMovementDistance;
    private RaycastHit2D RayCastHit;
    private int CurrentGravity = 1;

    private long stepCounter = 0; // for highscore

    private Ground_Manager MyGroundManager;
    private GameObject CurrentGround;
    // Use this for initialization
    void Start()
    {
        MyGroundManager = GameObject.Find("GameManager").GetComponent<Ground_Manager>();
    }
    void Update()
    {
        keyPressed = Input.GetKeyDown(KeyCode.Space);
        if (DaOneIsPerformed && GameManager.Alive)
        {
            if (keyPressed)
            {
                //Debug.Log("UPDATE: PosX: " + (transform.position.x + 8) + " | " + "Grounded: " + Grounded);
                DaOneIsPerformed = false;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Alive)
        {
            if (!DaOneIsPerformed)
            {
                //Debug.Log("JUMPED: PosX: " + (transform.position.x + 8) + " | " + "Grounded: " + Grounded);
                if (Grounded)
                {
                    StartCoroutine(PerformJump());
                }
                DaOneIsPerformed = true;
            }
            for (int i = 0; i < MovementSpeed; i++)
            {
                transform.Translate(1, 0, 0);

                if (stepCounter < 12) stepCounter++;
                else
                {
                    stepCounter = 0;
                    GameManager.Highscore += 0.5;
                }
            }
            PerformGravity();
        }
    }
    IEnumerator PerformJump()
    {
        int CurrentJumpInterval = JumpStepHeightMax;
        while (CurrentJumpInterval > 0)
        {
            transform.Translate(new Vector3(0, CurrentJumpInterval, 0));
            CurrentJumpInterval -= JumpIntervalMultiplicator;
            yield return new WaitForFixedUpdate();
        }
    }

    // edit nyrrrr: this could probably have been made easier if we just added a groundcheck object to the hero and used proper collision detection provided by unity
    void PerformGravity()
    {
        RayCheckStartPoints = new Vector2[2];
        // edit nyrrrr: this might be wrong (TODO ?)
        RayCheckStartPoints[0] = new Vector2(transform.position.x+1, transform.position.y);
        RayCheckStartPoints[1] = new Vector2(transform.position.x + ((BoxCollider2D)transform.collider2D).size.x+1, transform.position.y);
        int PerformDistance = Mathf.Abs(CurrentGravity);
        for (int i = 0; i < RayCheckStartPoints.Length; i++)
        {
            //Debug.DrawLine(RayCheckStartPoints[i], new Vector3(RayCheckStartPoints[i].x, 0, 0));
            RayCastHit = Physics2D.Raycast(RayCheckStartPoints[i], new Vector2(0, -1), Mathf.Abs(CurrentGravity), 1 << 31);
            if (RayCastHit)
            {
                if (Mathf.RoundToInt(Mathf.Abs(RayCheckStartPoints[i].y - RayCastHit.point.y)) == 0)
                {
                    if (transform.position.y < RayCastHit.collider.transform.position.y)
                    {
                        //Debug.Log("DEAD MOFO");
                        Die();
                        return;
                    }
                    CurrentGravity = 1;
                    Grounded = true;
                    return;
                }
                else
                {
                    PerformDistance = Mathf.RoundToInt(Mathf.Abs(RayCheckStartPoints[i].y - RayCastHit.point.y));
                    transform.Translate(0, -PerformDistance, 0);
                    Grounded = true;
                    return;
                }
            }
        }
        Grounded = false;
        transform.Translate(0, -PerformDistance, 0);
        CurrentGravity += GravitySpeed;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Triggered!: " + Grounded);
        if (col.gameObject.layer == 31)
        {
            if (CurrentGround != col.gameObject)
            {
                MyGroundManager.OrderNextGround();
                CurrentGround = col.gameObject;
            }
            if (!Grounded)
            {
                Die();
            }
        }
    }
    void Die()
    {
        GameManager.Alive = false;
    }
}