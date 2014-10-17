using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour {

	void Update ()
	{
		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		this.transform.Translate(new Vector3(xAxisValue/3f, 0.0f, zAxisValue/3f));
	}
}
