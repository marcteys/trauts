using UnityEngine;
using System.Collections;

public class SavePropretyBlock : MonoBehaviour {

	public MaterialPropertyBlock mb;

	public bool animate = false;
	public bool goToColor = false;

	private Color originalColor;
	private float speed = 0.02f;

	public bool useTint = true;

	void Awake ()
	{
		mb = new MaterialPropertyBlock();

		if( !useTint)
		{
			originalColor = this.renderer.material.color;
		}
		else
		{
			originalColor = this.renderer.material.GetColor("_TintColor");
		}

		/*
		mb.AddColor("_Color",this.renderer.material.color);
		mb.AddColor("_TintColor",this.renderer.material.color);*/
	}

	void Update()
	{
		if(animate)
		{
			Color newColor = new Color();
			Color currentColor ;

			if(goToColor)
			{
				if( !useTint) currentColor = this.renderer.material.color;
				else  currentColor = this.renderer.material.GetColor("_TintColor");

				newColor = Color.Lerp(currentColor,originalColor,Time.deltaTime);
				if(newColor.a > originalColor.a-0.005f)
				{
					Debug.Log ("lol");
					animate = false;
				}
			}
			else 
			{
				if( !useTint) currentColor = this.renderer.material.color;
				else  currentColor = this.renderer.material.GetColor("_TintColor");

				Color targetColor = new Color(originalColor.r,originalColor.g,originalColor.b,0);
				newColor = Color.Lerp(currentColor,targetColor,Time.deltaTime * speed);
				if(newColor.a < 0.005f){
					Debug.Log (newColor.a);
					animate = false;
				}

			}


			if( !useTint) mb.AddColor("_Color",newColor);
			if( useTint) mb.AddColor("_TintColor",newColor);
			if( !useTint)currentColor = this.renderer.material.color = newColor;
				if( useTint) this.renderer.material.SetColor("_TintColor",newColor);

			this.renderer.SetPropertyBlock(mb);

		}
	}

	public void Fade()
	{
		Debug.Log ("fade object " + transform.name ); 
		animate = true;
		goToColor = false;
	}

	public void Display() {
		goToColor = true;
		animate = true;
	}
}
