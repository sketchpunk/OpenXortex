using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
	#region Variables
	private SpawnPoint[] mPosition;
	public GameObject mPrefab;
	#endregion

	#region Behavior Events
	public void Start (){
		mPosition = GameObject.FindObjectsOfType<SpawnPoint>();

		//for(int i = 0; i < mPosition.Length; i++){ SpawnAt(i); }
		//RandomSpawn();
	}
	#endregion

	#region Spawn Methods
	public GameObject SpawnAt(int i){
		Vector3 pos = mPosition[i].transform.position + mPosition[i].transform.up;
		Quaternion rot = Quaternion.LookRotation(mPosition[i].transform.up);

		var go = (GameObject)Instantiate(mPrefab,pos,rot);
		go.transform.parent = this.transform;

		return go;
	}

	public void RandomSpawn(){
		int i = Random.Range(0,mPosition.Length);
		Debug.Log(i);
		SpawnAt(i);
	}
	#endregion
}
