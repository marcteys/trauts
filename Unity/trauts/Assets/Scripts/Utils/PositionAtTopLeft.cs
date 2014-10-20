using UnityEngine;
using System.Collections;

public class PositionAtTopLeft : MonoBehaviour {

	private Camera cam;
	public bool useParentCamera = false;
	public bool toRight = false;

	void Start ()
	{

	//	if(this.transform.parent.transform.camera != null)		cam = this.transform.parent.transform.camera;
		cam = Camera.main;
		if(useParentCamera) cam = this.transform.parent.camera;

		if(toRight) transform.position = cam.ViewportToWorldPoint(new Vector3(1,1,3));
		else transform.position = cam.ViewportToWorldPoint(new Vector3(0,1,3));
	}

}
