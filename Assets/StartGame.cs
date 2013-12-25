using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class StartGame : MonoBehaviour
{
    private GUIStyle centeredStyle;
    public Font font;
    public GameJoltAPIManager api;

    // Use this for initialization
    void Awake()
    {
        api = GameObject.Find("_GameJoltAPIManager").GetComponent<GameJoltAPIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            api.gameStarted = true;
            Application.LoadLevel(1);
        }
    }

    void OnGUI()
    {
        if (!api.gameStarted)
        {
            centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;
            centeredStyle.font = font;
            camera.backgroundColor = new Color32(0, 0, 0, 0);

            GUI.Label(new Rect((Screen.width / 2) - 400, Screen.height / 2 - 250, 800, 500), "<color=white><size=100>YOGOB</size></color>", centeredStyle);
            GUI.Label(new Rect((Screen.width / 2) - 400, Screen.height / 2 - 52, 800, 500), "<color=red><size=35>You only got one button</size></color>", centeredStyle);
            GUI.Label(new Rect((Screen.width / 2) - 400, Screen.height / 2 + 200, 800, 500), "<color=white><size=15>Press SPACE to start the game</size></color>", centeredStyle);

            GUI.Label(new Rect((Screen.width / 2) - 400, Screen.height / 2 + 40, 800, 500), "<color=white><size=10>a game made for #LD28 by</size></color>", centeredStyle);
            GUI.Label(new Rect((Screen.width / 2) - 400, Screen.height / 2 + 60, 800, 500), "<color=white><size=10>Zerano & nyrrrr</size></color>", centeredStyle);

            GUI.Label(new Rect((Screen.width / 2) - 400, Screen.height / 2 + 110, 800, 500), "<color=white><size=10>Music by: Tommy Bulpa</size></color>", centeredStyle);
        }
    }
}
