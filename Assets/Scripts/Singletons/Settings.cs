using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CustomSingletons;

public class Settings : Singleton<Settings>
{
	bool _isServer;
    public bool IsServer
    {
        get
        {
            return _isServer;
        }
    }

	void Awake()
	{
		#if UNITY_SERVER
		_isServer = true;
		Debug.Log("Is server!");
		#endif

		#if !UNITY_SERVER
		_isServer = false;
		Debug.Log("Not server");
		#endif
		// BuildPlayerOptions buildPlayerOptions = BuildPlayerWindow.DefaultBuildMethods.GetBuildPlayerOptions(new BuildPlayerOptions());
		// _isServer = buildPlayerOptions.subtarget == (int)StandaloneBuildSubtarget.Server;
	}
}
