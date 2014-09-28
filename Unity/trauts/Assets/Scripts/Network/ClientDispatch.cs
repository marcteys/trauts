﻿using UnityEngine;
using System.Collections;

public class ClientDispatch : MonoBehaviour {

	void Update ()
	{
		if(Network.isClient && Input.GetMouseButtonDown(1))
		{
			networkView.RPC ("CreateCircle",RPCMode.All,Vector3.one);
		}	
	}

	[RPC]
	void CreateCircle(Vector3 pos)
	{
		GameObject wavePrefab = (GameObject)Resources.Load ("Prefabs/circle") as GameObject;
		GameObject tmpWave = (GameObject)Instantiate(wavePrefab,pos,wavePrefab.transform.rotation);
	}


}
