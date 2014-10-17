using UnityEngine;
using System.Collections;

public class SayMatColorPlz : MonoBehaviour {

	void Start ()
	{
		Debug.Log ("r :" + this.renderer.material.color.r);
		Debug.Log ("g :" + this.renderer.material.color.g);
		Debug.Log ("b :" + this.renderer.material.color.b);
		Debug.Log ("a :" + this.renderer.material.color.a);
	}
	

}
