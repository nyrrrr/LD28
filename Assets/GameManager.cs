using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public bool alive = true, showYourScore = false, enableRestart = false;

    public double highscore = 0f;

    private static GameManager _selfRef;
    Texture2D _tex2d;

    GUIStyle centeredStyle;
    public Font font;
    public bool _started = false;
    private bool once = false;
    private bool showHighscore;

    private GameJoltAPIManager api;

    public static GameJoltAPIManager Api
    {
        get { return _selfRef.api; }
        set { _selfRef.api = value; }
    }

    // Use this for initialization
    void Awake()
    {
        _selfRef = this;
        api = GameObject.Find("_GameJoltAPIManager").GetComponent<GameJoltAPIManager>();
    }

    void Start()
    {
        api.AddTrophy(5259);
        api.IncreaseReplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
        {
            showYourScore = true;

            StartCoroutine("_HandleRestart");
            if (Input.GetKeyDown(KeyCode.Space) && enableRestart)
            {
                if (!once)
                {
                    showHighscore = true;
                    api.GenerateHighscores(highscore);
                    once = true;
                }
                else if (!api.BlockUserInput)
                {
                    showHighscore = false;
                    Application.LoadLevel(1);
                }
            }
        }
    }

    IEnumerator _HandleRestart()
    {
        yield return new WaitForSeconds(0.3f);
        enableRestart = true;
        yield return null;
    }


    void OnGUI()
    {
        centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;
        centeredStyle.font = font;

        centeredStyle.fontSize = 12;

        if (showYourScore && !showHighscore && !alive)
        {
            centeredStyle.alignment = TextAnchor.UpperCenter;

            _tex2d = new Texture2D(1, 1);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height)
                                , _tex2d
                                , ScaleMode.StretchToFill
                                , true
                                , 1.0F);

            GUI.Label(new Rect((Screen.width / 2) - 400, Screen.height / 2 - 250, 800, 500), "<color=red><size=100>YOU LOST!</size></color>", centeredStyle);
            GUI.Label(new Rect((Screen.width / 2) - 400, Screen.height / 2 - 52, 800, 500), "<color=red><size=35>ran " + highscore + "m before you</size></color>", centeredStyle);
            GUI.Label(new Rect((Screen.width / 2) - 400, Screen.height / 2 + 200, 800, 500), "<color=white><size=15>Press SPACE to view Highscore</size></color>", centeredStyle);

        }
        else if (showHighscore && !alive)
        {
            centeredStyle.alignment = TextAnchor.UpperCenter;

            _tex2d = new Texture2D(1, 1);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height)
                                , _tex2d
                                , ScaleMode.StretchToFill
                                , true
                                , 1.0F);

            GUI.Label(new Rect((Screen.width / 2) - 200, 100, 400, 80), "<color=red><size=35>Highscore</size></color>", centeredStyle);
            GJScore sc;

            for (int i = 0; i < api.Scores.Length; i++)
            {
                sc = api.Scores[i];
                centeredStyle.alignment = TextAnchor.UpperLeft;
                GUI.Label(new Rect((Screen.width / 2) - 200, 230 + (i * 25), 200, 40), "<color=" + (GameJoltAPIManager._selfRef.userName.Equals(sc.Name) ? "red" : "white" ) + "><size=15>" + sc.Name + "</size></color>", centeredStyle);
                centeredStyle.alignment = TextAnchor.UpperRight;
                GUI.Label(new Rect((Screen.width / 2) + 0, 230 + (i * 25), 200, 40), "<color=" + (GameJoltAPIManager._selfRef.userName.Equals(sc.Name) ? "red" : "white") + "><size=15>" + (sc.Score.IndexOf(".") == -1 ? sc.Score.Insert(sc.Score.IndexOf("m"), ".0") : sc.Score) + "</size></color>", centeredStyle);
            }
            centeredStyle.alignment = TextAnchor.UpperCenter;
            GUI.Label(new Rect((Screen.width / 2) - 400, Screen.height / 2 + 200, 800, 500), "<color=white><size=15>Press SPACE to play again</size></color>", centeredStyle);
        }
        else
        {
            centeredStyle.alignment = TextAnchor.UpperRight;
            GUI.Label(new Rect((Screen.width) - 210, 0, 200, 100), "<color=white>" + (int)highscore + "m</color>", centeredStyle);
        }
    }


    #region getter setter
    public static bool Alive
    {
        get { return _selfRef.alive; }
        set { _selfRef.alive = value; }
    }

    public static double Highscore
    {
        get { return _selfRef.highscore; }
        set { _selfRef.highscore = value; }
    }
    public static bool Started
    {
        get { return _selfRef._started; }
        set { _selfRef._started = value; }
    }
    #endregion

    public static bool ShowHighscore
    {
        get { return _selfRef.showHighscore; }
        set { _selfRef.showHighscore = value; }
    }
}
