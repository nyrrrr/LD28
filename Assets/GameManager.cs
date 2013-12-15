using UnityEngine;
using System.Collections;
/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{

    public bool alive = true, showHighscore = false, enableRestart = false;

    public double highscore = 0f;

    private static GameManager _selfRef;
    Texture2D _tex2d;

    GUIStyle centeredStyle;
    public Font font;

    // Use this for initialization
    void Awake()
    {
        _selfRef = this;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Screen.height);
        if (!alive)
        {
            showHighscore = true;

            StartCoroutine("_HandleRestart");
            if (Input.GetKeyDown(KeyCode.Space) && enableRestart)
            {
                Application.LoadLevel(1);
            }
        }
    }

    IEnumerator _HandleRestart()
    {

        yield return new WaitForSeconds(0.1f);

        enableRestart = true;

        yield return null;
    }


    void OnGUI()
    {
        if (showHighscore)
        {

            centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;
            centeredStyle.font = font;

            centeredStyle.fontSize = 12;

            _tex2d = new Texture2D(1, 1);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height)
                                , _tex2d
                                , ScaleMode.StretchToFill
                                , true
                                , 1.0F);

            GUI.Label(new Rect((Screen.width / 2) - 400, Screen.height / 2 - 250, 800, 500), "<color=red><size=100>YOU LOST!</size></color>", centeredStyle);
            GUI.Label(new Rect((Screen.width / 2) - 400, Screen.height / 2 - 52, 800, 500), "<color=red><size=35>ran " + highscore + "m before you</size></color>", centeredStyle);
            GUI.Label(new Rect((Screen.width / 2) - 400, Screen.height / 2 + 200, 800, 500), "<color=white><size=15>Press SPACE to play again</size></color>", centeredStyle);


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
    #endregion
}
