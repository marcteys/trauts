using UnityEngine;
using System.Collections;
using SimpleJSON;

public class CubesReceiver : MonoBehaviour {

	//json object
	public string jsonObject;

	//cubes objects detections
	public int cubeNumber;
	private GameObject[] cubesList;
	public GameObject[] stuPos;

	private ReceiveUDP udpR;
	public int divisionValue = 1;
	public int substractX = 0;
	public int substractY = 0;
	public float smoothSpeed = 1f;


	void Start()
	{
		//cubes detection on the scene
		cubesList = new GameObject[cubeNumber];
		stuPos = new GameObject[3];
		for(int i= 0; i<cubeNumber; ++i)
		{

			cubesList[i] = GameObject.Find("Cube_"+i);
		}
		udpR = this.GetComponent<ReceiveUDP>();

		stuPos[0] = GameObject.Find("stuPos0");
		stuPos[1] = GameObject.Find("stuPos1");
		stuPos[2] = GameObject.Find("stuPos2");



	}
	//{{"cubes": {"cube_0":{"visible" : false,"x":-1,"y":-1},"cube_1":{"visible" : true,"x":158,"y":348},"cube_2":{"visible" : false,"x":-1,"y":-1},"cube_3":{"visible" : true,"x":226,"y":348},"cube_4":{"visible" : true,"x":125,"y":261}},"stuart": {"stuPos0": {"x":244.936,"y":346.103,},"stuPos1": {"x":243.17,"y":313.533,},"stuPos2": {"x":224.74,"y":320.732,}}}
	void FixedUpdate()
	{
		if(udpR.isAlive && jsonObject != "")
		{
			jsonObject = udpR.datas;
			positionCubes();
			positionStuart();
		}
	}


	void positionCubes()
	{
		var json = JSON.Parse(jsonObject);

		for(int i= 0; i<cubeNumber; ++i)
		{
			bool visible = (json["cubes"]["cube_"+i][0].Value == "true") ;

			if(visible)
			{
				if(!cubesList[i].activeSelf) cubesList[i].SetActive(true);
				Vector3 newPos = new Vector3(
					json["cubes"]["cube_"+i]["x"].AsFloat - substractX,
					0,
					json["cubes"]["cube_"+i]["y"].AsFloat - substractY);
				cubesList[i].transform.position = Vector3.Lerp (cubesList[i].transform.position,newPos/divisionValue,Time.deltaTime * smoothSpeed);
			}
			else
			{
				cubesList[i].SetActive(false);
			}

		}

	}

	void positionStuart()
	{
		var json = JSON.Parse(jsonObject);
		for(int i= 0; i<3; ++i)
		{
				Vector3 newPos = new Vector3(
				json["stuart"]["stuPos"+i]["x"].AsFloat - substractX,
					0,
				json["stuart"]["stuPos"+i]["y"].AsFloat - substractY);
			stuPos[i].transform.position = Vector3.Lerp (stuPos[i].transform.position,newPos/divisionValue,Time.deltaTime * smoothSpeed);
		}
	}



}
