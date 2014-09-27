using UnityEngine;
using System.Collections;

public class ForceCalculation : MonoBehaviour {


	private Vector3 targetVector = Vector3.zero;
	private AstartAI astarVehicle = null;


	public byte leftMotorPower = 0;
	public bool leftMotorDirection = true; // true = forward, false = backward;
	
	public byte rightMotorPower = 0;
	public bool rightMotorDirection = true; // true = forward, false = backward;




	void Start ()
	{
		astarVehicle = this.GetComponent<AstartAI>();
	}
	
	void FixedUpdate ()
	{
		targetVector = astarVehicle.targetVector;
		WheelAngle();

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

		Debug.Log (angle);
	
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

	}


	float Remap(this float value,  float low1,  float high1,  float low2,  float high2)
	{
		return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
	}


	
}
