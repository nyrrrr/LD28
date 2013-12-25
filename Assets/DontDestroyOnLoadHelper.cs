using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadHelper : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this);
	}
}
