using UnityEngine;
using System.Collections;

public abstract class EnemyObj : ScriptableObject{
	public float Health = 100f;
	public GameObject Target = null;

	public abstract void Init(Transform t);
	public abstract void Update();
	public virtual bool ApplyDamage(float damage){ return false; }
}
