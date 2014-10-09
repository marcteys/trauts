using UnityEngine;
using System.Collections;

public class CubeInteraction : MonoBehaviour {

	private int cubeId =0;

	public float gauge = 1f;
	public enum InteractiveMode {
		Repulsive,
		Attractive,
		Emc
	}
	public InteractiveMode interactiveMode = InteractiveMode.Repulsive;

	//stats for nerds
	private float waveCost = 0.2f;
	private float regenSpeed = 0.1f;

	//visual stuff
	private GaugeInfo gaugeRepuls;
	private GaugeInfo gaugeAttract;
	private GaugeInfo gaugeEmc;

	private GaugeInfo activeGauge;

	//server stuff
	public NetworkView serverView;

	void Start() 
	{
		//find the cube id
		cubeId = int.Parse(this.transform.name.Split('_')[1]);

		//visual stuff
		gaugeRepuls = transform.Find("CubeInfoRepuls").GetComponent<GaugeInfo>();
		gaugeAttract = transform.Find("CubeInfoAttract").GetComponent<GaugeInfo>();
		gaugeEmc = transform.Find("CubeInfoEmc").GetComponent<GaugeInfo>();

		//network stuff
		if(serverView == null) serverView = GameObject.Find("_NetworkDispatcher").GetComponent<NetworkView>();
	}

	void OnConnectedToServer()
	{
		activeGauge = gaugeRepuls;
	}

	public void CreateWave()
	{
		activeGauge.CreateWave();
	}

	public void CreateNewWave() 
	{
		switch(interactiveMode)
		{
		case InteractiveMode.Repulsive :
			serverView.RPCEx("CreateWave", RPCMode.All, cubeId, (int)interactiveMode);

			break;
			
		case InteractiveMode.Attractive : 
			serverView.RPCEx("CreateWave", RPCMode.All, cubeId, (int)interactiveMode);

			break;
			
		case InteractiveMode.Emc : 
			serverView.RPCEx("CreateWave", RPCMode.Server, cubeId, (int)interactiveMode);

			break;
		}

	
	}

	public void SwipteType(bool slideRight)
	{
		Debug.Log ("Cube orientation Changed");

		if(slideRight) 
		{
			// si on vas vers la droite
			switch(interactiveMode)
			{
			case InteractiveMode.Repulsive :
				interactiveMode = InteractiveMode.Emc;
				activeGauge = gaugeAttract;
				GaugeToRight();
				break;
				
			case InteractiveMode.Attractive : 
				interactiveMode = InteractiveMode.Repulsive;
				activeGauge = gaugeRepuls;
				GaugeToRight ();
				break;
				
			case InteractiveMode.Emc : 
				interactiveMode = InteractiveMode.Attractive;
				activeGauge = gaugeEmc;
				
				GaugeToRight ();
				break;
			}
		}
		else 
		{
			//si on vas vers la gauche
			switch(interactiveMode)
			{
			case InteractiveMode.Repulsive :
				interactiveMode = InteractiveMode.Attractive;
				activeGauge = gaugeAttract;
				
				GaugeToLeft ();
				break;
				
			case InteractiveMode.Attractive : 
				interactiveMode = InteractiveMode.Emc;
				activeGauge = gaugeEmc;
				GaugeToLeft ();

				break;

			case InteractiveMode.Emc : 
				interactiveMode = InteractiveMode.Repulsive;
				activeGauge = gaugeRepuls;
				GaugeToLeft ();
				break;
			}
		}

		serverView.RPCEx("ChangeMode", RPCMode.All, cubeId, (int)interactiveMode);
	}

	public void SetNewType(int newType)
	{
		interactiveMode = (InteractiveMode)newType;
	}

	void GaugeToLeft()
	{
		gaugeAttract.GoToLeft();
		gaugeRepuls.GoToLeft();
		gaugeEmc.GoToLeft();
	}

	void GaugeToRight()
	{
		gaugeAttract.GoToRight();
		gaugeRepuls.GoToRight();
		gaugeEmc.GoToRight();
	}



}
