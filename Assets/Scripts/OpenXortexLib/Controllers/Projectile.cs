using UnityEngine;
using System.Collections;

//http://answers.unity3d.com/questions/914945/what-replaces-the-glow-effect-in-unity-5.html

public class Projectile : MonoBehaviour {
	#region Vars
	private const int LAYER_PROJECTILE = 8;
	public int targetLayer = 9;
	public float lifeSpan = 3f;
	public float damageStrength = 50f;

	private Rigidbody mRBody;
	private Projectile mProj;
	private IEnumerator mCoroutine = null;
	#endregion

	#region Static methods
	public static Projectile CreateNew(GameObject prefab){
		GameObject go = (GameObject)Instantiate(prefab);
		return go.GetComponent<Projectile>();
	}

	public static Projectile CreateNew(GameObject prefab, Vector3 pos, Quaternion rot, Vector3 velocity,float lifeSpan, float strength, int targetLayer){
		GameObject go = (GameObject)Instantiate(prefab);

		Projectile proj = go.GetComponent<Projectile>();
		proj.Reset(pos,rot,velocity,lifeSpan,strength,targetLayer);

		return proj;
	}
	#endregion

	#region Behavior Events
	void Awake(){
		mRBody = GetComponent<Rigidbody>();
		mProj = GetComponent<Projectile>();
	}

	void Start(){ 
		//Destroy(gameObject,lifeSpan);
	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject.layer == LAYER_PROJECTILE) onHit(null);
		else if(c.gameObject.layer == targetLayer) onHit(c.gameObject.GetComponent<ICharacter>());

		Debug.Log("Projectile trigger with " + c.gameObject.name + " " + c.gameObject.layer);
	}
	#endregion

	#region Get/Set
	public bool IsEnabled(){ return this.gameObject.activeSelf; }
	#endregion

	#region Manage Projectile Life
	public void Reset(Vector3 pos, Quaternion rot, Vector3 velocity,float lifeSpan, float strength, int targetLayer){
		transform.position = pos;
		transform.rotation = rot;

		mRBody.velocity = velocity;

		mProj.targetLayer = targetLayer;
		mProj.lifeSpan = lifeSpan;
		mProj.damageStrength = strength;

		this.gameObject.SetActive(true);
		mCoroutine = AutoDisable(lifeSpan);
		StartCoroutine(mCoroutine);
	}

	private void onHit(ICharacter cc){
		if(cc != null) cc.ApplyDamage(damageStrength);

		if(mCoroutine != null){ StopCoroutine(mCoroutine); mCoroutine = null; }
		this.gameObject.SetActive(false);
	}

	private IEnumerator AutoDisable(float lifeSpan){
		yield return new WaitForSeconds(lifeSpan);
		this.gameObject.SetActive(false);
		mCoroutine = null;
	}
	#endregion
}
