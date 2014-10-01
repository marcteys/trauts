using UnityEngine;
using System.Collections;

public class SetChildColor : MonoBehaviour {

	public Color newColor = new Color(1,1,1,1);
	private MaterialPropertyBlock mb;

	void Awake()
	{
		mb = new MaterialPropertyBlock();
		mb.AddColor("_Color",newColor);

		foreach (Transform child in transform)
		{
			child.renderer.SetPropertyBlock(mb);
		}
	}

}
