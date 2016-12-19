using UnityEngine;
using System.Collections;
namespace SP.Movement{
	public class SpaceTracker{
		public GameObject Target = null;
		public float SlowDistance = 0.7f;
		public float RotationSpeed = 1f;
		public float MaxSpeed = 0.5f;
		public float SpeedUpInc = 0.4f;
		public float SpeedDownInc = 0.3f;
		public bool InView = false;

		private Transform mTrans;
		private Rigidbody mRBody;
		private float mSpeed = 0;

		public SpaceTracker(GameObject moveObj, GameObject target){
			mTrans = moveObj.transform;
			mRBody = moveObj.GetComponent<Rigidbody>();
		}


		public void onUpdate(){
			float dist = Vector3.Distance(mTrans.position,Target.transform.position);
			Quaternion lookRotation = Quaternion.LookRotation(Target.transform.position - mTrans.position,mTrans.up);
			bool isMove = false;
			//bool isClose = IsClose(lookRotation,mTrans.rotation,0.01f);
			float rotAngle = Quaternion.Angle(mTrans.rotation,lookRotation);
			//Debug.Log(rotAngle);

			//TODO Try to figure out the math that the closer you are to the target, the faster the rotation.
			if(rotAngle > 0.5f){ //lookRotation.Equals(mTrans.rotation
				mTrans.rotation = Quaternion.Slerp(mTrans.rotation,lookRotation, RotationSpeed*Time.deltaTime);
				isMove = true; //Forward changes based on rotation, so need to change velocity with need heading
				//Debug.Log("no in view");
				//Debug.Log( Mathf.Abs(mTrans.rotation.x - lookRotation.x) + " " +  Mathf.Abs(mTrans.rotation.y - lookRotation.y) + " " +  Mathf.Abs(mTrans.rotation.z - lookRotation.z) +  " " +  Mathf.Abs(mTrans.rotation.w - lookRotation.w));
			}else mTrans.rotation = lookRotation;

			InView = (rotAngle <= 10);

			//At max range, only increase speed if it hasn't reached its fastest speed.
			if(dist > SlowDistance && mSpeed < MaxSpeed){
				mSpeed = Mathf.Min(mSpeed + (SpeedUpInc * Time.deltaTime),MaxSpeed);
				isMove = true;
			//Blow max range, decrease till zero
			}else if(dist <= SlowDistance && mSpeed > 0){
				mSpeed = Mathf.Max(mSpeed - (SpeedDownInc * Time.deltaTime),0f);
				isMove = true;
			}

			if(isMove){
				mRBody.velocity = mTrans.forward * mSpeed;
				mRBody.angularVelocity = Vector3.zero; //TODO When drone bumps into something it has a new angular velocity, Add code to fix it with a lerp or something so it can fly straight again.
			}

			//if(dist > StopDistance ) mTrans.position += mTrans.forward * MoveSpeed * Time.deltaTime;
			//if(dist > mSlowDistance){
				//Vector3 velocity = mTrans.forward * MoveSpeed;
				//if(dist <= SlowDistance ) Debug.Log( (dist-StopDistance)/(SlowDistance-StopDistance) );
				//mRBody.velocity = mTrans.forward * MoveSpeed * ((dist > SlowDistance)? 1f : Mathf.Max((dist-StopDistance)/(SlowDistance-StopDistance),0.23f) );

				//if(dist <= SlowDistance){
				//	Debug.Log(velocity);
				//	velocity = Vector3.Lerp(velocity,Vector3.zero,40*Time.deltaTime);

				//	Debug.Log(Vector3.zero);
				//}

				//mRBody.velocity = velocity;
				//mRBody.angularVelocity = Vector3.zero; 	 //TODO When drone bumps into something it has a new angular velocity, Add code to fix it with a lerp or something so it can fly straight again.
			//}else mRBody.velocity = Vector3.zero;
		}
	}
}