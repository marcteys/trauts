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

	private GameObject repulsObj;
	private GameObject attractObj;
	private GameObject emcObj;

	//visual stuff
	private Transform gaugeColor;
	private BlinkBackground gaugeBG;

	//server stuff
	public NetworkView serverView;

	void Start() 
	{
		//find the cube id
		cubeId = int.Parse(this.transform.name.Split('_')[1]);
		repulsObj = (GameObject)Resources.Load ("Tablet/Repulsive") as GameObject;
		gaugeColor = this.transform.Find("CubeInfo/Color");
		gaugeBG = this.transform.Find("CubeInfo/Background").GetComponent<BlinkBackground>();
		ChangeColor();

		if(serverView == null) serverView = GameObject.Find("_NetworkDispatcher").GetComponent<NetworkView>();
	}

	void OnConnectedToServer()
	{
		ChangeColor();
	}

	public void CreateWave()
	{
		if(gauge > waveCost)
		{
			switch(interactiveMode)
			{
			case InteractiveMode.Repulsive :
				RepulsiveWave();
				break;
				
			case InteractiveMode.Attractive : 
				AttractiveWave ();
				break;
				
			case InteractiveMode.Emc : 
				EmcWave();
				break;
			}
			gauge -= waveCost;
		}
		else
		{
			gaugeBG.StartBlink();
		}
	}

	void Update()
	{
		ApplyGauge();
	}
	
	void RepulsiveWave()
	{
		//Instantiate(repulsObj,this.transform.position,repulsObj.transform.rotation);
		serverView.RPCEx("CreateWave", RPCMode.All, cubeId, (int)interactiveMode);
	}

	void AttractiveWave()
	{
		Instantiate(repulsObj,this.transform.position,repulsObj.transform.rotation);
	}

	void EmcWave() 
	{
		serverView.RPCEx("CreateEmc", RPCMode.Server, cubeId);
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
				interactiveMode = InteractiveMode.Attractive;
				break;
				
			case InteractiveMode.Attractive : 
				interactiveMode = InteractiveMode.Emc;
				break;
				
			case InteractiveMode.Emc : 
				break;
			}
		}
		else 
		{
			//si on vas vers la gauche
			switch(interactiveMode)
			{
			case InteractiveMode.Repulsive :
				break;
				
			case InteractiveMode.Attractive : 
				interactiveMode = InteractiveMode.Repulsive;
				break;

			case InteractiveMode.Emc : 
				interactiveMode = InteractiveMode.Attractive;
				break;
			}
		}

		serverView.RPCEx("ChangeMode", RPCMode.All, cubeId, (int)interactiveMode);

		ChangeColor();
	}

	public void SetNewType(int newType)
	{
		interactiveMode = (InteractiveMode)newType;
		ChangeColor();
	}


	// other visual tools 

	void ApplyGauge()
	{
		if(gauge < 1f) gauge = gauge + Time.deltaTime * regenSpeed;
		
		Vector3 gaugeScale = new Vector3(0.2f,1,gauge);
		gaugeColor.localScale = gaugeScale;
	}

	void ChangeColor()
	{

		Color targetColor = new Color(1,1,1,1);

		switch(interactiveMode)
		{
		case InteractiveMode.Repulsive :
			targetColor = GameData.repulsiveColor;
			break;
			
		case InteractiveMode.Attractive : 
			targetColor = GameData.attractiveColor;
			break;
			
		case InteractiveMode.Emc : 
			targetColor = GameData.emcColor;
			break;
		}

		MaterialPropertyBlock mb = new MaterialPropertyBlock();
		mb.AddColor("_Color",targetColor);
		gaugeColor.transform.renderer.SetPropertyBlock(mb);

	}

}
