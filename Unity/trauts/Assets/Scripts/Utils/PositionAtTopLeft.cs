using UnityEngine;
using System.Collections;

public class PositionAtTopLeft : MonoBehaviour {

	private Camera cam;
	void Start ()
	{

	//	if(this.transform.parent.transform.camera != null)		cam = this.transform.parent.transform.camera;
		cam = Camera.main;

		transform.position = cam.ViewportToWorldPoint(new Vector3(0,1,3));

	}

}
