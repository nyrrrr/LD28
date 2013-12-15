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

	private int MinimumGroundWith = 160;
	private int MaximumGroundWith = 240;

	private int BlockSize = 16;
	private int MovementSpeed;

	IEnumerator Test ()
	{
		while (true)
		{
			//OrderNextGround();
			yield return new WaitForSeconds(1f);
		}
	}
	int CurrentGround = 0;
	public void OrderNextGround ()
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
		int groundWidth = MinimumGroundWith + Random.Range (0, (MaximumGroundWith - MinimumGroundWith) / BlockSize + 1) * BlockSize;
		MyGrounds [NextGround].transform.FindChild ("CornerTopRight").transform.localPosition = new Vector3 (groundWidth - BlockSize / 2, MyGrounds [NextGround].transform.FindChild ("CornerTopRight").transform.localPosition.y, 0);

		MyGrounds [NextGround].transform.FindChild ("SideRight").transform.localPosition = new Vector3 (groundWidth - BlockSize / 2, MyGrounds [NextGround].transform.FindChild ("SideRight").transform.localPosition.y, 0);

		MyGrounds [NextGround].transform.FindChild ("InnerCore").transform.localPosition 			= new Vector3 (groundWidth / 2, MyGrounds [NextGround].transform.FindChild ("InnerCore").transform.localPosition.y, 0);
		MyGrounds [NextGround].transform.FindChild ("InnerCore").transform.localScale 				= new Vector3 (BlockSize * (groundWidth / BlockSize - 2), MyGrounds [NextGround].transform.FindChild ("InnerCore").transform.localScale.y, 1);
		MyGrounds [NextGround].transform.FindChild ("InnerCore").renderer.material.mainTextureScale	= new Vector2 ((groundWidth / BlockSize - 2), MyGrounds [NextGround].transform.FindChild ("InnerCore").renderer.material.mainTextureScale.y);

		MyGrounds [NextGround].transform.FindChild ("TopGround").transform.localPosition 			= new Vector3 (groundWidth / 2, MyGrounds [NextGround].transform.FindChild ("TopGround").transform.localPosition.y, 0);
		MyGrounds [NextGround].transform.FindChild ("TopGround").transform.localScale 				= new Vector3 (BlockSize * (groundWidth / BlockSize - 2), MyGrounds [NextGround].transform.FindChild ("TopGround").transform.localScale.y, 1);
		MyGrounds [NextGround].transform.FindChild ("TopGround").renderer.material.mainTextureScale	= new Vector2 ((groundWidth / BlockSize - 2), MyGrounds [NextGround].transform.FindChild ("TopGround").renderer.material.mainTextureScale.y);

		MyGrounds [NextGround].GetComponent<BoxCollider2D> ().size 		= new Vector2 (groundWidth, MyGrounds [NextGround].GetComponent<BoxCollider2D> ().size.y);
		MyGrounds [NextGround].GetComponent<BoxCollider2D> ().center 	= new Vector2 (groundWidth / 2, MyGrounds [NextGround].GetComponent<BoxCollider2D> ().center.y);

		int RandomInt = Random.Range (-1, 2);
		if (RandomInt == -1)
		{
			RandomInt = Random.Range(-2, 0);
		}

		MyGrounds [NextGround].transform.position = 
			new Vector3 (
				MyGrounds [CurrentGround].transform.position.x + MyGrounds [CurrentGround].transform.Find("InnerCore").transform.localPosition.x * 2+ Random.Range (MinimumSpaceBetweenGrounds, MaximumSpaceBetweenGrounds),
				MyGrounds [CurrentGround].transform.position.y + BlockSize * RandomInt,
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