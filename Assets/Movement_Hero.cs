using UnityEngine;
using System.Collections;

public class Movement_Hero : MonoBehaviour
{
	private Vector2[] RayCheckStartPoints = new Vector2[2];
	private bool DaOneIsPerformed = true;
	private int MovementSpeed = 5;
	private int GravitySpeed = -1;
	private int JumpStepHeightMax = 16;
	private int JumpIntervalMultiplicator = 2;
	private bool Grounded = false;
	// Use this for initialization
	void Start ()
	{
	}
	void Update ()
	{
		if (DaOneIsPerformed && Grounded)
		{
			if (Input.GetKeyDown (KeyCode.Space))
			{
				DaOneIsPerformed = false;
			}
		}
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (!DaOneIsPerformed)
		{
			StartCoroutine (PerformJump());
			DaOneIsPerformed = true;
		}
		transform.Translate (MovementSpeed, 0, 0);
		PerformGravity ();
	}
	IEnumerator PerformJump ()
	{
		Grounded = false;
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
	private int CurrentGravity = 0;
	void PerformGravity ()
	{
		RayCheckStartPoints [0] = new Vector2 (transform.position.x , transform.position.y);
		RayCheckStartPoints [1] = new Vector2 (transform.position.x + 16, transform.position.y);
		int PerformDistance = Mathf.Abs(CurrentGravity);
		for (int i = 0; i < RayCheckStartPoints.Length; i++)
		{
			RayCastHit = Physics2D.Raycast(RayCheckStartPoints[i], new Vector2(0, -1), Mathf.Abs(CurrentGravity), 1 <<31);
			if (RayCastHit)
			{
				if (Mathf.RoundToInt(Mathf.Abs(RayCheckStartPoints[i].y) - RayCastHit.point.y) == 0)
				{
					CurrentGravity = 0;
					Grounded = true;
					return;
				}
				else
				{
					if (PerformDistance > Mathf.RoundToInt(Mathf.Abs(RayCheckStartPoints[i].y) - RayCastHit.point.y))
					{
						PerformDistance = Mathf.RoundToInt(Mathf.Abs(RayCheckStartPoints[i].y) - RayCastHit.point.y);
					}
				}
			}
		}
		Grounded = false;
		transform.Translate(0, -PerformDistance, 0);
		CurrentGravity += GravitySpeed;
		Debug.Log (PerformDistance);
	}
}
