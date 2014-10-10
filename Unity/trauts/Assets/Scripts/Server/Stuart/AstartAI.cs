using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use %Pathfinding
using Pathfinding;

[RequireComponent (typeof(Seeker))]


public class AstartAI : MonoBehaviour {
	
	// Variable init
	public Transform target;
	public float repathRate = 0.1F;
	
	public bool canSearch = true;

	protected float lastPathSearch = -9999;
	
	protected Seeker seeker;
	
	private float lastTimeCheck;

	protected Vector3[] path;
	protected int pathIndex = 0;
	protected Transform tr;

	public float pickNextWaypointDistance = 1F;
	public float targetReached = 0.2F;

	//custom values
	public Vector3 targetVector =  Vector3.zero;



	public void Start ()
	{
		//Get a reference to the Seeker component we added earlier
		seeker = GetComponent<Seeker>();
		
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (transform.position,target.position, OnPathComplete);
		tr=transform;
		Repath ();
	}

	public void OnPathComplete (Path p)
	{
		StartCoroutine (WaitToRepath ());

		//If the path didn't succeed, don't proceed
		if (p.error)
		{
			return;
		}
		
		//Get the calculated path as a Vector3 array
		path = p.vectorPath.ToArray();

		//Find the segment in the path which is closest to the AI
		//If a closer segment hasn't been found in '6' iterations, break because it is unlikely to find any closer ones then
		float minDist = Mathf.Infinity;
		int notCloserHits = 0;
		
		for (int i=0;i<path.Length-1;i++)
		{
			float dist = AstarMath.DistancePointSegmentStrict (path[i],path[i+1],tr.position);
			if (dist < minDist)
			{
				notCloserHits = 0;
				minDist = dist;
				pathIndex = i+1;
			}
			else if (notCloserHits > 6)
			{
				break;
			}
		}
	}

	
	public IEnumerator WaitToRepath ()
	{
		float timeLeft = repathRate - (Time.time-lastPathSearch);
		yield return new WaitForSeconds (timeLeft);
		Repath ();
	}
	
	
	public virtual void Repath ()
	{
		lastPathSearch = Time.time;

		if (seeker == null || target == null || !canSearch || !seeker.IsDone ())
		{
			StartCoroutine (WaitToRepath ());
			return;
		}
		
		Path p = ABPath.Construct(transform.position,target.position,null);
		seeker.StartPath (p,OnPathComplete);
		
	}

	
	public void PathToTarget (Vector3 targetPoint)
	{
		lastPathSearch = Time.time;
		if (seeker == null)
		{
			return;
		}
		//Start a new path from transform.positon to target.position, return the result to OnPathComplete
		seeker.StartPath (transform.position,targetPoint,OnPathComplete);
	}
	

	public void Update ()
	{// Start Update

		CalculateTarget();

	}// End Update

	
	public void CalculateTarget()
	{
		//	___________ Pas touche Astar
		if (path == null || pathIndex >= path.Length || pathIndex < 0)
		{
			return;
		}
		
		//Change target to the next waypoint if the current one is close enough
		Vector3 currentWaypoint = path[pathIndex];
		currentWaypoint.y = tr.position.y;

		while ((currentWaypoint - tr.position).sqrMagnitude < pickNextWaypointDistance*pickNextWaypointDistance)
		{
			pathIndex++;
			if (pathIndex >= path.Length)
			{
				//Use a lower pickNextWaypointDistance for the last point. If it isn't that close, then decrement the pathIndex to the previous value and break the loop
				if ((currentWaypoint - tr.position).sqrMagnitude < (pickNextWaypointDistance*targetReached)*(pickNextWaypointDistance*targetReached))
				{
					ReachedEndOfPath ();
					return;
				}
				else
				{
					pathIndex--;
					//Break the loop, otherwise it will try to check for the last point in an infinite loop
					break;
				}
			}
			currentWaypoint = path[pathIndex];
			currentWaypoint.y = tr.position.y;
		}

		//set target vector
		targetVector = /*path[pathIndex-3]*/currentWaypoint - tr.position;
		Debug.DrawRay(tr.position, targetVector, Color.red);

		//	___________ Pas touche Astar
	}


	
	public virtual void ReachedEndOfPath ()
	{
		//The AI has reached the end of the path
	}

} 