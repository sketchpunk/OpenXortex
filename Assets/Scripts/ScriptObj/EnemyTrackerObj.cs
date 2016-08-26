using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName="Xortex/Enemy Tracker")]
public class EnemyTrackerObj : EnemyObj{
	private Transform mTrans;
	private Rigidbody mRBody;

	//public static EnemyTrackerObj Create(GameObject go){
	//	var obj = ScriptableObject.CreateInstance<EnemyTrackerObj>();
	//	obj.Init(go);
	//	return obj;
	//}

	//private Rigidbody mRBody;
	private bool TrackEnabled = true;
	public float RotationSpeed = 1f;
	public float MoveSpeed = 0.5f;
	public float StopDistance = 0.22f;
	public float SlowDistance = 0.4f;

	public override void Init(Transform t){
		mTrans = t;
		mRBody = mTrans.gameObject.GetComponent<Rigidbody>();
	}

	public override bool ApplyDamage(float damage){
		Debug.Log("Apply Damage to Enemy Ship " + damage);

		if( (this.Health -= damage) <= 0){
			mTrans.gameObject.SendMessageUpwards(SpawnManager.ON_ENEMY_DESTROYED);
			Destroy(mTrans.gameObject);
			return true;
		}
		return false;
	}

	public override void Update() {
		if(!TrackEnabled || Target == null) return;

		float dist = Vector3.Distance(mTrans.position,Target.transform.position);
		Quaternion lookRotation = Quaternion.LookRotation(Target.transform.position - mTrans.position);

		//TODO Try to figure out the math that the closer you are to the target, the faster the rotation.
		if(!lookRotation.Equals(mTrans.rotation)){
			mTrans.rotation = Quaternion.Slerp(mTrans.rotation,lookRotation, RotationSpeed*Time.deltaTime);
		}

		//if(dist > StopDistance ) mTrans.position += mTrans.forward * MoveSpeed * Time.deltaTime;
		if(dist > StopDistance){
			//if(dist <= SlowDistance ) Debug.Log( (dist-StopDistance)/(SlowDistance-StopDistance) );
			mRBody.velocity = mTrans.forward * MoveSpeed * ((dist > SlowDistance)? 1f : Mathf.Max((dist-StopDistance)/(SlowDistance-StopDistance),0.3f) );
			mRBody.angularVelocity = Vector3.zero; 	 //TODO When drone bumps into something it has a new angular velocity, Add code to fix it with a lerp or something so it can fly straight again.
		}else mRBody.velocity = Vector3.zero;

		//this.transform.rotation.Sl
		//Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;
	    //Quaternion.AngleAxis (Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;
		//this.transform.LookAt(Target.transform);
		//mRBody.AddForce(Vector3.forward);
	}
}
