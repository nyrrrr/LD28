using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

    Vector2 newPos;
    Transform hero;
    private const int HERO_OFFSET_X = 100;
    private const int HERO_OFFSET_Y = 40;

    void Awake()
    {
        hero = GameObject.Find("Hero").transform;
    }

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(hero.position.x + HERO_OFFSET_X, hero.position.y + HERO_OFFSET_Y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DontDestroyOnLoadHelper.GameStarted)
        {
            newPos = GroundManager.MyGroundsProp[GroundManager.NextGround].transform.position;
            transform.position = new Vector3(hero.position.x + HERO_OFFSET_X, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, newPos.y + HERO_OFFSET_Y, transform.position.z), 1f * Time.deltaTime);
            //new Vector3(hero.position.x + HERO_OFFSET_X, newPos.y + HERO_OFFSET_Y, transform.position.z);
        }
    }
}
