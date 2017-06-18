using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : Command {
	public override void Execute (DiceController diceController) {
		base.Execute (diceController);
		dice.Move (Vector3.back);
	} 

	public override void Undo () {
		dice.Move (Vector3.forward, 3f);
	}
}
