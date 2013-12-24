using UnityEngine;
using System.Collections;

public class GameJoltAPIManager : MonoBehaviour
{
    public int gameID = 20247;
    public string privateKey = "950cc59a8bd8314eacc06beebd64c0b5";
    public string userName;
    public string userToken;
    private bool _once = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GJAPI.Init(gameID, privateKey);
        GJAPIHelper.Users.GetFromWeb(OnGetFromWeb);
    }

    // Callback
    void OnGetFromWeb(string name, string token)
    {
        GJAPI.Users.Verify(name, token);
    }

    void OnEnable()
    {
        GJAPI.Users.VerifyCallback += OnVerifyUser;
    }

    void OnDisable()
    {
        GJAPI.Users.VerifyCallback -= OnVerifyUser;
    }

    void OnVerifyUser(bool success)
    {
        if (success)
        {
            GJAPIHelper.Users.ShowGreetingNotification();
        }
        else
        {
            Debug.Log("Um... Something went wrong.");
        }
    }
}
