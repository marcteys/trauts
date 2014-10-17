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
			newColor.a = (1-child.localPosition.y*8)/2f;
			mb.AddColor("_Color",newColor);
			mb.AddColor("_TintColor",newColor);

			child.renderer.SetPropertyBlock(mb);
			child.renderer.material.SetTexture("_MainTex", texture);
		}
	}

	public void SetColor(Color col)
	{
		mb = new MaterialPropertyBlock();

		foreach (Transform child in transform)
		{	
			col.a = (1-child.localPosition.y*6)/2f;
			
			mb.AddColor("_Color",col);
			mb.AddColor("_TintColor",col);

			child.renderer.SetPropertyBlock(mb);
		}
	}



	

}
