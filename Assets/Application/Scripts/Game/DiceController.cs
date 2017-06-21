using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour {

	[SerializeField]private float sideLength = 2f;
	private bool isMove = false;
	private Vector3 startPos;
	private Vector3 purposePos;
	private Vector3 rotateDir;
	private DiceInputHandler inputHandler = new DiceInputHandler ();

	private void Start(){
	}


	private void Update () {
		if (!isMove) {
			Command cmd = inputHandler.HandleInput ();
			if (cmd != null) {
				cmd.Execute (this);
			}
		}
	}

	public void Move(Vector3 rotateDir, float speed = 1.5f){
		isMove = true;
		purposePos = transform.position + rotateDir*sideLength;
		iTween.MoveTo (gameObject, iTween.Hash ("x", purposePos.x, "z", purposePos.z, "oncomplete", "MoveEnd", "time", 1f / speed, "easeType", iTween.EaseType.easeOutCubic));
	}

	private void MoveEnd(){
		isMove = false;
		transform.position = purposePos;
	}
}
