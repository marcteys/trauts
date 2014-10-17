using UnityEngine;
using System.Collections;

public class PlayAnimationBackward : MonoBehaviour {

	void Awake ()
	{
		this.animation["WaveRevert"].speed = -1;
		this.animation["WaveRevert"].time = this.animation["WaveRevert"].length;
		this.animation.Play("WaveRevert");
		

	}

}
