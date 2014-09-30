
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class SomeBaseClass : MonoBehaviour
{
	[RPC]
	protected void PrintThis(string text,string t2)
	{
		Debug.Log(text);
		Debug.Log (t2);
	}
}

[AddComponentMenu("Tests/Inherited")]
public class Inherited : SomeBaseClass
{
	
}


