using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceInputHandler : Command {
	public static List<Command> executedCommands = new List<Command>();

	//[SerializeField] private DiceController dice;
	private int commandCount = 0;

	public Command HandleInput(){
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Command cmd = new MoveUp();
			executedCommands.Add (cmd);
			return executedCommands [commandCount++];
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Command cmd = new MoveRight();
			executedCommands.Add (cmd);
			return executedCommands [commandCount++];
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Command cmd = new MoveDown();
			executedCommands.Add (cmd);
			return executedCommands [commandCount++];
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Command cmd = new MoveLeft();
			executedCommands.Add (cmd);
			return executedCommands [commandCount++];
		} else if (Input.GetKey (KeyCode.Backspace)) {
			if (commandCount > 0) {
				executedCommands [commandCount - 1].Undo ();
				commandCount--;
			}
		}

		return null;
	}
}
