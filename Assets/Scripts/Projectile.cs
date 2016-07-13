using UnityEngine;
using System.Collections;

//http://answers.unity3d.com/questions/914945/what-replaces-the-glow-effect-in-unity-5.html

public class Projectile : MonoBehaviour {
	#region Vars
	private const int LAYER_PROJECTILE = 8;
	public int targetLayer = 9;
	public float lifeSpan = 3f;
	public float damageStrength = 50f;
	#endregion

	#region Static methods
	public static GameObject CreateNew(GameObject prefab, Vector3 pos, Quaternion rot, Vector3 velocity,float lifeSpan, float strength, int targetLayer){
		GameObject go = (GameObject)Instantiate(prefab,pos,rot);

		Rigidbody rb = go.GetComponent<Rigidbody>();
		rb.velocity = velocity;

		Projectile proj = go.GetComponent<Projectile>();
		proj.targetLayer = targetLayer;
		proj.lifeSpan = lifeSpan;
		proj.damageStrength = strength;

		return go;
	}
	#endregion

	#region Behavior Events
	void Start (){ Destroy(gameObject,lifeSpan); }

	void OnTriggerEnter(Collider c){
		if(c.gameObject.layer == LAYER_PROJECTILE) Destroy(this.gameObject);
		else if(c.gameObject.layer == targetLayer){
			CharController controller = c.gameObject.GetComponent<CharController>();
			if(controller != null) controller.ApplyDamage(damageStrength);
			Destroy(this.gameObject);
		}

		Debug.Log("Projectile trigger with " + c.gameObject.name + " " + c.gameObject.layer);
	}
	#endregion
}
