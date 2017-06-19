using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : Command {
	public override void Execute (DiceController diceController) {
		base.Execute (diceController);
		dice.Move (Vector3.left);
	} 

	public override void Undo () {
		dice.Move (Vector3.right, 3f);
	}
}
