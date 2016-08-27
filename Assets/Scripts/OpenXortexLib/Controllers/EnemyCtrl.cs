using UnityEngine;
using System.Collections;

public class EnemyCtrl : MonoBehaviour, ICharacter{
	public EnemyObj EnemyType;

	void Start(){
		EnemyType.Init(this.transform);
		EnemyType.SetTarget(PlayerCtrl.Instance().gameObject);
	}

	void Update(){
		EnemyType.Update();
	}

	public bool ApplyDamage(float points){
		return EnemyType.ApplyDamage(points);
	}
}
