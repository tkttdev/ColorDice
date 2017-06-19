using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StageEditor : EditorWindow {
	private const string stageMapDataPath = "ScriptableObject/StageMapData";
	private const string stagePath = "Prefab/Stage/Stage";

	private static StageMapData stageMapData;

	private enum colorType : int {
		GLAY,
		BLUE,
	}

	private int selectStageIndex = -1;
	private int width = 1;
	private int height = 1;
	private int beforeWidth = 1;
	private int beforeHeight = 1;
	private Vector2 scrollPos;
	private Event e;
	private bool isEdit = false;
	private Color[] stageColor = new Color[]{Color.white, Color.red, Color.blue, Color.yellow};

	[MenuItem("Window/StageEditor")]
	public static void Open(){
		if (stageMapData == null) {
			stageMapData = Resources.Load (stageMapDataPath) as StageMapData;
		}
		var window = GetWindow<StageEditor> ();
		window.minSize = window.maxSize = new Vector2 (700, 700);
	}

	private void OnGUI(){
		e = Event.current;

		DisplayStageEditor ();

		DisplayStageList ();
		DisplayTools ();

		MoveStageList (e);
	}

	private void DisplayStageEditor(){
		GUI.BeginGroup (new Rect (0, 0, 600, 700));
		EditorGUI.BeginChangeCheck ();
		width = EditorGUI.IntField (new Rect (0, 0, 300, 20), "Width", width);
		height = EditorGUI.IntField (new Rect (300, 0, 300, 20), "Height", height);
		if (EditorGUI.EndChangeCheck ()) {
			width = Mathf.Clamp (width, 1, 20);
			height = Mathf.Clamp (height, 1, 20);
			if (width != beforeWidth || height != beforeHeight) {
				
			}
			Repaint ();
		}
		//GUI.
		float boxWidth, boxHeight;
		boxWidth = 600f / width;
		boxHeight = 600f / height;

		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				GUI.Box (new Rect (0 + boxWidth * j, 20 + boxHeight * i, boxWidth, boxHeight), "");
				EditorGUI.DrawRect (new Rect (0 + boxWidth * j, 20 + boxHeight * i, boxWidth - 1, boxHeight - 1), Color.white);
			}
		}
		GUI.EndGroup ();
	}

	private void DisplayStageList(){
		GUI.BeginGroup (new Rect (600, 0, 100, 500));
		GUI.Box (new Rect (0, 0, 100, 500), "");
		for (int i = 0; i < stageMapData.stageMap.Count; i++) {
			bool flag = (selectStageIndex == i);
			flag = GUILayout.Toggle (flag, "Stage" + i.ToString (), "Ol Elem");
			if (flag != (selectStageIndex == i)) {
				selectStageIndex = i;
			}
		}
		GUI.EndGroup();
	}

	private void DisplayTools(){
		GUI.BeginGroup (new Rect (600, 500, 100, 200));
		if (GUI.Button (new Rect(0,0,100,20),"Add Stage")) {
			stageMapData.stageMap.Add (new StageMap ());
		}
		if (GUI.Button (new Rect(0,20,100,20),"Remove All")) {
			stageMapData.stageMap.Clear ();
			selectStageIndex = -1;
		}
		string[] tools = new string[]{ "aaa", "bbb" };
		//GUI.Toolbar(new Rect(0, 40, 100, 20), 0, tools);
		colorType t = colorType.BLUE;
		EditorGUI.EnumPopup(new Rect(0, 40, 100, 20), t);
		GUI.EndGroup ();
	}

	private void MoveStageList(Event e){
		if(e.type == EventType.KeyDown && e.keyCode == KeyCode.DownArrow){
			if (selectStageIndex < stageMapData.stageMap.Count - 1) {
				selectStageIndex++;
			}
			Repaint ();
		} 
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.UpArrow) {
			if (selectStageIndex > 0) {
				selectStageIndex--;
			}
			Repaint ();
		}
	}
}
