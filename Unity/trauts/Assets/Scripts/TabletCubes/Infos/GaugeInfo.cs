using UnityEngine;
using System.Collections;

public class GaugeInfo : MonoBehaviour {

	//type
	public enum InteractiveMode {
		Repulsive,
		Attractive,
		Emc
	}
	public InteractiveMode type;
	private CubeInteraction parentCube;

	//stats for nerd
	public float gauge = 1f;
	public float waveCost = 0.2f;
	public float regenSpeed = 0.1f;

	private Transform colorBG;
	private Animator anim;

	//position stuff
	public int position = 0 ;
	public bool goRight = false;
	public bool goLeft = false;
	public float positionVal = 0.3f;
	private Vector3 targetPosition;
	private Vector3 targetScale;
	private Vector3 halfScale = new Vector3(0.5f,0.5f,0.5f);

	//visual styff
	private MaterialPropertyBlock targetMB;

	void Start ()
	{
		colorBG = this.transform.Find("Color");	
		parentCube = this.transform.parent.transform.parent.GetComponent<CubeInteraction>();
		targetPosition  = this.transform.localPosition;
		targetScale = Vector3.one;
		if(position != 1)
		{
			targetScale = halfScale;
			targetPosition  = new Vector3(this.transform.localPosition.x,0.5f,this.transform.localPosition.z);
		}
	}
	
	void Awake()
	{
		if(position != 1)
		{
			globalHalf();
			targetScale = halfScale;
		}
	}
	
	void Update ()
	{
		ApplyGauge();
		ApplyPosition();
	}

	void ApplyGauge()
	{
		if(gauge < 1f)
			gauge = gauge + Time.deltaTime * regenSpeed;
		
		Vector3 gaugeScale = new Vector3(0.2f,1,gauge);
		colorBG.localScale = gaugeScale;
	}

	void ApplyPosition()
	{
		this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,targetPosition,Time.deltaTime * 6f); 
		this.transform.localScale = Vector3.Lerp(this.transform.localScale,targetScale,Time.deltaTime * 6f); 
	}

	public void CreateWave()
	{
		if(gauge > waveCost)
		{
			#if UNITY_ANDROID
			Handheld.Vibrate();
			#endif
			parentCube.CreateNewWave();
			gauge -= waveCost;
		}
		else
		{

		}
	}

	public void GoToLeft()
	{
		--position;
		targetPosition = new Vector3(positionVal*position + positionVal, 0.5f, this.transform.localPosition.z);
		//if(position <= 0) globalFade();
		Invoke("SwitchPos", 0.2f);
		globalHalf();
	}
	
	public void GoToRight()
	{
		++position;
		targetPosition = new Vector3(positionVal*position + positionVal, 0.5f, this.transform.localPosition.z);
		//if(position >= 4) globalFade();
		Invoke("SwitchPos", 0.2f);
		globalHalf();
		
	}
	
	void SwitchPos()
	{

		if(position <= 0){
			position =3;
			Vector3 posPlace = new Vector3(positionVal*(position+2),0.5f, this.transform.localPosition.z);
			this.transform.localPosition = posPlace;
			this.transform.localScale = halfScale;
			targetPosition = new Vector3(positionVal*(position+1),0.5f, this.transform.localPosition.z);
		 }
		 else if(position >=4)
		 {
			position = 1;
			Vector3 posPlace = new Vector3(positionVal,0.5f, this.transform.localPosition.z);
			this.transform.localPosition = posPlace;
			this.transform.localScale = halfScale;
			targetPosition = new Vector3(positionVal*(position+1),0.5f, this.transform.localPosition.z);
		 }
		 
		// s'il est en avant
		if(position == 1)
		{
			globalDisplay();
			targetScale = Vector3.one;
			targetPosition = new Vector3(positionVal*position + positionVal, 0f, this.transform.localPosition.z);
		}
		else
		{
			globalHalf();
		}
	}

	void globalHalf()
	{
		foreach (Transform child in transform)
		{
			child.GetComponent<SavePropretyBlock>().HalfTransparency();
		}
		targetScale = halfScale;
	}
	
	void globalZero()
	{
		foreach (Transform child in transform)
		{
			child.GetComponent<SavePropretyBlock>().Zero();
		}
	}
	
	void globalFade()
	{
		foreach (Transform child in transform)
		{
			child.GetComponent<SavePropretyBlock>().Fade();
		}
	}

	void globalDisplay()
	{
		foreach (Transform child in transform)
		{
			child.GetComponent<SavePropretyBlock>().Display();
		}
	}

}
