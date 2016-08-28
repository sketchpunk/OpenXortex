using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
	private ParticleSystem mParticle;
	void Start(){
		mParticle = GetComponent<ParticleSystem>();
	}

	public void PlayEffect(){ mParticle.Play(); }
	//void Update(){}
}
