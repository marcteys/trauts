using UnityEngine;
using System.Collections;

public class ForceWave : MonoBehaviour {


	private GameObject waveRepulsion;
	private GameObject waveAttractive;
	private GameObject waveEmc;

	static int waveCount = 0;

	public enum InteractiveMode {
		Repulsive,
		Attractive,
		Emc
	}
	public InteractiveMode interactiveMode = InteractiveMode.Repulsive;


	void Start()
	{
		waveRepulsion = (GameObject)Resources.Load ("Prefabs/circle") as GameObject;
		waveAttractive = (GameObject)Resources.Load ("Prefabs/circle") as GameObject;
		waveEmc = (GameObject)Resources.Load ("Prefabs/circle") as GameObject;
	}


	public void CreateWave()
	{
		/*
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




		GameObject tmpWave = (GameObject)Instantiate(wavePrefab,this.transform.position,wavePrefab.transform.rotation);
		tmpWave.transform.parent = this.transform;
		tmpWave.transform.name = "Wave"+waveCount;
		tmpWave.GetComponent<Waves>().SetParent(gameObject);
		waveCount++;*/

	}

	public void SetCubeType(int cubeType)
	{

	}

}
