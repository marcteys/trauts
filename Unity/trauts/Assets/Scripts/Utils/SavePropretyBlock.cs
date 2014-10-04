using UnityEngine;
using System.Collections;

public class SavePropretyBlock : MonoBehaviour {

	public MaterialPropertyBlock mb;

	public bool animate = false;
	public bool goToColor = false;
	public bool goToHalf = false;
	
	private Color originalColor;
	private float speed = 20f;

	public bool useTint = true;
	public Color newColor;
	
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
	}

	
	void Update()
	{
		if(animate)
		{	
			Color currentColor;
			Color targetColor;
			
			if(goToColor)
			{
				if( !useTint) currentColor = this.renderer.material.color;
				else  currentColor = this.renderer.material.GetColor("_TintColor");

				targetColor = originalColor;
				
				if(goToHalf)
				{
					targetColor.a = originalColor.a *0.01f;
				}

				newColor = Color.Lerp(currentColor,targetColor,Time.deltaTime * speed/5f);
				if(newColor.a > targetColor.a-0.005f) animate = false;

			}
			else
			{
				if( !useTint) currentColor = this.renderer.material.color;
				else  currentColor = this.renderer.material.GetColor("_TintColor");

				targetColor = new Color(originalColor.r,originalColor.g,originalColor.b,0);
				newColor = Color.Lerp(currentColor,targetColor,Time.deltaTime * speed);
				if(newColor.a < 0.005f)	animate = false;

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
		animate = true;
		goToColor = false;
	}

	public void Display() {
		goToColor = true;
		animate = true;
		goToHalf = false;
	}
	
	public void HalfTransparency()
	{
		animate = true;
		goToColor = true;
		goToHalf = true;
	}
}
