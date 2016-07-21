using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileManager : MonoBehaviour {
	private List<Projectile> mCache = new List<Projectile>();
	private int mCacheLen = 0;
	public static ProjectileManager Instance;

	void Start(){
		ProjectileManager.Instance = this;
	}
	
	void Update(){}


	//public static GameObject CreateNew(GameObject prefab, Vector3 pos, Quaternion rot, Vector3 velocity,float lifeSpan, float strength, int targetLayer){

	public void Fire(GameObject prefab, Vector3 pos, Quaternion rot, Vector3 velocity,float lifeSpan, float strength, int targetLayer){
		Projectile p = findAvailableItem();
		if(!p){
			Debug.Log("Create new Projectile");
			p = Projectile.CreateNew(prefab);
			p.gameObject.transform.parent = this.transform;
			mCache.Add(p);
			mCacheLen++;
		}

		p.Reset(pos,rot,velocity,lifeSpan,strength,targetLayer);
	}

	private Projectile findAvailableItem(){
		for(int i=0; i < mCacheLen; i++){
			if(!mCache[i].IsEnabled()) return mCache[i];
		}
		return null;
	}


}
