using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveMode : int {
	STOP = 0,
	MOVE_UP = 1,
	MOVE_RIGHT = 2,
	MOVE_DOWN = 3,
	MOVE_LEFT = 4,
}

public class DiceController : MonoBehaviour {

	[SerializeField]private float speed = 3;
	private MoveMode moveMode = MoveMode.STOP;
	private float moveTime = 0f;
	private Vector3 rotateAxis = Vector3.zero;
	private Vector3 startPos = Vector3.zero;
	private Vector3 endPos = Vector3.zero;
	private DiceInputHandler inputHandler = new DiceInputHandler ();

	void Start(){
	}


	void Update () {
		if (moveMode == MoveMode.STOP) {
			Command cmd = inputHandler.HandleInput ();
			if (cmd != null) {
				cmd.Execute (this);
			}
		}
	}


}
