using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Application.runInBackground = true;
		Application.targetFrameRate = 60;
		Screen.SetResolution(1400, 800, FullScreenMode.Windowed);
		DontDestroyOnLoad(gameObject);

		bool isServer = Settings.Instance.IsServer;
		Debug.Log("isServer: " + isServer);
    }
}
