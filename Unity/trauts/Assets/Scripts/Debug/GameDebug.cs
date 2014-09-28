using UnityEngine;
using System.Collections;

public class GameDebug : MonoBehaviour {

	
	
	public Transform ground;
	
	public string tagObstacles  = "Cube" ;
	public float limit  = 250f ;
	
	
	private ForceWave forceWaveScript;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			DetectCube();
		}	
	}
	
	
	void DetectCube() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		//desactivate if click somewhere
		//if(tmpCubeScript) tmpCubeScript.Desactivate();
		//tmpCubeScript = null;
		
		if (Physics.Raycast(ray, out hit, limit)) {
			
			if(hit.transform.CompareTag( tagObstacles )){
				Debug.Log("click on Cube " + hit.transform.name);
				forceWaveScript = hit.transform.gameObject.GetComponent<ForceWave>();
				forceWaveScript.CreateWave();
			} 
			
		}
	}

}
