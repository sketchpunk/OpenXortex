using UnityEngine;
using System.Collections;
using SP.VR;

public class GameManager : MonoBehaviour{
	void Awake(){
		Debug.Log("Game Manager Start");
		ViveControllerMan.Init();
	}

	void Start(){
	}

	void Update(){
		ViveControllerMan.UpdateState();
	}
}
