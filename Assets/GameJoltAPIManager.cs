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

    public bool gameStarted = false;

    private int replay = 0;
    public void IncreaseReplay()
    {
        replay++;
        if (replay == 10) AddTrophy(5266);
        if (replay == 25) AddTrophy(5267);
        if (replay == 50) AddTrophy(5268);
    }

    private GJScore[] _scores;
    private bool blockUserInput = true;
    private bool once = true;

    private GameJoltAPIManager() { }

    void Awake()
    {
        _selfRef = this;
        DontDestroyOnLoad(gameObject);
        if (once) GJAPI.Init(gameID, privateKey);
    }

    void Start()
    {
        if (once) GetScore();
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
            gameStarted = true;
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
        if (trophy != null && !trophy.Achieved)
        {
            GJAPIHelper.Trophies.ShowTrophyUnlockNotification(trophy.Id);
            GJAPI.Trophies.Add(trophy.Id);
        }
    }

    public void GenerateHighscores(double highscore)
    {
        GJAPI.Scores.Add(highscore + "m", (uint)(highscore * 10));
        GJAPI.Scores.AddCallback += _selfRef.OnAddRequestFinished;
    }

    private void OnAddRequestFinished(bool success)
    {
        GetScore();
    }

    private void GetScore()
    {
        once = false;
        GJAPI.Scores.Get();
        GJAPI.Scores.GetMultipleCallback += _selfRef.GetScoreCallback;
        if(gameStarted) GameManager.ShowHighscore = true;
    }

    private void GetScoreCallback(GJScore[] scores)
    {
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
}
