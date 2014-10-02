using UnityEngine;
using System.Collections;

public class StuartInteractions : MonoBehaviour {



	private GameObject ground;
	private GameObject stuartObj;
	
	void Start ()
	{
		ground = GameObject.Find ("Ground");
		stuartObj = GameObject.Find("Stuart");
	}
	
	void Update ()
	{
		if(Input.GetMouseButtonUp(0))
		{
			DetectGround();
		}	
		
	}
	
	void DetectGround()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100f))
		{
			if(hit.transform.CompareTag( "Ground" ))
			{
				stuartObj.GetComponent<StuartControls>().SetTarget(hit.point);
			}
		}
	}



}
