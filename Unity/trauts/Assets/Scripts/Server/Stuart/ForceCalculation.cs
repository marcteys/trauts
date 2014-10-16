﻿using UnityEngine;
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

	//sendViaOSC
	private OSCSendStuart oscStuart;

	/*
	  Just remember...

	msg[0]=(byte) o; // moteur gauche, vers l'avant
	msg[1]=0; //moteur de gauche , vers l'arriere
	msg[2]=0; //moteur de droite, vers l'arriere
	msg[3]=0; //moteur de droite, vers l'avant
*/

	void Start ()
	{
		astarVehicle = this.GetComponent<AstartAI>();
		oscStuart = GetComponent<OSCSendStuart>();
	}
	
	void FixedUpdate ()
	{
		targetVector = astarVehicle.targetVector;
		GetCubesForce();
		WheelAngle();
	}

	void WheelAngle()
	{
		//apply tht counter force
		if(counterForce != Vector3.zero)
		{
			targetVector = (targetVector + counterForce) * 0.5f;
			Debug.DrawRay(transform.position,targetVector*2,Color.black); // the target vector with forces
		}

		float angle = Vector3.Angle(targetVector, transform.forward);
		Vector3 cross= Vector3.Cross(targetVector, transform.forward);
		if (cross.y < 0) angle = -angle;

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



		/*
	  Just remember...

	msg[0]=(byte) o; // moteur gauche, vers l'avant
	msg[1]=0; //moteur de gauche , vers l'arriere
	msg[2]=0; //moteur de droite, vers l'arriere
	msg[3]=0; //moteur de droite, vers l'avant
*/
		oscStuart.m1_1 = (byte)0;
		oscStuart.m1_2 = (byte)0;
		oscStuart.m2_1 = (byte)0;
		oscStuart.m2_2 = (byte)0;

		if(leftMotorDirection) oscStuart.m1_1 = (byte)leftMotorPower;
		else oscStuart.m1_2 = (byte)leftMotorPower;

		if(rightMotorDirection) oscStuart.m2_1 = (byte)rightMotorPower;
		else oscStuart.m2_2 = (byte)rightMotorPower;

		//oscStuart.

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
				counterForce = counterForce + hit.collider.GetComponent<Waves>().GetForceAtPoint(hit.point);
				// ici appliquer une force relative a la puissance ???
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

