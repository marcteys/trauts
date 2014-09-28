using UnityEngine;
using System.Collections;

public class ForceWave : MonoBehaviour {


	private GameObject wavePrefab;


	void Start()
	{
		wavePrefab = (GameObject)Resources.Load ("Prefabs/circle") as GameObject;
	}


	public void CreateWave() {
		GameObject tmpWave = (GameObject)Instantiate(wavePrefab,this.transform.position,wavePrefab.transform.rotation);
		tmpWave.transform.parent = this.transform;
		tmpWave.transform.name = "Wave";
		tmpWave.GetComponent<Waves>().SetParent(gameObject);

	}

}
