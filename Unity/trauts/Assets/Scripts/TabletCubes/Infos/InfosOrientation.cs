using UnityEngine;
using System.Collections;

public class InfosOrientation : MonoBehaviour {


	private Transform mainCam;
	private Transform parent;
	private float speed = 10f;

	private Vector3 targetAngle = Vector3.zero;


	private float degree;
	private float angle;

	void Start ()
	{
		parent = this.transform.parent;
		mainCam = Camera.main.transform;
	}
	
	void Update ()
	{

		float gauche = AngleDir(new Vector3(1,0,1),mainCam.position,Vector3.up);
		float face = AngleDir(new Vector3(-1,0,1),mainCam.position,Vector3.up);


		Vector3 targetVector = new Vector3(0,0,0);


		Debug.Log ("face " + face + " gauche " + gauche);
	
		if(gauche == 1)
		{
			if(face == 1)
			{
				targetVector =  Vector3.right;
				targetAngle = new Vector3(0,270,0);
				degree = 270;
			}
			else
			{
				targetVector =  -Vector3.forward;
				targetAngle = new Vector3(0,0,0);
				degree = 0;

			}
		}
		else
		{

			if(face == 1)
			{
				targetVector =  Vector3.forward;
				targetAngle = new Vector3(0,180,0);
				degree = 180;

			}
			else
			{
				targetVector =  - Vector3.right;
				targetAngle = new Vector3(0,90,0);
				degree = 90;

			}
		}


		Vector3 targetRot = Vector3.Lerp (this.transform.localEulerAngles, targetAngle, Time.deltaTime * speed );

	//	this.transform.localRotation = Quaternion.Euler(targetRot);

		if (Input.GetKeyUp(KeyCode.RightArrow))
			degree += 90f;
		if (Input.GetKeyUp(KeyCode.LeftArrow))
			degree -= 90f;
		
		angle = Mathf.LerpAngle(transform.rotation.y, degree, Time.deltaTime);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, degree,0 ), Time.deltaTime * speed );

	}


	void SetNewAngle()
	{
	//	targetAngle = 
	}


	float AngleDir (Vector3 fwd, Vector3 targetDir, Vector3 up) {
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);
		
		if (dir > 0) {
			return 1;
		} else if (dir < 0) {
			return -1;
		} else {
			return 0;
		}
	}




}