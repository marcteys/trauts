using UnityEngine;
using System.Collections;

public class SlideTest : MonoBehaviour {


	private GaugeInfo gaugeRepuls;
	private GaugeInfo gaugeAttract;
	private GaugeInfo gaugeEmc;
	
	// Use this for initialization
	void Start () {
		gaugeRepuls = transform.Find("CubeInfoRepuls").GetComponent<GaugeInfo>();
		gaugeAttract = transform.Find("CubeInfoAttract").GetComponent<GaugeInfo>();
		gaugeEmc = transform.Find("CubeInfoEmc").GetComponent<GaugeInfo>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			GaugeToLeft();
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			GaugeToRight();
		}
		
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
