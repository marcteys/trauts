using UnityEngine;
using System.Collections;

public class ForceCalculation : MonoBehaviour {


	private Vector3 targetVector = Vector3.zero;
	private AstartAI astarVehicle = null;

	//motors
	public byte leftMotorPower = 0;
	public bool leftMotorDirection = true; // true = forward, false = backward;
	public byte rightMotorPower = 0;
	public bool rightMotorDirection = true; // true = forward, false = backward;

	//force
	public Vector3 counterForce = Vector3.zero;


	void Start ()
	{
		astarVehicle = this.GetComponent<AstartAI>();
	}
	
	void FixedUpdate ()
	{
		targetVector = astarVehicle.targetVector;
		WheelAngle();
		GetCubesForce();

	}

	void WheelAngle()
	{
		float angle = Vector3.Angle(targetVector, transform.forward);

		Vector3 cross= Vector3.Cross(targetVector, transform.forward);
		if (cross.y < 0) angle = -angle;

		/*
		byte leftMotorPower = 0;
		bool leftMotorDirection = true; // true = forward, false = backward;

		byte rightMotorPower = 0;
		bool rightMotorDirection = true; // true = forward, false = backward;
		*/
	
		if(angle < 50 && angle> -50)
		{
			//tout droit
			leftMotorDirection = true;
			rightMotorDirection = true;

			leftMotorPower = (byte)Remap(angle,-50,50,255,0);
			rightMotorPower  = (byte)Remap(angle,-50,50,0,255);
		} 
		else
		{
			//sur la gauche ou sur la droit
			if( angle > 50 )
			{
				//doit tourner vers la gauche
				leftMotorDirection = false;
				rightMotorDirection = true;
				
				leftMotorPower = (byte)Remap(angle,50,-180,100,255);
				rightMotorPower  = (byte)Remap(angle,50,-180,100,255);
			} 
			else
			{
				//doit tourner vers la droite
				leftMotorDirection = true;
				rightMotorDirection = false;
				
				leftMotorPower = (byte)Remap(angle,-50,-180,100,255);
				rightMotorPower  = (byte)Remap(angle,-50,-180,100,255);
			}
		} // end left/right

	}




	void GetCubesForce()
	{
		//We reset the counterForce
		counterForce = Vector3.zero;

		//We hit a ray 
		RaycastHit[] hits;
		hits = Physics.RaycastAll(transform.position+(Vector3.up*3), -Vector3.up * 5 , 100.0F);
		//Debug.DrawRay(transform.position+(Vector3.up*3), -Vector3.up * 5 ,Color.grey); //The ray from the car to the ground
		int i = 0;
		while (i < hits.Length)
		{
			RaycastHit hit = hits[i];
			if(hit.collider.CompareTag("Wave")) 
			{
				Debug.Log(hit.collider.name);
				counterForce = counterForce + hit.collider.GetComponent<Waves>().GetForceAtPoint(hit.point);
			}
			i++;
		}


		Debug.DrawRay(transform.position, counterForce, Color.blue);//the counter force

	}


	float Remap(this float value,  float low1,  float high1,  float low2,  float high2)
	{
		return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
	}


	
}

/*
public class DataClass {
	public first parameter;
	public second parameter
		
	public DataClass(first,second) {
		first parameter = first;
		second parameter = second;
	}
}

*/



