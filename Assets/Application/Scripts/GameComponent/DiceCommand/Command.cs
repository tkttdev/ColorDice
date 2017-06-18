using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour {
	protected DiceController dice;
	public virtual void Execute(DiceController diceController){}
	public virtual void Redo(){}
}
