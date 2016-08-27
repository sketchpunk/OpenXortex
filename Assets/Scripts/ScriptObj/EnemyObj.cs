using UnityEngine;
using System.Collections;

public abstract class EnemyObj : ScriptableObject{
	public float Health = 100f;

	public abstract void Init(Transform t);
	public abstract void Update();
	public abstract void SetTarget(GameObject target);
	public virtual bool ApplyDamage(float damage){ return false; }
}
