using UnityEngine;
using System.Collections;

public class MenuBehaviour : MonoBehaviour {


	public GameObject background;
	public GameObject menuOpen;
	public GameObject menuClose;
	public GameObject menuRestart;
	public GameObject pauseText;
	public GameObject dieText;
	public GameObject winText;
	public GameObject stuartBtn;
	public GameObject cubesBtn;

	private Camera parentCam;

	void Start ()
	{
		background = this.transform.Find("Background").gameObject;
		menuOpen = this.transform.Find("MenuOpen").gameObject;
		menuClose = this.transform.Find("MenuClose").gameObject;
		pauseText = this.transform.Find("PauseText").gameObject;
		dieText = this.transform.Find("DieText").gameObject;
		winText = this.transform.Find("WinText").gameObject;
		stuartBtn = this.transform.Find("StuartBtn").gameObject;
		cubesBtn = this.transform.Find("CubesBtn").gameObject;
		menuRestart = this.transform.Find("MenuRestart").gameObject;

		HideAll();

		pauseText.SetActive(false);
		menuClose.SetActive(false);
		dieText.SetActive(false);
		winText.SetActive(false);
		stuartBtn.SetActive(false);
		cubesBtn.SetActive(false);

		parentCam = this.transform.camera;

	}
	
	void Update ()
	{
		if(Input.GetMouseButtonUp(0)) 
		{	
			DetectBouton ();
		}	

	}


	public void HideAll()
	{
		HideGameObject(pauseText,5f);
		HideGameObject(dieText,5f);
		HideGameObject(winText,5f);
		HideGameObject(stuartBtn,5f);
		HideGameObject(cubesBtn,5f);

	}

	public void HideGameObject(GameObject go, float speed)
	{
		SavePropretyBlock sb =  go.GetComponent<SavePropretyBlock>();
	//	sb.originalColor = Color.white;
		sb.speed = speed;
	//	sb.newColor = Color.white;
		sb.Zero();
	}

	public void DisplayGameObject(GameObject go, float speed)
	{
		if(go.activeInHierarchy ==false) go.SetActive(true);

		SavePropretyBlock sb =  go.GetComponent<SavePropretyBlock>();
	//	sb.originalColor = Color.white;
		sb.speed = speed;
		sb.Display();
	}

	void DetectBouton()
	{

		Ray ray = parentCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100f))
		{

			if(hit.transform.name == menuOpen.transform.name)
			{
				Debug.Log ("open menu");
				DisplayMenu(true);
			}
			else if(hit.transform.name == menuClose.transform.name)
			{
				DisplayMenu(false);

			}else if(hit.transform.name == stuartBtn.transform.name)
			{
				StartLevel("clientStuart");
				
			}else if(hit.transform.name == cubesBtn.transform.name)
			{
				StartLevel("clientCubes");

			}else if(hit.transform.name == menuRestart.transform.name)
			{
				StartLevel("androidScene");
				
			}

			
		}
	}




	public void DisplayMenu(bool active)
	{
		menuClose.SetActive(active);
		menuOpen.SetActive(!active);
		menuRestart.SetActive(!active);

		pauseText.SetActive(!active);
		stuartBtn.SetActive(!active);
		cubesBtn.SetActive(!active);

		if(active)
		{
			DisplayGameObject(pauseText, 5f);
			DisplayGameObject(background, 5f);
			DisplayGameObject(stuartBtn, 5f);
			DisplayGameObject(cubesBtn, 5f);
		}
		else if(!active)
		{
			HideGameObject(pauseText,5f);
			HideGameObject(stuartBtn,5f);
			HideGameObject(cubesBtn,5f);
			HideGameObject(background,5f);

		}



	}

	public void DisplayEnd()
	{

		string thisLevel = Application.loadedLevelName;

		if(thisLevel == "clientStuart")
		{
			DisplayGameObject(background,5f);
			DisplayGameObject(dieText, 5f);
			DisplayGameObject(stuartBtn, 5f);
			DisplayGameObject(cubesBtn, 5f);

			StartCoroutine(WaitBeforeDisplay());

		}
		else
		{
			DisplayGameObject(background,5f);
			DisplayGameObject(winText, 5f);
			DisplayGameObject(stuartBtn, 5f);
			DisplayGameObject(cubesBtn, 5f);
			
			StartCoroutine(WaitBeforeDisplay());
		}

	}

	IEnumerator WaitBeforeDisplay() {
		yield return new WaitForSeconds(2.0f);

	}


	public void StartLevel(string levelName)
	{
		Application.LoadLevel(levelName);

	}

}
