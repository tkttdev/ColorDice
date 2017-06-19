using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : Command {
	public override void Execute (DiceController diceController) {
		base.Execute (diceController);
		dice.Move (Vector3.right);
	} 

	public override void Undo () {
		dice.Move (Vector3.left, 3f);
	}
}
