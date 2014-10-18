using UnityEngine;
using System.Collections;

public class ExplicationsBehavious : MonoBehaviour {



	public Texture[] defaultTextures;
	public Texture[] stuartTextures;
	public Texture[] cubesTextures;

	public float[] defaultDelay;
	public float[] stuartDelay;
	public float[] cubesDelay;
	public float targetDelay = 0;

	//btn
	private GameObject stuartBtn;
	private GameObject cubesBtn;
	private GameObject playBtn;

	public bool startStuart = false;
	public bool startCubes = false;

	private bool pause = false;
	private bool skip = false;


	public float counter = 0;

	public int currentState = 0;


	public AudioClip[] audios;
	private AudioSource aso;

	void Start ()
	{
		stuartBtn = GameObject.Find("Stuart");
		cubesBtn = GameObject.Find("Cubes");
		playBtn = GameObject.Find("Play");
		pause = false;

		targetDelay = defaultDelay[currentState];

		ActivateObjects(false);
		playBtn.gameObject.SetActive(false);
		aso = this.GetComponent<AudioSource>();

		PlaySound(0);
	}
	
	void Update ()
	{
		if(Input.GetMouseButtonUp(0)) 
		{	
			 DetectBouton ();
		}	



		// si on ets sur le truc de base
		if(!startStuart && !startCubes)
		{
			if(counter >= targetDelay)
			{
				if(!pause)  currentState ++;
				if(currentState >= defaultDelay.Length){
					pause = true;
					this.renderer.enabled = false;
					ActivateObjects(true);
				} else
				{
					targetDelay = defaultDelay[currentState];
					this.renderer.material.SetTexture("_MainTex", defaultTextures[currentState]);
				}

			}

		} else if(startStuart)
		{

			if(counter >= targetDelay)
			{
				if(!pause)  currentState ++;

				if(currentState >= stuartDelay.Length){
					pause = true;
					DisplayPlay();

				} else
				{
					targetDelay = stuartDelay[currentState];
					this.renderer.material.SetTexture("_MainTex", stuartTextures[currentState]);
				}
				
			}
			if(currentState == stuartTextures.Length) pause = true;


		}
		else if(startCubes)
		{	if(counter >= targetDelay)
			{
				if(!pause) currentState ++;

				if(currentState >= cubesDelay.Length){
					pause = true;
					DisplayPlay();
				} else
				{
					targetDelay = cubesDelay[currentState];
					this.renderer.material.SetTexture("_MainTex", cubesTextures[currentState]);
				}
				
			}
			if(currentState == cubesDelay.Length) pause = true;
		}


		//fin du delai de l'intro

		if(!pause)	counter += Time.deltaTime;

	}
	
	void DetectBouton()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100f))
		{
			if(hit.transform.name == "Stuart")
			{
				PlayStuart();

			} else if(hit.transform.name ==  "Cubes")
			{
				PlayCubes();
			}
			else if(hit.transform.name ==  "Play")
			{
				Play();
			}else if(hit.transform.name ==  "Skip")
			{
				Skip();
			}


		}
	}

	void PlaySound(int id)
	{
		aso.clip = audios[id];
		aso.Play();
	}

	void ActivateObjects(bool state)
	{
		stuartBtn.SetActive(state);
		cubesBtn.SetActive(state);
	}

	public void PlayStuart()
	{
		counter = 0;
		pause = false;
		startStuart = true;
		ActivateObjects(false);
		currentState= 0;
		this.renderer.enabled = true;
		targetDelay = stuartDelay[0];
		this.renderer.material.SetTexture("_MainTex", stuartTextures[0]);
		PlaySound(1);
		if(skip) Play();

	}

	public void PlayCubes()
	{
		counter = 0;
		pause = false;
		startCubes = true;
		startCubes = true;
		ActivateObjects(false);
		currentState= 0;
		this.renderer.enabled = true;
		targetDelay = cubesDelay[0];
		this.renderer.material.SetTexture("_MainTex", cubesTextures[0]);
		PlaySound(2);
		if(skip) Play();

	}

	void DisplayPlay()
	{
		PlaySound(3);
		playBtn.gameObject.SetActive(true);
		this.renderer.enabled = false;
	}


	void Skip()
	{
		pause = true;
		this.renderer.enabled = false;
		ActivateObjects(true);
			skip = true;
		aso.Stop ();
	}


	public void Play()
	{
		if(startStuart)  Application.LoadLevel("clientStuart");
		if(startCubes)  Application.LoadLevel("clientCubes");
	}
}
