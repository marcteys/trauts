using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {
	private Animator am;

	public AnimationClip an;

	void Start ()
	{
		am = this.GetComponent<Animator>();

	}
	
	void Awake ()
	{
		am.Play (an.name);
	}
}
