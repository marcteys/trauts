using UnityEngine;
using System.Collections;

public class SetHologramColor : MonoBehaviour {

	public Texture texture;
	private MaterialPropertyBlock mb;
	Color defaultColor;
	
	void Awake()
	{

		mb = new MaterialPropertyBlock();
		mb.AddTexture("_MainTex",texture);

		foreach (Transform child in transform)
		{	defaultColor = child.renderer.material.color;


			Color newColor = defaultColor;
			newColor.a = (1-child.localPosition.y*4)/1.2f;
			mb.AddColor("_Color",newColor);
			mb.AddColor("_TintColor",newColor);

			child.renderer.SetPropertyBlock(mb);
			child.renderer.material.SetTexture("_MainTex", texture);
		}
	}

	void SetColor()
	{
		mb = new MaterialPropertyBlock();
/*		mb.AddColor("_Color",newColor);
		mb.AddColor("_TintColor",newColor);
		
		foreach (Transform child in transform)
		{
			child.renderer.SetPropertyBlock(mb);
		}*/

	}



	

}
