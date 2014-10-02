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
	public GameObject[] cubesListNetwork;
	public GameObject stuartObj;
	public GameObject stuartObjNetwork;

	private bool isReady = false;


	void Start()
	{
		//debug
		//Network.InitializeServer(200,8081,true);

		cubesList = new GameObject[cubeNumber];

		for(int i= 0; i<cubeNumber; ++i)
		{
			cubesList[i] = GameObject.Find("Cube_"+i);
		//	cubesListNetwork[i]= GameObject.Find("Cube_Network_"+i);
		}
		stuartObj = GameObject.Find("Stuart");
	//	stuartObjNetwork = GameObject.Find("Stuart_Network");

	}


	void OnServerInitialized()
	{
		SpawnCubes();
	}
	void  OnConnectedToServer()
	{
		SpawnCubes();
	}

	void SpawnCubes()
	{
		//instantiate cubes
		cubesListNetwork = new GameObject[cubeNumber];
		for(int i= 0; i<cubeNumber; ++i)
		{
			GameObject tmpCube = (GameObject)Resources.Load("Network/Cube_Network_"+i) as GameObject;
			GameObject newCube = (GameObject)Network.Instantiate(tmpCube,this.transform.position,Quaternion.identity,1) as GameObject;
			newCube.transform.name = tmpCube.transform.name;
			cubesListNetwork[i] = GameObject.Find("Cube_Network_"+i);
		}

		//instantialte stuart
		GameObject tmpStuart = (GameObject)Resources.Load("Network/Stuart_Network") as GameObject;
		GameObject newStu = (GameObject)Network.Instantiate(tmpStuart,this.transform.position,Quaternion.identity,1) as GameObject;
		newStu.transform.name = tmpStuart.transform.name;
		stuartObjNetwork = GameObject.Find("Stuart_Network");

		isReady = true;
	}

	void Update()
	{
		if(Network.isServer && isReady)
		{
			for(int i= 0; i<cubeNumber; ++i)
			{
				cubesListNetwork[i].transform.position = cubesList[i].transform.position;
				Vector3 newRotation = new Vector3(cubesList[i].transform.eulerAngles.x, cubesList[i].transform.eulerAngles.x, cubesList[i].transform.eulerAngles.z);
				cubesListNetwork[i].transform.eulerAngles = newRotation;
			}

			stuartObjNetwork.transform.position = stuartObj.transform.position;
			Vector3 stRotation = new Vector3(stuartObj.transform.eulerAngles.x, stuartObj.transform.eulerAngles.x, stuartObj.transform.eulerAngles.z);
			stuartObjNetwork.transform.eulerAngles = stRotation;

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
		/*GameObject tmpWave = (GameObject)*/Instantiate(wavePrefab,cubePos,wavePrefab.transform.rotation);
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
