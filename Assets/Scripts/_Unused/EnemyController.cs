using UnityEngine;
using System.Collections;

public class EnemyControllerX : CharController{
	#region Vars
	public GameObject Target;
	private bool TrackEnabled = true;

	//private Rigidbody mRBody;
	private float mRotationSpeed = 1f;
	private float mMoveSpeed = 0.5f;
	private float mStopDistance = 0.22f;
	#endregion

	#region Behavior Events
	//public override void Start () {}

	// Update is called once per frame
	void Update () {
		if(TrackEnabled){
			float dist = Vector3.Distance(this.transform.position,Target.transform.position);
			Quaternion lookRotation = Quaternion.LookRotation(Target.transform.position - this.transform.position);

			//TODO Try to figure out the math that the closer you are to the target, the faster the rotation.
			if(!lookRotation.Equals(this.transform.rotation)){
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation,lookRotation, mRotationSpeed*Time.deltaTime);
			}

			if(dist > mStopDistance ) this.transform.position += this.transform.forward * mMoveSpeed * Time.deltaTime;
		}
		//this.transform.rotation.Sl
		//Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;
	    //Quaternion.AngleAxis (Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;
		//this.transform.LookAt(Target.transform);
		//mRBody.AddForce(Vector3.forward);
	}
	#endregion

	#region CharController
	public override bool ApplyDamage(float damage){
		Debug.Log("Apply Damage to Enemy Ship " + damage);

		if( (this.mHealth -= damage) <= 0){
			this.gameObject.SendMessageUpwards("OnEnemyDestroyed");
			Destroy(this.gameObject);
			return true;
		}
		return false;
	}
	#endregion

}
