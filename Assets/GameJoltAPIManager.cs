using UnityEngine;
using System.Collections;

public class GameJoltAPIManager : MonoBehaviour
{
    public int gameID = 20247;
    public string privateKey = "950cc59a8bd8314eacc06beebd64c0b5";
    public string userName;
    public string userToken;
    public GJTrophy trophies;
    public static GameJoltAPIManager _selfRef;

    private GJScore[] _scores;
    private bool blockUserInput = true;

    public bool BlockUserInput
    {
        get { return _selfRef.blockUserInput; }
        set { _selfRef.blockUserInput = value; }
    }

    public GJScore[] Scores
    {
        get { return _selfRef._scores; }
        set { _selfRef._scores = value; }
    }

    private GameJoltAPIManager() { }

    void Awake()
    {
        _selfRef = this;
        DontDestroyOnLoad(gameObject);
        GJAPI.Init(gameID, privateKey);
        GJAPIHelper.Users.GetFromWeb(OnGetFromWeb);
        GetScore();
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

    public void AddTrophy(uint id)
    {
        GJAPI.Trophies.Get(id);
        GJAPI.Trophies.GetOneCallback += _selfRef.OnGetRequestFinished;
    }

    private void OnGetRequestFinished(GJTrophy trophy)
    {
        if (!trophy.Achieved)
        {
            GJAPIHelper.Trophies.ShowTrophyUnlockNotification(trophy.Id);
            GJAPI.Trophies.Add(trophy.Id);
        }
    }

    public void GenerateHighscores()
    {
        GJAPI.Scores.Add(GameManager.Highscore + "m", (uint)(GameManager.Highscore * 10));
        GJAPI.Scores.AddCallback += _selfRef.OnAddRequestFinished;
    }

    private void OnAddRequestFinished(bool success)
    {
        GetScore();
    }

    private void GetScore()
    {
        GJAPI.Scores.Get();
        Debug.Log("GET");
        GJAPI.Scores.GetMultipleCallback += _selfRef.GetScoreCallback;
    }

    private void GetScoreCallback(GJScore[] scores)
    {
        Debug.Log("GET CALBACK");
        _scores = scores;
        blockUserInput = false;
    }

    void OnApplicationQuit()
    {
        foreach (var go in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            Destroy(go);
        }
    }
}
