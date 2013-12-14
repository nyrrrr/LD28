using UnityEngine;
using System.Collections;

public class Ground_Manager : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		StartCoroutine (Test ());
	}
	// Update is called once per frame
	void Update ()
	{
		
	}

	public GameObject[] MyGrounds;

	private int MinimumSpaceBetweenGrounds = 16;
	private int MaximumSpaceBetweenGrounds = 24;

	private int BlockSize = 16;
	private int MovementSpeed;

	IEnumerator Test ()
	{
		while (true)
		{
			OrderNextGround();
			yield return new WaitForSeconds(2f);
		}
	}
	int CurrentGround = 0;
	void OrderNextGround ()
	{
		int NextGround;
		if (CurrentGround == MyGrounds.Length - 1)
		{
			NextGround = 0;
		}
		else
		{
			NextGround = CurrentGround + 1;
		}

		MyGrounds [NextGround].transform.position = 
			new Vector3 (
				MyGrounds [CurrentGround].transform.position.x + MyGrounds [CurrentGround].transform.lossyScale.x / 2 + MyGrounds [NextGround].transform.lossyScale.x / 2 + Random.Range (MinimumSpaceBetweenGrounds, MaximumSpaceBetweenGrounds),
				MyGrounds [CurrentGround].transform.position.y + BlockSize * Random.Range (-2, 2),
				0);
		if (CurrentGround == MyGrounds.Length - 1)
		{
			CurrentGround = 0;
		}
		else
		{
			CurrentGround++;
		}
	}
}