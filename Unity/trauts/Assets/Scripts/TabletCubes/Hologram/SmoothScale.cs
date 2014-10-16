using UnityEngine;
using System.Collections;

public class SmoothScale : MonoBehaviour {

	public float rate;
	public float midScale;
	public float ratio;
	
	// Drag this game object onto this variable slot.
	// It makes the code run faster than using "transform" by itself.

	Vector3 scale;

	void FixedUpdate ()
	{
		float scaleComponent = midScale * Mathf.Pow(ratio, Mathf.Sin(Time.time * rate));
		for (int i = 0; i < 3; ++i) scale[i] = scaleComponent;
		transform.localScale = scale;
	}
}
