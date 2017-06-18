using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceInputHandler : Command {
	public static List<Command> executedCommands = new List<Command>();

	[SerializeField] private DiceController dice;
	private int commandCount = 0;

	public Command HandleInput(){
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			executedCommands.Add (new MoveUp ());
			return executedCommands [commandCount++];
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			executedCommands.Add (new MoveRight ());
			return executedCommands [commandCount++];
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			executedCommands.Add (new MoveDown ());
			return executedCommands [commandCount++];
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			executedCommands.Add (new MoveLeft ());
			return executedCommands [commandCount++];
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			if (commandCount > 0) {
				executedCommands [commandCount - 1].Redo ();
				commandCount--;
			}
		}

		return null;
	}
}
