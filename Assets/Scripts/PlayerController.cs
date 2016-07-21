using UnityEngine;
using System.Collections;

public class PlayerController : CharController {
	#region Vars
	public GameObject ProjectilePrefab;
	private float speedFactor = 0.1f; 
	private float fireStrength = 50f;
	private float fireRate = 0.30f;
	private float fireRateCounter = 0;
	private float fireSpeed = 4f;
	private int targetLayer = 9;

	private LaserSight mLaser;
	#endregion

	#region Behavior Events
	public override void Start (){
		mLaser = new LaserSight(this.gameObject);
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
		if(Input.GetButton("Fire1")) FireWeapon();
		if(Input.GetButtonUp("Fire1")) fireRateCounter = 0;

		mLaser.Update();
	}
	#endregion

	#region CharController
	public override bool ApplyDamage(float damage){ return false; }
	#endregion

	#region Misc
	private void FireWeapon(){
		if( (fireRateCounter -= Time.deltaTime) <= 0){
			Projectile.CreateNew(ProjectilePrefab
				,transform.position+(Vector3.forward*1.1f)
				,this.transform.rotation
				,Vector3.forward*fireSpeed
				,3f,fireStrength,targetLayer);

			fireRateCounter = fireRate;
		}
	}
	#endregion
}
