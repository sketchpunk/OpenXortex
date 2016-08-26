using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
	#region Variables
	private SpawnPoint[] mPosition;
	public GameObject mPrefab;
	public const string ON_ENEMY_DESTROYED = "OnEnemyDestroyed";
	private float mSpawnDistance = 0.08f;
	#endregion

	#region Behavior Events
	public void Start (){
		mPosition = GameObject.FindObjectsOfType<SpawnPoint>();
		//Debug.Log(mPosition.Length);
		//for(int i = 0; i < mPosition.Length; i++){ SpawnAt(i); }
		//RandomSpawn();
	}
	#endregion

	#region Spawn Methods
	public GameObject SpawnAt(int i){
		Vector3 pos = mPosition[i].transform.position + (mPosition[i].transform.up * mSpawnDistance);
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

	public void OnEnemyDestroyed(){
		RandomSpawn();
	}
}
