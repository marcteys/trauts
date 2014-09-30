using UnityEngine;
using System.Collections;

public class TouchEventHandler : MonoBehaviour {

	public string tagCube = "Cube";

	private SwipeDetector sd;
	private float limit = 100;

	void Start ()
	{
		sd = this.GetComponent<SwipeDetector>();
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
				hit.transform.GetComponent<CubeInteraction>().CreateWave();
			}
		}
	}

}
