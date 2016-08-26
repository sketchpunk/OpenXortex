using UnityEngine;
using System.Collections;

namespace SP.Effects{
	public class LaserSight{
		private LineRenderer mLineRender;				//Ref to line renderer on the go
		private Transform mTrans;						//Ref to parent go
		private float mLastDistance = 0f;				//Keep track of last distance, so only to update the line if the distance has changed.
		private const float mDefaultDistance = 20f;		//Default distance when not colliding with anything.

		public LaserSight(GameObject go, float forOffset, float upOffset){
			mTrans = go.transform;
			mLineRender = go.GetComponent<LineRenderer>();
			mLineRender.SetPosition(0, mTrans.InverseTransformPoint(mTrans.position + (mTrans.forward * forOffset) + (mTrans.up * upOffset) ) );
		}

		public void onUpdate(){
			RaycastHit hit;
			float dist = mDefaultDistance;

			if(Physics.Raycast(mTrans.position,mTrans.forward,out hit)
				&& hit.collider
				&& hit.collider.gameObject.layer != Consts.LAYER_PROJECTILES) dist = hit.distance;
				 
			//Debug.Log(hit.collider.name + " " + dist);

			if(dist != mLastDistance){
				mLineRender.SetPosition(1, mTrans.InverseTransformPoint( mTrans.position + (mTrans.forward * dist)));
				mLastDistance = dist;
			}
		}
	}
}