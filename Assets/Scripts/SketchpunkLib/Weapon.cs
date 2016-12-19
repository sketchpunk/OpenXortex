using UnityEngine;
using System.Collections;

//TODO Add Powerups that may change the prefab, strength, etc. Maybe use Scriptable Objects to define each projectile type.
namespace SP.Attacking{
	public class Weapon{
		#region Private vars
		private Transform parentTrans = null;
		private GameObject projectilePrefab = null;
		private bool mIsFiring = false;
		private float fireRateCounter = 0;
		#endregion

		#region Properties / Constructor
		public int targetLayer = 0;

		public float lifeSpan = 3f;
		public float fireStrength = 50f;
		public float fireRate = 0.30f;
		public float fireSpeed = 4f;

		public float offsetForward = 0.1f;
		public float offsetUp = 0f;

		public Weapon(Transform parent, GameObject projectile, float forOffset, float upOffset){
			parentTrans = parent;
			projectilePrefab = projectile;
			offsetForward = forOffset;
			offsetUp = upOffset;
		}
		#endregion

		#region Methods
		public void SetActive(bool isOn){
			mIsFiring = isOn;
			if(!isOn) fireRateCounter = 0;
		}

		public void onUpdate(){
			if(mIsFiring || Input.GetButton("Fire1")) Shoot();
			if(Input.GetButtonUp("Fire1")) fireRateCounter = 0;
		}
		#endregion

		private void Shoot(){
			if( (fireRateCounter -= Time.deltaTime) <= 0 ){
				fireRateCounter = fireRate; //Reset Counter for the next shot
				ProjectileManager.Instance.Fire(projectilePrefab
					,parentTrans.position + (parentTrans.forward * offsetForward) + (parentTrans.up * offsetUp)
					,parentTrans.rotation
					,parentTrans.forward * fireSpeed
					,lifeSpan,fireStrength,targetLayer);
				
			}
		}
	}
}