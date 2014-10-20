using UnityEngine;
using System.Collections;

public class ForceCalculation : MonoBehaviour {


	public Vector3 targetVector = Vector3.zero;
	private AstartAI astarVehicle = null;
	private Transform targetObj;
	//motors
	public byte leftMotorPower = 0;
	public bool leftMotorDirection = true; // true = forward, false = backward;
	public byte rightMotorPower = 0;
	public bool rightMotorDirection = true; // true = forward, false = backward;
	public float maxSpeed = 160;
	float minSpeed = 0;
	public bool stopStuart = false;

	//force
	public Vector3 counterForce = Vector3.zero;

	//emc
	public bool isInEmc = false;
	public bool isSafe = false;
	public float delayBeforeHide = 1f;
	private float countDownBeforeDie = 0; 
	private bool isAlive = true;

	//sendViaOSC
	private OSCSendStuart oscStuart;

	//network
	public NetworkView serverView;

	void Start ()
	{
		astarVehicle = this.GetComponent<AstartAI>();
		oscStuart = GetComponent<OSCSendStuart>();
		targetObj = GameObject.Find ("Target").transform;

		if(serverView == null) serverView = GameObject.Find("_NetworkDispatcher").GetComponent<NetworkView>();

	}
	
	void Update ()
	{
		if(isAlive)
		{
			if(isInEmc)
			{
				if( countDownBeforeDie > delayBeforeHide)
				{
					if(!isSafe) 
					{
						//stuart est mort
						serverView.RPCEx("StuartDead", RPCMode.All);
						isAlive = false;
						Debug.Log("You'r dead, bitch");
					} else
					{
						//reset des variables
						countDownBeforeDie = 0f;
						isInEmc = false;
						isSafe = true;
						Debug.Log("Definitly safe");

					}
				}

				//décrémente le temps
				countDownBeforeDie += Time.deltaTime;
			}


			GetCubesForce();
			if(!stopStuart)
			{
				WheelAngle();
			}
			else
			{
				StopStuart();
			}

		} else
		{
			stopStuart = true;
			StopStuart();
		}
	}

	public void StopStuart()
	{
		if(oscStuart != null)
		{
			oscStuart.m1_1 = (byte)0;
			oscStuart.m1_2 = (byte)0;
			oscStuart.m2_1 = (byte)0;
			oscStuart.m2_2 = (byte)0;
		}

	}

	void WheelAngle()
	{
		//apply tht counter force
		if(counterForce != Vector3.zero)
		{
			targetVector = (targetVector + counterForce) * 0.5f;
			Debug.DrawRay(transform.position,targetVector,Color.black); // the target vector with forces
		}
		Debug.DrawRay(this.transform.position,targetVector, Color.red);

		float angle = Vector3.Angle(targetVector, transform.forward);
		Vector3 cross= Vector3.Cross(targetVector, transform.forward);
		if (cross.y < 0) angle = -angle;

		if(angle < 50 && angle> -50)
		{
			//tout droit
			leftMotorDirection = true;
			rightMotorDirection = true;

			leftMotorPower = (byte)Remap(angle,-50,50,maxSpeed,0);
			rightMotorPower  = (byte)Remap(angle,-50,50,0,maxSpeed);
		} 
		else
		{
			//sur la gauche ou sur la droit
			if( angle > 50 )
			{
				//doit tourner vers la gauche
				leftMotorDirection = false;
				rightMotorDirection = true;
				
				leftMotorPower = (byte)Remap(angle,50,180,maxSpeed,0);
				rightMotorPower  = (byte)Remap(angle,50,180,maxSpeed,minSpeed);
				rightMotorPower  = (byte)maxSpeed;
			} 
			else if( angle < -50 )
			{
				//doit tourner vers la droite
				leftMotorDirection = true;
				rightMotorDirection = false;
				
				leftMotorPower = (byte)Remap(angle,-50,-180,minSpeed,maxSpeed);
				rightMotorPower  = (byte)Remap(angle,-50,-180,maxSpeed,0);
				leftMotorPower  = (byte)maxSpeed;

			}
		} // end left/right

		/*
	  Just remember...
	  
    int(motors[0]), // celui de droite vers l'arrière
     int(motors[1]),// celui de droite vers l'avant
     int(motors[2]),// celui de gauche vers l'avant
     int(motors[3]));// celui de gauche vers l'arrière
*/

		//set the target to stuart position
		if(Vector3.Distance(targetObj.position, transform.position) < 0.6f)
		{
			targetObj.position = transform.position;
			StopStuart();
		}
		else
		{
			StopStuart();
			if(leftMotorDirection) oscStuart.m2_1 = (byte)leftMotorPower;
			else oscStuart.m2_2 = (byte)leftMotorPower;
			
			if(rightMotorDirection) oscStuart.m1_2 = (byte)rightMotorPower;
			else oscStuart.m1_1 = (byte)rightMotorPower;
		}
	
	}

	void GetCubesForce()
	{
		//We reset the counterForce
		counterForce = Vector3.zero;

		//We hit a ray 
		RaycastHit[] hits;
		hits = Physics.RaycastAll(transform.position+(Vector3.up*3), -Vector3.up * 10f , 100.0F);
		//Debug.DrawRay(transform.position+(Vector3.up*3), -Vector3.up * 5 ,Color.grey); //The ray from the car to the ground
		int i = 0;
		while (i < hits.Length)
		{

			RaycastHit hit = hits[i];
			//Debug.Log (hit.collider.name);
			if(hit.collider.CompareTag("RepulsiveWave")) 
			{
				counterForce = counterForce + hit.collider.GetComponent<Waves>().GetForceAtPoint(hit.point);
				// ici appliquer une force relative a la puissance ???
			}
			else if(hit.collider.CompareTag("AttractiveWave")) 
			{
				counterForce = counterForce + hit.collider.GetComponent<Waves>().GetForceAtPoint(hit.point);
				counterForce = -counterForce;
			}else if(!isInEmc && hit.collider.CompareTag("EmcWave")) 
			{
				isInEmc = true;
				Debug.Log ("Warning, is in emc ! ");

			} else if(!isSafe && hit.collider.CompareTag("SafeZone")) 
			{
				isSafe = true;
				Debug.Log ("You are safe");
			}

			i++;
		}


	}
	
	float Remap(this float value,  float low1,  float high1,  float low2,  float high2)
	{
		return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
	}


	void OnDisable()
	{
		StopStuart();
	}
	void OnApplicationQuit() 
	{
		StopStuart();
	}

	void OnApplicationPause() 
	{
		StopStuart();
	}
	
}

