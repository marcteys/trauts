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
	
	private string colorType = "_Color";
	
	void Awake ()
	{
		mb = new MaterialPropertyBlock();
		
		if( useTint) colorType = "_TintColor";
		originalColor = this.renderer.material.GetColor(colorType);
	}
	
	void Update()
	{
		if(animate)
		{	
			Color currentColor;
			Color targetColor;
			
			if(goToColor)
			{
				currentColor = this.renderer.material.GetColor(colorType);
				targetColor = originalColor;
				
				if(goToHalf) targetColor.a = originalColor.a *0.2f;

				newColor = Color.Lerp(currentColor,targetColor,Time.deltaTime * speed/5f);
				if(newColor.a > targetColor.a-0.005f && newColor.a < targetColor.a+0.005f)
				{
					 animate = false;
					 if(this.transform.name == "Color" )
					 {
						Debug.Log ("target col " + targetColor.a);
						Debug.Log ("o col " + originalColor.a);
					 }
				}
			}
			else
			{
				currentColor = this.renderer.material.GetColor(colorType);

				targetColor = new Color(originalColor.r,originalColor.g,originalColor.b,0);
				newColor = Color.Lerp(currentColor,targetColor,Time.deltaTime * speed);
				if(newColor.a < 0.005f)	animate = false;
			}

			mb.AddColor(colorType,newColor);
			this.renderer.material.SetColor(colorType,newColor);
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
		Debug.Log ("ezry");
	}
}
