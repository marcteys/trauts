using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {

	private Animator am;

	public AnimationClip an;


	
	void Awake ()
	{
		am = transform.GetComponent<Animator>();

	am.Play (an.name);
	}
}
