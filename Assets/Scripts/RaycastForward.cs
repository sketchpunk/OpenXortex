using UnityEngine;
using System.Collections;

public class RaycastForward : MonoBehaviour{
	private float mDistance = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		float distance;

		//Debug Raycast in editor.
		Vector3 origin = this.transform.position + (this.transform.forward);

		Vector3 forward = this.transform.TransformDirection(Vector3.forward) * mDistance;
		Debug.DrawRay(origin,forward,Color.green);

		if(Physics.Raycast(origin,forward,out hit,mDistance)){
			distance = hit.distance;
			print(distance + " " + hit.collider.gameObject.name);
		}
	}
}
