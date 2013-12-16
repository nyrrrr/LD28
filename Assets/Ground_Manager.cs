using UnityEngine;
using System.Collections;

public class Ground_Manager : MonoBehaviour
{

    private static Ground_Manager _selfRef;
    void Awake()
    {
        _selfRef = this;
    }

    public GameObject[] MyGrounds;

    public static GameObject[] MyGroundsProp
    {
        get { return _selfRef.MyGrounds; }
        set { _selfRef.MyGrounds = value; }
    }

    private int minimumSpaceBetweenGrounds = 55;
    private int maximumSpaceBetweenGrounds = 80;

    private int MinimumGroundWith = 48;
    private int MaximumGroundWith = 240;

    private int BlockSize = 16;
    private int MovementSpeed;

    public int currentGround = 0;

    public static int CurrentGround
    {
        get { return _selfRef.currentGround; }
        set { _selfRef.currentGround = value; }
    }
    public int nextGround;

    public static int NextGround
    {
        get { return _selfRef.nextGround; }
        set { _selfRef.nextGround = value; }
    }
    public void OrderNextGround()
    {
        if (currentGround == MyGrounds.Length - 1)
        {
            nextGround = 0;
        }
        else
        {
            nextGround = currentGround + 1;
        }
        int groundWidth = MinimumGroundWith + Random.Range(0, (MaximumGroundWith - MinimumGroundWith) / BlockSize + 1) * BlockSize;
        MyGrounds[nextGround].transform.FindChild("CornerTopRight").transform.localPosition = new Vector3(groundWidth - BlockSize / 2, MyGrounds[nextGround].transform.FindChild("CornerTopRight").transform.localPosition.y, 0);

        MyGrounds[nextGround].transform.FindChild("SideRight").transform.localPosition = new Vector3(groundWidth - BlockSize / 2, MyGrounds[nextGround].transform.FindChild("SideRight").transform.localPosition.y, 0);

        MyGrounds[nextGround].transform.FindChild("InnerCore").transform.localPosition = new Vector3(groundWidth / 2, MyGrounds[nextGround].transform.FindChild("InnerCore").transform.localPosition.y, 0);
        MyGrounds[nextGround].transform.FindChild("InnerCore").transform.localScale = new Vector3(BlockSize * (groundWidth / BlockSize - 2), MyGrounds[nextGround].transform.FindChild("InnerCore").transform.localScale.y, 1);
        MyGrounds[nextGround].transform.FindChild("InnerCore").renderer.material.mainTextureScale = new Vector2((groundWidth / BlockSize - 2), MyGrounds[nextGround].transform.FindChild("InnerCore").renderer.material.mainTextureScale.y);

        MyGrounds[nextGround].transform.FindChild("TopGround").transform.localPosition = new Vector3(groundWidth / 2, MyGrounds[nextGround].transform.FindChild("TopGround").transform.localPosition.y, 0);
        MyGrounds[nextGround].transform.FindChild("TopGround").transform.localScale = new Vector3(BlockSize * (groundWidth / BlockSize - 2), MyGrounds[nextGround].transform.FindChild("TopGround").transform.localScale.y, 1);
        MyGrounds[nextGround].transform.FindChild("TopGround").renderer.material.mainTextureScale = new Vector2((groundWidth / BlockSize - 2), MyGrounds[nextGround].transform.FindChild("TopGround").renderer.material.mainTextureScale.y);

        MyGrounds[nextGround].GetComponent<BoxCollider2D>().size = new Vector2(groundWidth, MyGrounds[nextGround].GetComponent<BoxCollider2D>().size.y);
        MyGrounds[nextGround].GetComponent<BoxCollider2D>().center = new Vector2(groundWidth / 2, MyGrounds[nextGround].GetComponent<BoxCollider2D>().center.y);

        int RandomInt = Random.Range(-1, 2);
        if (RandomInt == -1)
        {
            RandomInt = Random.Range(-2, 0);
        }

        MyGrounds[nextGround].transform.position =
            new Vector3(
                MyGrounds[currentGround].transform.position.x + MyGrounds[currentGround].transform.Find("InnerCore").transform.localPosition.x * 2 + Random.Range(minimumSpaceBetweenGrounds, maximumSpaceBetweenGrounds),
                MyGrounds[currentGround].transform.position.y + BlockSize * RandomInt,
                0);
        if (currentGround == MyGrounds.Length - 1)
        {
            currentGround = 0;
        }
        else
        {
            currentGround++;
        }
    }
}