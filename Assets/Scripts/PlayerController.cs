using UnityEngine;
using System.Collections;

public class PlayerController : CharController {
	#region Vars
	public GameObject ProjectilePrefab;
	private float speedFactor = 0.1f;

	private bool mIsFiring = false;
	private float fireStrength = 50f;
	private float fireRate = 0.30f;
	private float fireRateCounter = 0;
	private float fireSpeed = 4f;
	private int targetLayer = 9;

	private LaserSight mLaser;
	private static PlayerController mInstance = null;
	#endregion

	#region Behavior Events
	public override void Start (){
		mLaser = new LaserSight(this.gameObject);
		ViveControllerMan.TriggerState += new ViveTriggerEventHandler(this.OnControllerTrigger);
		mInstance = this;
	}
	
	// Update is called once per frame
	void Update (){
		//...........................................
		//Handle movement
		float mx = Input.GetAxis("Horizontal")
			,my = Input.GetAxis("Vertical");

		if(mx < -0.02f || mx > 0.02) transform.position += ((mx > 0)? Vector3.right : Vector3.left) * speedFactor;
		if(my < -0.02f || my > 0.02) transform.position += ((my > 0)? Vector3.up : Vector3.down) * speedFactor;

		//...........................................
		//Handle Attacking
		if(mIsFiring || Input.GetButton("Fire1")) FireWeapon();
		if(Input.GetButtonUp("Fire1")) fireRateCounter = 0;

		mLaser.Update();
	}
	#endregion

	public void OnControllerTrigger(uint index,int state,float axis){
		switch(state){
			case ViveControllerMan.STATE_DOWN:
			case ViveControllerMan.STATE_ACTIVE:
				mIsFiring = true;
				break;
			case ViveControllerMan.STATE_UP:
				mIsFiring = false;
				fireRateCounter = 0;
				break;
		}
	}

	public static GameObject Instance(){ return mInstance.gameObject; }

	#region CharController
	public override bool ApplyDamage(float damage){ return false; }
	#endregion

	#region Misc
	private void FireWeapon(){
		if( (fireRateCounter -= Time.deltaTime) <= 0){
			ProjectileManager.Instance.Fire(ProjectilePrefab
				,transform.position + (transform.forward * 0.1f)
				,this.transform.rotation
				,transform.forward*fireSpeed
				,3f,fireStrength,targetLayer);
			/*
			Projectile.CreateNew(ProjectilePrefab
				,transform.position+(Vector3.forward*1.1f)
				,this.transform.rotation
				,Vector3.forward*fireSpeed
				,3f,fireStrength,targetLayer);
			*/

			fireRateCounter = fireRate;
		}
	}
	#endregion
}
