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
	public CubeInteraction parentCube;

	//stats for nerd
	public float gauge = 1f;
	private float waveCost = 0.2f;
	private float regenSpeed = 0.1f;

	private Transform colorBG;
	private Animator anim;

	//position stuff
	public int position = 0 ;
	public bool goRight = false;
	public bool goLeft = false;
	public float positionVal = 0.3f;
	public Vector3 targetPosition;

	//visual styff
	private MaterialPropertyBlock targetMB;

	void Start ()
	{
		colorBG = this.transform.Find("Color");	
		parentCube = this.transform.parent.GetComponent<CubeInteraction>();
		targetPosition  = this.transform.localPosition;
	}
	
	void Update ()
	{
		ApplyGauge();
		ApplyPosition();
	}

	void ApplyGauge()
	{
		if(gauge < 1f) gauge = gauge + Time.deltaTime * regenSpeed;
		
		Vector3 gaugeScale = new Vector3(0.2f,1,gauge);
		colorBG.localScale = gaugeScale;
	}

	void ApplyPosition()
	{
		this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,targetPosition,Time.deltaTime * 4.0f); 
	}

	public void CreateWave()
	{
		if(gauge > waveCost)
		{
		//	Handheld.Vibrate();
			parentCube.CreateNewWave();
			foreach (Transform child in transform)
			{
				child.GetComponent<SavePropretyBlock>().Fade();
			}
			gauge -= waveCost;
		}
		else
		{

		}
	}

	void ImpossibleToCreate()
	{

	}

	public void GoToLeft()
	{
		--position;
		targetPosition = new Vector3(positionVal*position + positionVal, this.transform.localPosition.y, this.transform.localPosition.z);
		if(position == 0)
		{
			position = 3;
			SwitchPos(new Vector3(positionVal*4f + positionVal,this.transform.localPosition.y, this.transform.localPosition.z));
		}
	}

	IEnumerator SwitchPos(Vector3 position)
	{
		this.transform.localPosition = position;
		//faire augmenter l'opacité
		//targetPosition = new Vector3(positionVal*position, this.transform.localPosition.y, this.transform.localPosition.z);
		yield return true;
	}

	public void GoToRight()
	{
		++position;
		targetPosition = new Vector3(positionVal*position +positionVal, this.transform.localPosition.y, this.transform.localPosition.z);
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
