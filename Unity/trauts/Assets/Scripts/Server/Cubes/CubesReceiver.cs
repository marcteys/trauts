using UnityEngine;
using System.Collections;
using SimpleJSON;

public class CubesReceiver : MonoBehaviour {

	//json object
	public string jsonObject;

	//cubes objects detections
	public int cubeNumber;
	private GameObject[] cubesList;

	void Start()
	{
		//cubes detection on the scene
		cubesList = new GameObject[cubeNumber];
		for(int i= 0; i<cubeNumber; ++i)
		{
			cubesList[i] = GameObject.Find("Cube_"+i);
		}

		lol();

	}


	void lol()
	{
		var json = JSON.Parse(jsonObject);

		for(int i= 0; i<cubeNumber; ++i)
		{
			bool visible = (json["cubes"]["cube_"+i][0].Value == "true") ;

			if(visible)
			{
				if(!cubesList[i].activeSelf) cubesList[i].SetActive(true);
				Vector3 newPos = new Vector3(json["cubes"]["cube_"+i]["x"].AsFloat,0,json["cubes"]["cube_"+i]["y"].AsFloat);
				cubesList[i].transform.position = newPos;
			}
			else
			{
				cubesList[i].SetActive(false);
			}

		}

	}



}
