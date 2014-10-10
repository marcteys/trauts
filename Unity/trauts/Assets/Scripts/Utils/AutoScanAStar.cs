using UnityEngine;
using System.Collections;
using Pathfinding;

public class AutoScanAStar : MonoBehaviour {


	public float frequency = 0f;
	private float currentTime = 0f;

	void Start ()
	{
		Debug.Log ("AutoScanAStar.cs : Auto scan fixed to " +frequency+" seconds. (0 = disabled) ");
	}
	
	void FixedUpdate ()
	{

		if(frequency > 0) {
			currentTime += Time.deltaTime;
			
			if(currentTime >= frequency)
			{
				AstarPath.active.Scan();
				currentTime = 0f;
			}
		}
	}

	
	void OnGUI ()
	{
		if (GUI.Button (new Rect (10,10,40,20), "Scan"))
		{
			AstarPath.active.Scan();
		}
	}



}
