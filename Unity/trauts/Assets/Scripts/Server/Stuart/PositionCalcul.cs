using UnityEngine;
using System.Collections;

public class PositionCalcul : MonoBehaviour {

	public GameObject stuartObj;
	public Transform[] receivedPoints;

	void Start ()
	{
		stuartObj = GameObject.Find("Stuart");
		receivedPoints = new Transform[3];
		int i = 0;
		foreach (Transform child in transform)
		{
			receivedPoints[i] = child;
			i++;
		}

		CalculatePosition();
	}
	
	void Update ()
	{
		CalculatePosition();
	}


	void CalculatePosition()
	{
		float[] dist = new float[3];
		
		dist[0] = Vector3.Distance(receivedPoints[1].position,receivedPoints[2].position);
		dist[1] = Vector3.Distance(receivedPoints[2].position,receivedPoints[0].position);
		dist[2] = Vector3.Distance(receivedPoints[0].position,receivedPoints[1].position);
		
		int farestPoint =MinValueID(dist) ;
		Vector3 farDistance = receivedPoints[farestPoint].position;
		Vector3 nearDistance = Vector3.zero;

	
		switch(farestPoint)
		{
		case 0:
			nearDistance = Vector3.Lerp(receivedPoints[1].position, receivedPoints[2].position,0.5f);
			break;
		case 1:
			nearDistance = Vector3.Lerp(receivedPoints[0].position, receivedPoints[2].position,0.5f);
			break;
		case 2:
			nearDistance = Vector3.Lerp(receivedPoints[1].position, receivedPoints[0].position,0.5f);
			break;

		}
		Vector3 direction = farDistance - nearDistance;
		Vector3 center = Vector3.Lerp(farDistance,nearDistance,0.5f);
		stuartObj.transform.position = center;

		stuartObj.transform.rotation = Quaternion.Euler(direction);

		Debug.DrawRay(nearDistance,direction,Color.red);
		Debug.DrawRay(center,Vector3.up,Color.green);
		//Debug.Log (MinValue(dist));

	}


	float MinValue(float[] values)
	{
		int end =values.Length;
		if (end <= 0)
			return 0.0f;
		float minValue = values[0];
		for(int i= 1; i < end; i++)
		{
			if (minValue>values[i])minValue=values[i];
		}
		return minValue;
	}
	
	int MinValueID(float[] values)
	{
		int end =values.Length;
		if (end <= 0)
			return 0;

		int id = 0;
		float minValue = values[0];

		for(int i= 1; i < end; i++)
		{
			if (minValue>values[i])
			{
				minValue=values[i];
				id =i;
			}
		}
		return id;
	}

}
