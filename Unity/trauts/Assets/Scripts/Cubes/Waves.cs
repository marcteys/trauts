using UnityEngine;
using System.Collections;

public class Waves : MonoBehaviour {


	public GameObject parentEmitter = null;


	private float maxWaveScale = 5f;
	private float waveSpeed = 5f;

	public float currentForce = 0f;

	void FixedUpdate()
	{
		currentForce = currentForce + Time.deltaTime*waveSpeed ;
		ApplyScale();
		if(currentForce > maxWaveScale )
		{
			StopWave();
		}
	}

	void ApplyScale()
	{
		Vector3 newScale = new Vector3(currentForce,currentForce,currentForce);
		this.transform.localScale = newScale;
	}
	void StopWave() 
	{
		Destroy(this.transform.gameObject);
	}

	public void SetParent(GameObject parent)
	{
		parentEmitter = parent;
	}

	public void GetForceAtPoint()
	{
		
		
	}

}
