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

	void Awake()
	{
		audioS = GetComponent<AudioSource>();
	}

	void FixedUpdate()
	{
		if (Input.GetMouseButtonDown(0))
			Trigger(SoundType.click);
	}

	public void Trigger (SoundType type)
	{
	 	List<AudioClip> audioList = new List<AudioClip>();
		string soundType = type.ToString(); 
		Debug.Log ("lll");

		foreach(AudioClip g in Resources.LoadAll("Sounds/"+soundType, typeof(AudioClip)))
		{
			Debug.Log("prefab found: " + g.name);
			audioList.Add(g);
		}

		audioS.clip = audioList[Random.Range(0, audioList.Count)];
		audioS.Play();

	}
}
