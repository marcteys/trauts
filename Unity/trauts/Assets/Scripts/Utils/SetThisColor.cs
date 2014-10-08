﻿using UnityEngine;
using System.Collections;

public class SetThisColor : MonoBehaviour {

	public Color newColor = new Color(1,1,1,1);
	private MaterialPropertyBlock mb;

	void Awake()
	{
		mb = new MaterialPropertyBlock();
		mb.AddColor("_Color",newColor);
		mb.AddColor("_TintColor",newColor);

		this.transform.renderer.SetPropertyBlock(mb);
	}

}
