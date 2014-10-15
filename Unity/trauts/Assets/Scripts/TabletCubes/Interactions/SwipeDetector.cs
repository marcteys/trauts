using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour {
	
	// Values to set:
	public float comfortZone = 70.0f;
	public float minSwipeDist = 14.0f;
	public float maxSwipeTime = 0.5f;
	
	private float startTime;
	private Vector2 startPos;
	public bool couldBeSwipe;
	
	public enum SwipeDirection {
		None,
		Left,
		Right
	}
		
	public SwipeDirection lastSwipe = SwipeDetector.SwipeDirection.None;
	public float lastSwipeTime;

	public CubeInteraction clickedCube;

	void  Update()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];

			switch (touch.phase)
			{
			case TouchPhase.Began:
				lastSwipe = SwipeDetector.SwipeDirection.None;
				lastSwipeTime = 0;
				startPos = touch.position;
				startTime = Time.time;
				couldBeSwipe = true;

				if(!VerifIfCube(startPos))
				{
					couldBeSwipe = false;
					return;

				}
				break;
				
			case TouchPhase.Moved:
				if (Mathf.Abs(touch.position.y - startPos.y) > comfortZone)
				{
					Debug.Log("Not a swipe. Swipe strayed " + (int)Mathf.Abs(touch.position.y - startPos.y) +
					          "px which is " + (int)(Mathf.Abs(touch.position.y - startPos.y) - comfortZone) +
					          "px outside the comfort zone.");
					couldBeSwipe = false;
				}
				break;

			case TouchPhase.Ended:
				if (couldBeSwipe)
				{
					float swipeTime = Time.time - startTime;
					float swipeDist = (new Vector3(0, touch.position.x, 0) - new Vector3(0, startPos.x, 0)).magnitude;
					
					if ((swipeTime < maxSwipeTime) && /*||*/ (swipeDist > minSwipeDist))
					{
						// It's a swiiiiiiiiiiiipe!
						float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
						
						// If the swipe direction is positive, it was an upward swipe.
						// If the swipe direction is negative, it was a downward swipe.
						if (swipeValue > 0)
						{
							lastSwipe = SwipeDetector.SwipeDirection.Left;
							clickedCube.SwipteType(true);
						}
						else if (swipeValue < 0)
						{
							lastSwipe = SwipeDetector.SwipeDirection.Right;
							clickedCube.SwipteType(false);
						}
						
						// Set the time the last swipe occured, useful for other scripts to check:
						lastSwipeTime = Time.time;
						Debug.Log("Found a swipe!  Direction: " + lastSwipe);
					}
				}
				couldBeSwipe = false;
				break;
			}
		}
	}

	bool VerifIfCube(Vector3 touchPos)
	{
		Ray ray = Camera.main.ScreenPointToRay(touchPos);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100f))
		{
			if(hit.transform.CompareTag( "Cube" ))
			{
				clickedCube = hit.transform.gameObject.GetComponent<CubeInteraction>();
				return true;
			}
		}
		Debug.Log ("cube not clicked");
		return false;

	}
}