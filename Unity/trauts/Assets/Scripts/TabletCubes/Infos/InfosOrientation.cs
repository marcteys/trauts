using UnityEngine;
using System.Collections;

public class InfosOrientation : MonoBehaviour {


	private Transform mainCam;
	private Transform parent;
	private float speed = 5f;

	private float degree;
	private float angle;

    public bool isStuart = false;

	void Start ()
	{
		parent = this.transform.parent;
		mainCam = Camera.main.transform;
        Debug.Log("root for " + this.transform.name + " ; " + this.transform.root);

       
	}
	
	void Update ()
	{
        if (isStuart && this.gameObject.activeInHierarchy == true)
        {
            this.gameObject.SetActive(false);
        } else
        {
            RotateGauge();
        }

	}

    void RotateGauge()
    {
		float gauche = AngleDir(new Vector3(1, 0, 1), mainCam.position, parent.up);
		float face = AngleDir(new Vector3(-1, 0, 1), mainCam.position, parent.up);

        if (gauche == 1)
        {
            if (face == 1)
            {
                degree = 270;
            }
            else
            {
                degree = 0;
            }
        }
        else
        {
            if (face == 1)
            {
                degree = 180;
            }
            else
            {
                degree = 90;
            }
        }

        angle = Mathf.LerpAngle(transform.rotation.y, degree, Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, degree, 0), Time.deltaTime * speed);

    }

	float AngleDir (Vector3 fwd, Vector3 targetDir, Vector3 up) {
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);
		
		if (dir > 0) {
			return 1;
		} else if (dir < 0) {
			return -1;
		} else {
			return 0;
		}
	}




}