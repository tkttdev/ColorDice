using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command {
	protected DiceController dice;
	public virtual void Execute(DiceController diceController){
		dice = diceController;
	}
	public virtual void Undo(){}
}
