using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadHelper : MonoBehaviour {

    private bool gameStarted = false;

    public static bool GameStarted
    {
        get { return _self.gameStarted; }
        set { _self.gameStarted = value; }
    }

    public static bool GameManagerAccess
    {
        get { return _self.gameManagerAccess; }
        set { _self.gameManagerAccess = value; }
    }

    private static DontDestroyOnLoadHelper _self;
    private bool once = false;
    private bool gameManagerAccess = false;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this);
        _self = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameStarted && !once) {
            once = true;
            StartCoroutine("StartGame");
        }
	}

    IEnumerator StartGame() {
        while (!(GameObject.Find("GameManager") != null)) {
            yield return new WaitForEndOfFrame();
        }
        gameManagerAccess = true;
        GameManager.Started = true; 
        yield return null;
    }
}
