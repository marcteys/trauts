using UnityEngine;
using System.Collections;


public static class ExtensionMethods {

	public static void ResetTransformation(this Transform trans)
	{
		trans.position = Vector3.zero;
		trans.localRotation = Quaternion.identity;
		trans.localScale = new Vector3(1, 1, 1);
	}
	/*
	
	public static float Remap(this float value,  float low1,  float high1,  float low2,  float high2)
	{
		return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
	}
*/
}