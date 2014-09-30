using UnityEngine;
using System.Collections;

public class Autodestroy : MonoBehaviour {


	public float destroyDelay = 1f;

	// Update is called once per frame
	void Update () {
		if(destroyDelay > 0) 
		{
			destroyDelay -= Time.deltaTime;
		}
		else 
		{
			Destroy(this.gameObject);
		}
	}
}
