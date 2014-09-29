using UnityEngine;
using System.Collections;

public class TouchEventHandler : MonoBehaviour {

	public string tagCube = "Cube";

	private SwipeDetector sd;
	private float limit = 100;

	private GameObject wave;

	void Start ()
	{
		sd = this.GetComponent<SwipeDetector>();
		wave = (GameObject)Resources.Load ("Tablet/CircleExpand") as GameObject;
	}
	
	void Update ()
	{
		if(Input.GetMouseButtonUp(0))
		{
			DetectCubes();
		}	

	}
	
	void DetectCubes()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, limit))
		{
			if(hit.transform.CompareTag( tagCube ))
			{
				Debug.Log ("cube");
				Instantiate(wave, hit.transform.position, wave.transform.rotation);
			}
		}
	}


}
