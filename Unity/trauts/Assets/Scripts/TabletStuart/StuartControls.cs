using UnityEngine;
using System.Collections;

public class StuartControls : MonoBehaviour {

	public Transform targetObj;

	public NetworkView serverView;

	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}
	
	public void SetTarget(Vector3 clickedPosition)
	{
		targetObj.transform.position = clickedPosition;

		Vector3 targetVector = clickedPosition - this.transform.position;

		serverView.RPCEx("SetStuartTarget",RPCMode.Server,targetVector.x,targetVector.y,targetVector.z);

	}

}
