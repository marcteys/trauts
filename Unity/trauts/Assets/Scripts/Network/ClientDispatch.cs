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

	public int cubeNumber;
	public GameObject[] cubesList;

	void Start()
	{
		//debug
		//Network.InitializeServer(200,8081,true);

		cubesList = new GameObject[cubeNumber];

		for(int i= 0; i<cubeNumber; ++i)
		{
			cubesList[i] = GameObject.Find("Cube_"+i);
		}



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
	void CreateWave(int cubeID, int intMode)
	{
		Vector3 cubePos = cubesList[cubeID].transform.position;

		GameObject wavePrefab = (GameObject)Resources.Load ("Tablet/Repulsive") as GameObject;
		GameObject tmpWave = (GameObject)Instantiate(wavePrefab,cubePos,wavePrefab.transform.rotation);
	}


	[RPC]
	void ChangeMode(int cubeID, InteractiveMode newMode)
	{

	}

}
