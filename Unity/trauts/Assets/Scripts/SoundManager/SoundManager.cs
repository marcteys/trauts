using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SoundManager : MonoBehaviour {



	private AudioSource audioS;
	private bool isPlaying = false;

	public enum SoundType {
		click,
		error,
		startGame,
		repulsive,
		attractive,
		emc,
		slide,
		stuartDeath,
		stuartReact
	};

	public void Trigger (SoundType type)
	{
	 	List<AudioClip> audioList = new List<AudioClip>();
		string soundType = type.ToString(); 

		foreach(AudioClip g in Resources.LoadAll("Sounds/"+soundType, typeof(AudioClip)))
		{
			audioList.Add(g);
		}
//		Debug.Log ("SoundManager.cs : Play sound " + soundType);

		AudioSource audioSource = Camera.main.GetComponent<AudioSource>();
		audioSource.clip = audioList[Random.Range(0, audioList.Count)];
		audioSource.Play();

	}
}
