using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
	#region Variables
	private SpawnPoint[] mPosition;
	public GameObject mPrefab;
	public const string ON_ENEMY_DESTROYED = "OnEnemyDestroyed";
	private float mSpawnDistance = 0.3f;
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
	private IEnumerator SpawnAt(int i){
		yield return new WaitForSeconds(1.2f);

		Vector3 pos = mPosition[i].transform.position + (mPosition[i].transform.up * mSpawnDistance);
		Quaternion rot = Quaternion.LookRotation(mPosition[i].transform.up);

		var go = (GameObject)Instantiate(mPrefab,pos,rot);
		go.transform.parent = this.transform;
	}

	public void RandomSpawn(){
		int i = Random.Range(0,mPosition.Length);
		Debug.Log(i);
		mPosition[i].PlayEffect();
		StartCoroutine(SpawnAt(i));
	}
	#endregion

	public void OnEnemyDestroyed(){
		RandomSpawn();
	}
}
