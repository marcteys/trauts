using UnityEngine;
using System.Collections;
using SimpleJSON;

public class CubesReceiver : MonoBehaviour {

	//json object
	public string jsonObject;

	//cubes objects detections
	public int cubeNumber;
	private GameObject[] cubesList;

	private ReceiveUDP udpR;
	public int divisionValue = 1;
	public float smoothSpeed = 1f;
	void Start()
	{
		//cubes detection on the scene
		cubesList = new GameObject[cubeNumber];
		for(int i= 0; i<cubeNumber; ++i)
		{
			cubesList[i] = GameObject.Find("Cube_"+i);
		}
		udpR = this.GetComponent<ReceiveUDP>();


	}
	//{'cubes': {'cube_0':{'visible' : true,'x':205,'y':405},'cube_1':{'visible' : false,'x':-1,'y':-1},'cube_2':{'visible' : true,'x':97,'y':376},'cube_3':{'visible' : false,'x':-1,'y':-1},'cube_4':{'visible' : false,'x':-1,'y':-1}}}
	void FixedUpdate()
	{
			jsonObject = udpR.datas;
			positionCubes();
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
				Vector3 newPos = new Vector3(json["cubes"]["cube_"+i]["x"].AsFloat,0,json["cubes"]["cube_"+i]["y"].AsFloat);
				cubesList[i].transform.position = Vector3.Lerp (cubesList[i].transform.position,newPos/divisionValue,Time.deltaTime * smoothSpeed);
			}
			else
			{
				cubesList[i].SetActive(false);
			}

		}

	}



}
