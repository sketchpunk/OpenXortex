using UnityEngine;
using System.Collections;

public class LaserSight{
	private LineRenderer mLineRender;				//Ref to line renderer on the go
	private GameObject mParent;						//Ref to parent go
	private float mLastDistance = 0f;				//Keep track of last distance, so only to update the line if the distance has changed.
	private const float mDefaultDistance = 20f;		//Default distance when not colliding with anything.

	public LaserSight(GameObject go){
		mParent = go;
		mLineRender = go.GetComponent<LineRenderer>();
	}

	public void Update(){
		RaycastHit hit;
		float dist = mDefaultDistance;
	
		if(Physics.Raycast(mParent.transform.position,mParent.transform.forward,out hit) && hit.collider) dist = hit.distance; 
		//Debug.Log(hit.collider.name + " " + dist);

		if(dist != mLastDistance){
			mLineRender.SetPosition(1,mParent.transform.forward * dist);
			mLastDistance = dist;
		}

	}
}
