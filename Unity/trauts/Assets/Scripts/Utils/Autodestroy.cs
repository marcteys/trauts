using UnityEngine;
using System.Collections;

public class Autodestroy : MonoBehaviour {


	public float destroyDelay = 1f;
	public float fadeAfter = 1f;
	public bool fade = false;

	void Awake()
	{

	}
	// Update is called once per frame
	void Update () {
		if(destroyDelay > 0) 
		{
			destroyDelay -= Time.deltaTime;

			if(fade && destroyDelay < fadeAfter )
				StartFade();

		}
		else 
		{
			Destroy(this.gameObject);
		}
	}

	void StartFade()
	{
		foreach(Transform child in this.transform)
		{
			SavePropretyBlock pb = child.gameObject.GetComponent<SavePropretyBlock>();
			pb.speed = 8f;
			pb.Fade();
		}
	}
}
