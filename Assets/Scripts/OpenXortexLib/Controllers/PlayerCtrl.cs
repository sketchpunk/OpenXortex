using UnityEngine;
using System.Collections;
using SP.Effects;
using SP.Attacking;
using SP.VR;

[RequireComponent(typeof(LineRenderer))] //For Laser
public class PlayerCtrl : MonoBehaviour {
	#region Vars
	private static PlayerCtrl mInstance = null;
	private Weapon mWeapon = null;
	private LaserSight mLaser;
	public GameObject ProjectilePrefab;
	public bool AutoAttachToVive = true;

	private const float weaponForwardOffset	= 0.13f;
	private const float weaponTopOffset		= -0.015f;
	#endregion

	public static PlayerCtrl Instance(){ return mInstance; }

	#region Behaviour Events
	void Awake(){ mInstance = this; Debug.Log("PlayerCtrl Awake"); }

	void Start(){
		Debug.Log("PlayerCtrl Start");

		mLaser	= new LaserSight(this.gameObject, weaponForwardOffset, weaponTopOffset);
		mWeapon	= new Weapon(this.transform, ProjectilePrefab, weaponForwardOffset, weaponTopOffset);
		mWeapon.targetLayer = Consts.LAYER_ENEMIES;

		ViveControllerMan.TriggerState += new ViveTriggerEventHandler(this.OnControllerTrigger);

		//Parent gameobject to the Vive Controller.
		if(AutoAttachToVive) ViveControllerMan.attachToController(0,"base",true,new Vector3(0f,0.024f,-0.215f),this.transform);
	}

	void Update(){
		SP.Movement.WASDMove.onUpdate(transform,0.01f); //TODO, Remove this is only for debugging without vive controller.
		mWeapon.onUpdate();
		mLaser.onUpdate();
	}
	#endregion

	#region vive
	public void OnControllerTrigger(uint index,int state,float axis){
		switch(state){
			case ViveControllerMan.STATE_DOWN:
			case ViveControllerMan.STATE_ACTIVE:	mWeapon.SetActive(true);	break;
			case ViveControllerMan.STATE_UP:		mWeapon.SetActive(false);	break;
		}
	}
	#endregion
}
