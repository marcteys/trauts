using UnityEngine;
using System.Collections;

public class PositionAtTopLeft : MonoBehaviour {

	private Camera cam;
	void Start ()
	{
		cam = this.transform.parent.transform.camera;
		transform.position = cam.ViewportToWorldPoint(new Vector3(0,1,3));

	}

}
