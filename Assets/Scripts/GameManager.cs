using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour{
	void Start(){
		Debug.Log("Game Manager Start");
		ViveControllerMan.Init();
	}

	void Update(){
		ViveControllerMan.UpdateState();
	}
}
