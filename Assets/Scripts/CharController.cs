﻿using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {
	protected float mHealth = 100f;


	public virtual void Start () {
		Debug.Log("Start Character");
	}

	public virtual bool ApplyDamage(float damage){ return false; }
}
