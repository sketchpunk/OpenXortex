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
		ViveControllerMan.TriggerState += new ViveTriggerEventHandler(this.OnControllerTrigger);

		//Parent gameobject to the Vive Controller.
		this.transform.position += new Vector3(0f,0f,0.05f); //Move the object a little so it lines up better with the controller
		ViveControllerMan.attachToController(0,"base",true,this.transform);	//Make this model a child to a section of the controller.
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
