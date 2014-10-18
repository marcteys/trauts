using UnityEngine;
using System.Collections;

public class FitScreen : MonoBehaviour {


	private float margin = 2f;
	public float scaleFactor = 1f;

	void Start () {
		
		float height = Camera.main.orthographicSize * 2.0f;
		float width = height * Screen.width / Screen.height;
		float fix = 0;

		if( width > height ) fix = width + margin;
		if( width < height ) fix = height +margin;
		this.transform.localScale = new Vector3(fix/scaleFactor, fix/scaleFactor, 0.1f);
	}
	

}
