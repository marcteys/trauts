using UnityEngine;
using System.Collections;

public class PlayAnimationSlow : MonoBehaviour {

	public float speed = 3f;

	void Awake ()
	{
		this.animation["WaveEMC"].speed = 1/speed;
		this.animation.Play("WaveEMC");

	}

}
