using UnityEngine;
using System.Collections;

public class BlinkBackground : MonoBehaviour {


	public Animation am;

	void Start()
	{
		am = GetComponent<Animation>();
	}

	public void StartBlink()
	{
		am.Play();

	}
}
