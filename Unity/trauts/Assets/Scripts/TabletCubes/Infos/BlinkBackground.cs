using UnityEngine;
using System.Collections;

public class BlinkBackground : MonoBehaviour {


	private Animator am;
	public AnimationClip clip;


	void Start()
	{

		//am = GetComponent<Animation>();
		//am.Play(clip.name);

	//Animator a = GetComponent<Animator>();/*
		//a.Play("blinkBackground");
	}

	public void StartBlink()
	{
		Animation ani = this.GetComponent<Animation>();

		//animation.Play();
	//	animation.Play();
	//	am.Play(clip.name);
	}
}
