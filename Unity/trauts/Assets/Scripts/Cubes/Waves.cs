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

	public Vector3 GetForceAtPoint(Vector3 point)
	{
		Debug.Log (point);
		Vector3 forceDirection = Vector3.Normalize(( point - parentEmitter.transform.position));
		//Debug.DrawRay(parentEmitter.transform.position, forceDirection*5, Color.red); // The ray from object to raycast hit
		return forceDirection;
	}

}

