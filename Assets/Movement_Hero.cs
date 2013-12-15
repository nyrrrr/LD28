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
	private bool Alive = true;

	private Ground_Manager MyGroundManager;
	private GameObject CurrentGround;
	// Use this for initialization
	void Start ()
	{
		MyGroundManager = GameObject.Find ("Ground_Manager").GetComponent<Ground_Manager>();
	}
	void Update ()
	{
		if (DaOneIsPerformed)
		{
			if (Input.GetKeyDown (KeyCode.Space))
			{
				Debug.Log("UPDATE: PosX: " + (transform.position.x + 8) + " | " + "Grounded: " + Grounded);
				DaOneIsPerformed = false;
			}
		}
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (Alive)
		{
			if (!DaOneIsPerformed)
			{
				Debug.Log("JUMPED: PosX: " + (transform.position.x + 8) + " | " + "Grounded: " + Grounded);
				if (Grounded)
				{
					StartCoroutine (PerformJump());
				}
				DaOneIsPerformed = true;
			}
			for (int i = 0; i < MovementSpeed; i++)
			{
				transform.Translate (1, 0, 0);
			}
			PerformGravity ();
		}
	}
	IEnumerator PerformJump ()
	{
		int CurrentJumpInterval = JumpStepHeightMax;
		while (CurrentJumpInterval > 0)
		{
			transform.Translate(new Vector3(0, CurrentJumpInterval, 0));
			CurrentJumpInterval -= JumpIntervalMultiplicator;
			yield return new WaitForFixedUpdate ();
		}
	}
	private Vector2 MaxMoveMovementDistance;
	private RaycastHit2D RayCastHit;
	private int CurrentGravity = 1;
	void PerformGravity ()
	{
		RayCheckStartPoints = new Vector2[2];
		RayCheckStartPoints [0] = new Vector2 (transform.position.x + 0, transform.position.y);
		RayCheckStartPoints [1] = new Vector2 (transform.position.x + 16, transform.position.y);
		int PerformDistance = Mathf.Abs(CurrentGravity);
		for (int i = 0; i < RayCheckStartPoints.Length; i++)
		{
			//Debug.DrawLine(RayCheckStartPoints[i], new Vector3(RayCheckStartPoints[i].x, 0, 0));
			RayCastHit = Physics2D.Raycast(RayCheckStartPoints[i], new Vector2(0, -1), Mathf.Abs(CurrentGravity), 1 <<31);
			if (RayCastHit)
			{
				if (Mathf.RoundToInt(Mathf.Abs(RayCheckStartPoints[i].y - RayCastHit.point.y)) == 0)
				{
					if (transform.position.y < RayCastHit.collider.transform.position.y)
					{
						Debug.Log("DEAD MOFO");
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
	void OnTriggerEnter2D (Collider2D col)
	{
		Debug.Log("Triggered!: " + Grounded);
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
		Alive = false;
	}
}