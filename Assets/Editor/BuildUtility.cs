using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

public class BuildUtility
{
	[MenuItem("BuildUtility/Build Client", false, 1)]
	static void BuildPlayer ()
	{
		BuildPlayer(isServer: false);
	}

	[MenuItem("BuildUtility/Start Client", false, 2)]
	private static void StartClient()
	{
		StartPlayer(isServer: false);
	}

	[MenuItem("BuildUtility/Build Server", false, 50)]
	static void BuildServer()
	{
		BuildPlayer(isServer: true);
	}

	[MenuItem("BuildUtility/Start Server", false, 51)]
	private static void StartServer()
	{
		StartPlayer(isServer: true);
	}

	[MenuItem("BuildUtility/Build All", false, 100)]
	static void BuildAll()
	{
		BuildPlayer(isServer: true);
		BuildPlayer(isServer: false);
	}

	[MenuItem("BuildUtility/Start All", false, 101)]
	private static void StartAll()
	{
		StartPlayer(isServer: true);
		StartPlayer(isServer: false);
	}

	static void BuildPlayer (bool isServer)
	{
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		buildPlayerOptions.scenes = GetScenePaths();
		buildPlayerOptions.options = BuildOptions.None;
		buildPlayerOptions.target = BuildTarget.StandaloneWindows;

		if (isServer)
		{
			buildPlayerOptions.locationPathName = "Builds/Windows/Server/Basic MP Networking.exe";
			buildPlayerOptions.subtarget = (int)StandaloneBuildSubtarget.Server;
		}
		else
		{
			buildPlayerOptions.locationPathName = "Builds/Windows/Client/Basic MP Networking.exe";
		}

		BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
		BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
			Debug.Log("Output path: " + summary.outputPath);
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
	}

	private static void StartPlayer(bool isServer)
	{
		System.Diagnostics.Process process = new System.Diagnostics.Process();
		System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
		startInfo.FileName = "cmd.exe";
		
		if (isServer)
		{
			startInfo.Arguments = "/C cd Builds/Windows/Server && \"Basic MP Networking.exe\"";
		}
		else
		{
			startInfo.Arguments = "/C cd Builds/Windows/Client && \"Basic MP Networking.exe\"";
			// startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
		}

		process.StartInfo = startInfo;
		process.Start();
	}

	static string[] GetScenePaths()
	{
		string[] scenes = new string[EditorBuildSettings.scenes.Length];
		for(int i = 0; i < scenes.Length; i++) {
			scenes[i] = EditorBuildSettings.scenes[i].path;
		}
		return scenes;
	}
}
