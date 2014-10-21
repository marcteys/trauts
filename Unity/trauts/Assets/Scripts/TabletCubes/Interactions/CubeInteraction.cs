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
	public GaugeInfo activeGauge;

	public TextMesh percentText;

	//server stuff
	public NetworkView serverView;

	void Start() 
	{
		//find the cube id
		cubeId = int.Parse(this.transform.name.Split('_')[1]);

		//visual stuff
		gaugeRepuls = transform.Find("Infos/CubeInfoRepuls").GetComponent<GaugeInfo>();
		gaugeAttract = transform.Find("Infos/CubeInfoAttract").GetComponent<GaugeInfo>();
		gaugeEmc = transform.Find("Infos/CubeInfoEmc").GetComponent<GaugeInfo>();

		activeGauge = gaugeRepuls;

		//network stuff
		if(serverView == null) serverView = GameObject.Find("_NetworkDispatcher").GetComponent<NetworkView>();
		NewDeco();
	}

	void FixedUpdate()
	{
		//percentText.text = "kj";
		if(activeGauge != null) percentText.text = GetGaugePercent(activeGauge);

		/// DEBUG /////////
		/// 
		if(this.transform.name == "Cube_1" && Network.isServer)
		{
			if(Input.GetMouseButtonUp(0)) 
			{	
				CreateWave();

			}	
			if(Input.GetMouseButtonUp(2)) 
			{	
				SwipteType(true);
				
			}	

		}
	}
	
	string GetGaugePercent(GaugeInfo gi)
	{
		return Mathf.Floor(gi.gauge*100f) + "%";
	}


	void OnConnectedToServer()
	{
		activeGauge = gaugeRepuls;
		NewDeco();
	}

	public void CreateWave()
	{
		Debug.Log (activeGauge.transform.name);
		activeGauge.CreateWave();
	}

	public void CreateNewWave() 
	{

		switch(interactiveMode)
		{
		case InteractiveMode.Repulsive :
			serverView.RPCEx("CreateWave", RPCMode.All, cubeId, (int)interactiveMode);
			Debug.Log ("a");

			break;
			
		case InteractiveMode.Attractive : 
			serverView.RPCEx("CreateWave", RPCMode.All, cubeId, (int)interactiveMode);
			Debug.Log ("b");

			break;
			
		case InteractiveMode.Emc : 
			Debug.Log ("émojqhsfmoqshfg");
			serverView.RPCEx("CreateWave", RPCMode.All, cubeId, (int)interactiveMode);
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
				activeGauge = gaugeEmc;
				GaugeToRight();
				break;
				
			case InteractiveMode.Attractive : 
				interactiveMode = InteractiveMode.Repulsive;
				activeGauge = gaugeRepuls;
				GaugeToRight ();
				break;
				
			case InteractiveMode.Emc : 
				interactiveMode = InteractiveMode.Attractive;
				activeGauge = gaugeAttract;
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
		NewDeco();

		serverView.RPCEx("ChangeMode", RPCMode.All, cubeId, (int)interactiveMode);
	}

	public void SetNewType(int newType)
	{
		interactiveMode = (InteractiveMode)newType;
		NewDeco ();
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

	void NewDeco()
	{
		switch(interactiveMode)
		{
		case InteractiveMode.Repulsive :
			transform.Find("BottomDeco/SelectedRPS01").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedRPS02").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedEMC01").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedEMC02").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedATC01").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedATC02").gameObject.SetActive(false);
			transform.Find("Hologram").GetComponent<SetHologramColor>().SetColor(GameData.repulsiveColor);
			break;
			
		case InteractiveMode.Attractive : 
			transform.Find("BottomDeco/SelectedRPS01").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedRPS02").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedEMC01").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedEMC02").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedATC01").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedATC02").gameObject.SetActive(false);
			transform.Find("Hologram").GetComponent<SetHologramColor>().SetColor(GameData.attractiveColor);
			break;
			
		case InteractiveMode.Emc : 
			transform.Find("BottomDeco/SelectedRPS01").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedRPS02").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedEMC01").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedEMC02").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedATC01").gameObject.SetActive(false);
			transform.Find("BottomDeco/SelectedATC02").gameObject.SetActive(false);
			transform.Find("Hologram").GetComponent<SetHologramColor>().SetColor(GameData.emcColor);
			break;
		}
	}



}
