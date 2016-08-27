using UnityEngine;
using System.Collections;
using SP.Movement;

//http://docs.unity3d.com/ScriptReference/Mathf.SmoothDamp.html
//http://gamedev.stackexchange.com/questions/55725/move-object-to-position-using-velocity
//http://answers.unity3d.com/questions/181969/speed-based-on-distance.html


[CreateAssetMenu (menuName="Xortex/Enemy Tracker")]
public class EnemyTrackerObj : EnemyObj{
	#region Public Properties
	public float SlowDistance = 0.7f;
	public float RotationSpeed = 1f;
	public float MaxSpeed = 0.5f;
	public float SpeedUpInc = 0.4f;
	public float SpeedDownInc = 0.3f;
	#endregion

	#region Private vars
	private Transform mTrans;
	private Rigidbody mRBody;
	private SpaceTracker mTracker;
	private bool TrackEnabled = true;
	#endregion

	#region Inits / Setter / Getters
	public override void Init(Transform t){
		mTrans = t;
		mRBody = mTrans.gameObject.GetComponent<Rigidbody>();
		mTracker = new SpaceTracker(t.gameObject,null);
		/* TODO, FIX Drone Object in editor with new default values.
		mTracker.SlowDistance = SlowDistance;
		mTracker.RotationSpeed = RotationSpeed;
		mTracker.MaxSpeed = MaxSpeed;
		mTracker.SpeedUpInc = SpeedUpInc;
		mTracker.SpeedDownInc = SpeedDownInc;
		*/
	}

	public override void SetTarget(GameObject target){ mTracker.Target = target; }
	#endregion

	public override bool ApplyDamage(float damage){
		Debug.Log("Apply Damage to Enemy Ship " + damage);

		if( (this.Health -= damage) <= 0){
			mTrans.gameObject.SendMessageUpwards(SpawnManager.ON_ENEMY_DESTROYED);
			Destroy(mTrans.gameObject);
			return true;
		}
		return false;
	}

	public override void Update(){ if(TrackEnabled) mTracker.onUpdate(); }
}
