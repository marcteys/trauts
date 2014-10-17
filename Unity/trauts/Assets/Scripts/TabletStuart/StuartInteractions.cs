using UnityEngine;
using System.Collections;

public class StuartInteractions : MonoBehaviour {



	private GameObject ground;
	private GameObject stuartObj;
    private ClientDispatch cd;

	//temp
	private DefaultTrackableEventHandler defEvent;

	void Start ()
	{
		ground = GameObject.Find ("Ground");
		stuartObj = GameObject.Find("Stuart");
        cd = GameObject.Find("_NetworkDispatcher").GetComponent<ClientDispatch>();
		defEvent = stuartObj.GetComponent<DefaultTrackableEventHandler>();
	}
	
	void Update ()
	{
		if(Input.GetMouseButtonUp(0)) // is active seulement si stuart est actif
		{	
			if(defEvent.isActive) DetectGround();
			else cd.soundManager.Trigger(SoundManager.SoundType.click);
		}	
		
	}
	
	void DetectGround()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100f))
		{
			if(hit.transform.CompareTag( "Ground" ))
			{
				stuartObj.GetComponent<StuartControls>().SetTarget(hit.point);
               cd.soundManager.Trigger(SoundManager.SoundType.stuartReact);
			}
		}
	}



}
