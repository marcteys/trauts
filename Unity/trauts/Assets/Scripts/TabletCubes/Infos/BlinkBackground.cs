using UnityEngine;
using System.Collections;

public class BlinkBackground : MonoBehaviour {


	public int blinkTime = 5;
	public float blinkSpeed = 2f;
	public bool blink = false;
	public float maxAlpha = 0.3f;


	Renderer rd;
	MaterialPropertyBlock materialProperty;

	public Color col;
	public int tmpBlink = 0;
	public bool blinkOn = true;

	void Start()
	{
		rd = this.transform.GetComponent<Renderer>();
		col = new Color(1,1,1,0);
		materialProperty = new MaterialPropertyBlock();
		//materialProperty.AddColor("_TintColor", col);
		//rd.SetPropertyBlock(materialProperty);

	}

	void Update ()
	{
		if(blink) Blink();
	}

	void Blink()
	{
		if(tmpBlink < blinkTime)
		{
			if(blinkOn)
			{
				col = Color.Lerp(col,new Color(1,1,1,maxAlpha), Time.deltaTime * blinkSpeed);
				if(col.a > maxAlpha-0.5f)
				{
					blinkOn = false;
					tmpBlink++;

				}

			} 
			else 
			{
				col = Color.Lerp(col,new Color(1,1,1,0), Time.deltaTime * blinkSpeed);
				if(col.a < 0.05f)
				{
					blinkOn = true;
					tmpBlink++;
				}

			}

			materialProperty.AddColor("_TintColor", col);
			rd.SetPropertyBlock(materialProperty);

		}
		else
		{
			//reset values
			blink = false;
			tmpBlink = 0;
			col = new Color(1,1,1,0);
			materialProperty.AddColor("_TintColor", col);
			rd.SetPropertyBlock(materialProperty);

		}

	}
}
