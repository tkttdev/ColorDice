using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : Command {
	public override void Execute (DiceController diceController) {
		base.Execute (diceController);
		dice.Move (Vector3.forward);
	} 

	public override void Undo () {
		dice.Move (Vector3.back, 3f);
	}
}
