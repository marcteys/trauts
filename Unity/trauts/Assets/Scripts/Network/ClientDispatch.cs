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
	void ChangeMode(int cubeID, int newMode)
	{
		cubesList[cubeID].GetComponent<CubeInteraction>().SetNewType(newMode);
	}

	[RPC]
	void CreateEmc(int cubeID)
	{
		//we need to calculate the vector from stuart on the scene to each cubes
		Vector3 stuartPos = GameObject.Find("Stuart").transform.position;

		for(int i = 0; i < cubesList.Length; i++)
		{
			if(i != cubeID)
			{
				Vector3 diffVector =   cubesList[cubeID].transform.position - cubesList[i].transform.position;
				serverView.RPCEx("ApplyEmC", RPCMode.All, i, diffVector.x,diffVector.y,diffVector.z);
			}
		}
	}

	[RPC]
	void ApplyEmC(int cubeID, float cubeOriginX,float cubeOriginY,float cubeOriginZ)
	{
		Vector3 cubeOrigin = new Vector3( cubeOriginX, cubeOriginY, cubeOriginZ);

		Debug.DrawRay(cubeOrigin,Vector3.up, Color.black,50f);

		EmcCreator emc = new EmcCreator();
		emc.LaunchEmc(cubeOrigin,cubesList[cubeID]);
		//cubesList[i].GetComponent<CubeInteraction>().emc.
			
	}

}
