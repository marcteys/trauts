using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ClientDispatch : MonoBehaviour {

	public NetworkView serverView;

	//waves 
	public enum InteractiveMode {
		Repulsive,
		Attractive,
		Emc
	}

	//cubes sync position
	public int cubeNumber;
	private GameObject[] cubesList;
	public GameObject[] cubesListNetwork;
	private GameObject stuartObj;
	public GameObject stuartObjNetwork;
	private bool isReady = false;
	public string activeImageTarget;

	// target manager
	private GameObject targetObj;
	public bool isStuartTablet = false;

	public SoundManager soundManager;

	void Start()
	{
		//debug
	//	Network.InitializeServer(200,8081,true);

		cubesList = new GameObject[cubeNumber];
		for(int i= 0; i<cubeNumber; ++i)
		{
			cubesList[i] = GameObject.Find("Cube_"+i);
		}
		stuartObj = GameObject.Find("Stuart");
		targetObj = GameObject.Find ("Target");

		soundManager = new SoundManager();
	}


	void OnServerInitialized()
	{
		SpawnCubes();
		//Debug.Log("Server init");
	}
	void  OnConnectedToServer()
	{
	//	SpawnCubes();
		//Debug.Log("Connected to server");
		StartCoroutine(GetObjects());


	}

	IEnumerator GetObjects() {
		yield return new WaitForSeconds(2.0f);
		cubesListNetwork = new GameObject[cubeNumber];
		for(int i= 0; i<cubeNumber; ++i)
		{
			cubesListNetwork[i] = GameObject.Find("Cube_Network_"+i +"(Clone)");
		}
		stuartObjNetwork = GameObject.Find("Stuart_Network(Clone)");
	}


	void SpawnCubes()
	{
		//Debug.Log ("spawn cubes");
		//instantiate cubes
		cubesListNetwork = new GameObject[cubeNumber];
		for(int i= 0; i<cubeNumber; ++i)
		{
			GameObject tmpCube = (GameObject)Resources.Load("Network/Cube_Network_"+i) as GameObject;
			GameObject newCube = (GameObject)Network.Instantiate(tmpCube,this.transform.position,Quaternion.identity,0) as GameObject;
			newCube.transform.name = tmpCube.transform.name;
			cubesListNetwork[i] = GameObject.Find("Cube_Network_"+i);
		}

		//instantialte stuart
		GameObject tmpStuart = (GameObject)Resources.Load("Network/Stuart_Network") as GameObject;
		GameObject newStu = (GameObject)Network.Instantiate(tmpStuart,this.transform.position,Quaternion.identity,0) as GameObject;
		newStu.transform.name = tmpStuart.transform.name;
		stuartObjNetwork = GameObject.Find("Stuart_Network");

		isReady = true;
	}

	void Update()
	{
		if(Network.isServer && isReady)
		{
			//update cube position
			for(int i= 0; i<cubeNumber; ++i)
			{
				cubesListNetwork[i].transform.position = cubesList[i].transform.position;
				Vector3 newRotation = new Vector3(cubesList[i].transform.eulerAngles.x, cubesList[i].transform.eulerAngles.x, cubesList[i].transform.eulerAngles.z);
				cubesListNetwork[i].transform.eulerAngles = newRotation;
			}

			stuartObjNetwork.transform.position = stuartObj.transform.position;
			Vector3 stRotation = new Vector3(stuartObj.transform.eulerAngles.x, stuartObj.transform.eulerAngles.x, stuartObj.transform.eulerAngles.z);
			stuartObjNetwork.transform.eulerAngles = stRotation;

			//recalculate target in real time
			Vector3 recalculatedTarget = (targetObj.transform.position - stuartObj.transform.position);
			recalculatedTarget = Quaternion.Inverse(stuartObj.transform.rotation) * recalculatedTarget;

			serverView.RPCEx("SendStuartTarget",RPCMode.Others,recalculatedTarget.x,recalculatedTarget.y,recalculatedTarget.z);

		}


		if (Input.GetMouseButtonDown(0))
        {
            //soundManager.Trigger(SoundManager.SoundType.click);
        }

	}

	[RPC]
	void CreateWave(int cubeID, int intMode)
	{
		Vector3 cubePos = cubesList[cubeID].transform.position;
		if(isStuartTablet)
		{
			if(cubesList[cubeID].transform.parent.GetComponent<DefaultTrackableEventHandler>().isActive == true  )
			{
				//si le cube sur lequel on lance la vague est actif
				cubePos = cubesList[cubeID].transform.position;
			}
			else if(stuartObj.GetComponent<DefaultTrackableEventHandler>().isActive)
			{
				cubePos = Quaternion.Inverse(stuartObjNetwork.transform.rotation) * (cubesListNetwork[cubeID].transform.position - stuartObjNetwork.transform.position);
			}
			else
			{
				cubePos = new Vector3(1000,1000,1000);
			}
		}

		InteractiveMode waveType =  (InteractiveMode)intMode;

		GameObject repulsiveWave = (GameObject)Resources.Load ("Tablet/StuartWaveRepulsive") as GameObject;
		GameObject attractiveWave = (GameObject)Resources.Load ("Tablet/StuartWaveAttractive") as GameObject;
		GameObject emcWave = (GameObject)Resources.Load ("Tablet/StuartWaveEmc") as GameObject;
		GameObject wavePrefab  = attractiveWave;

		switch(waveType)
		{
		case InteractiveMode.Repulsive :
			wavePrefab  = repulsiveWave;
			if(Network.isServer) soundManager.Trigger(SoundManager.SoundType.repulsive);
			break;
			
		case InteractiveMode.Attractive : 
			wavePrefab  = attractiveWave;
			if(Network.isServer) soundManager.Trigger(SoundManager.SoundType.attractive);
			break;

		case InteractiveMode.Emc : 
			wavePrefab  = emcWave;
			if(Network.isServer) soundManager.Trigger(SoundManager.SoundType.emc);
			CreateEmc(cubeID);
			break;
		}

		/*GameObject tmpWave = (GameObject)*/Instantiate(wavePrefab,cubePos,cubesList[cubeID].transform.rotation);
	}

	[RPC]
	void ChangeMode(int cubeID, int newMode)
	{
		cubesList[cubeID].GetComponent<CubeInteraction>().SetNewType(newMode);
		if(!Network.isServer) soundManager.Trigger(SoundManager.SoundType.slide);
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
				Vector3 diffVector =  cubesList[cubeID].transform.position - cubesList[i].transform.position;
				serverView.RPCEx("ApplyEmC", RPCMode.All, i, diffVector.x,diffVector.y,diffVector.z);
//				Debug.Log ("we create a emc from cube_" + cubeID );
			}
		}
	}

	[RPC]
	void ApplyEmC(int cubeID, float cubeOriginX,float cubeOriginY,float cubeOriginZ)
	{
		Vector3 cubeOrigin = new Vector3( cubeOriginX, cubeOriginY, cubeOriginZ);

		//Debug.DrawRay(cubeOrigin,Vector3.up, Color.black,50f);

		EmcCreator emc = new EmcCreator();
		emc.LaunchEmc(cubeOrigin,cubesList[cubeID]);
		//cubesList[i].GetComponent<CubeInteraction>().emc.
			
	}

	[RPC]
	void SetStuartTarget(float targetPositionX,float targetPositionY,float targetPositionZ)
	{
        //quand la tablette stuart envoie une nouvelle direction au serveur
		targetObj.transform.position = stuartObj.transform.position + (stuartObj.transform.rotation  * new Vector3(targetPositionX,targetPositionY,targetPositionZ));

	}

	[RPC]
	void SendStuartTarget(float targetPositionX,float targetPositionY,float targetPositionZ)
	{
        //retour en temps réel recalculé dans cette boucle loop
        targetObj.transform.position = stuartObj.transform.position +  new Vector3(targetPositionX, targetPositionY, targetPositionZ);
	}

	[RPC]
	void StuartDead()
	{


	}


}
