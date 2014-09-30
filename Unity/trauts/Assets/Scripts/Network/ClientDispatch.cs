using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ClientDispatch : MonoBehaviour {

	public NetworkView serverView;

	public enum InteractiveMode {
		Repulsive,
		Attractive,
		Emc
	}

	void OnGUI()
	{
		if(GUILayout.Button("Call Print"))
		{
			serverView.RPCEx("PrintThis", RPCMode.All, "Hello World","Not lol");
		}
	}



	[RPC]
	protected void PrintThis(string text,string t2)
	{
		Debug.Log(text);
		Debug.Log (t2);
	}


	[RPC]
	void CreateWave(int cubeID, Vector3 cubePos, InteractiveMode intMode)
	{
		GameObject wavePrefab = (GameObject)Resources.Load ("Tablet/Repulsive") as GameObject;
		GameObject tmpWave = (GameObject)Instantiate(wavePrefab,cubePos,wavePrefab.transform.rotation);
	}


	[RPC]
	void ChangeMode(int cubeID, InteractiveMode newMode)
	{

	}

}
