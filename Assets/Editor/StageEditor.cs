using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StageEditor : EditorWindow {
	private const string stageMapDataPath = "ScriptableObject/StageMapData";
	private const string stageRuleDataPath = "ScriptableObject/StageRuleData";
	private const string stageFolderPath = "Prefab/Stage";

	private SerializedObject stageMapData;
	private SerializedProperty stageMapInfoProp;
	private SerializedProperty stageWidthProp;
	private SerializedProperty stageHeightProp;
	private SerializedProperty stageMapProp;

	private SerializedObject stageRuleData;
	private SerializedProperty stageRuleProp;
	private SerializedProperty redCountProp;
	private SerializedProperty blueCountProp;
	private SerializedProperty yellowCountProp;

	private int selectStageIndex = -1;
	private int width = 1;
	private int height = 1;
	private int[] stageMap = new int[1];
	private int beforeWidth = 1;
	private int beforeHeight = 1;
	private float boxWidth = 30f;
	private float boxHeight = 30f;
	private float colorPaletteWidth = 60f;
	private float colorPaletteHeight = 60f;
	private int selectStageComponentIndex = 0;
	private Vector2 scrollPos;
	private Event e;
	private bool isEdited = false;
	private Color[] stageColor = new Color[]{Color.black, Color.gray, Color.white, Color.red, Color.blue, Color.yellow};
	private int redCount, blueCount, yellowCount;

	[MenuItem("Window/StageEditor")]
	public static void Open(){
		var window = GetWindow<StageEditor> ();
		window.minSize = window.maxSize = new Vector2 (700, 700);
	}

	private void OnEnable(){
		stageMapData = new SerializedObject (Resources.Load (stageMapDataPath) as StageMapData);
		stageMapInfoProp = stageMapData.FindProperty ("stageMapInfo");
		stageRuleData = new SerializedObject (Resources.Load (stageRuleDataPath) as StageRuleData);
		stageRuleProp = stageRuleData.FindProperty ("stageRule");
	}

	private void OnGUI(){
		e = Event.current;

		if (selectStageIndex > -1 && selectStageIndex < stageMapInfoProp.arraySize) {
			DisplayStageInfo ();
			DisplayStageEditor ();
			DisplayStageComponent ();
		}

		DisplayStageList ();
		DisplayTools ();

		MoveStageList ();
	}

	private void SetStageMapProp(int stageId){
		stageWidthProp = stageMapInfoProp.GetArrayElementAtIndex (stageId).FindPropertyRelative ("width");
		stageHeightProp = stageMapInfoProp.GetArrayElementAtIndex (stageId).FindPropertyRelative ("height");
		stageMapProp = stageMapInfoProp.GetArrayElementAtIndex (stageId).FindPropertyRelative ("map");
	}

	private void SetStageRuleProp(int stageId){
		redCountProp = stageRuleProp.GetArrayElementAtIndex (stageId).FindPropertyRelative ("redCount");
		blueCountProp = stageRuleProp.GetArrayElementAtIndex (stageId).FindPropertyRelative ("blueCount");
		yellowCountProp = stageRuleProp.GetArrayElementAtIndex (stageId).FindPropertyRelative ("yellowCount");
	}

	private void SetEditableValues(){
		width = Mathf.Clamp (stageWidthProp.intValue, 1, 20);
		height = Mathf.Clamp (stageHeightProp.intValue, 1, 20);
		stageMap = new int[width * height];
		for (int i = 0; i < stageMapProp.arraySize; i++) {
			stageMap[i] = stageMapProp.GetArrayElementAtIndex (i).intValue;
		}

		redCount = redCountProp.intValue;
		blueCount = blueCountProp.intValue;
		yellowCount = yellowCountProp.intValue;
	}

	private void SaveStage(){
		if (selectStageIndex < 0) {
			return;
		}
		stageWidthProp.intValue = width;
		stageHeightProp.intValue = height;
		stageMapProp.arraySize = stageMap.Length;
		for (int i = 0; i < stageMap.Length; i++) {
			stageMapProp.GetArrayElementAtIndex (i).intValue = stageMap [i];
		}
		redCountProp.intValue = redCount;
		blueCountProp.intValue = blueCount;
		yellowCountProp.intValue = yellowCount;
		stageMapData.ApplyModifiedProperties ();
		stageRuleData.ApplyModifiedProperties ();
		isEdited = false;
	}

	private void DisplayStageInfo(){
		GUI.BeginGroup (new Rect (0, 0, 600, 40));
		EditorGUI.BeginChangeCheck ();
		GUI.Box (new Rect (0, 0, 600, 20), "");
		width = EditorGUI.IntField (new Rect (0, 0, 300, 20), "Width", width);
		height = EditorGUI.IntField (new Rect (300, 0, 300, 20), "Height", height);

		if (EditorGUI.EndChangeCheck ()) {
			width = Mathf.Clamp (width, 1, 20);
			height = Mathf.Clamp (height, 1, 20);
			if (width != beforeWidth || height != beforeHeight) {
				stageMap = new int[width * height];
				beforeWidth = width;
				beforeHeight = height;
				Debug.Log ("change");
				isEdited = true;
			}
			Repaint ();
		}

		EditorGUI.BeginChangeCheck ();
		GUI.Box (new Rect (0, 20, 600, 20), "");
		redCount = EditorGUI.IntField (new Rect (0, 20, 200, 20), "Red Count", redCount);
		blueCount = EditorGUI.IntField (new Rect (200, 20, 200, 20), "Blue Count", blueCount);
		yellowCount = EditorGUI.IntField (new Rect (400, 20, 200, 20), "Yellow Count", yellowCount);
		if (EditorGUI.EndChangeCheck ()) {
			isEdited = true;
		}

		GUI.EndGroup ();
	}

	private void DisplayStageEditor(){
		GUI.BeginGroup (new Rect (0, 40, 600, 600));
		EditorGUI.DrawRect (new Rect (0, 0, 600, 600), Color.gray);
		float startBoxPosX = 300f - (width * boxWidth) / 2f;
		float startBoxPosY = 300f - (height * boxHeight) / 2f; 
		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				DrawFrameBox (new Rect (boxWidth * j + startBoxPosX, boxHeight * i + startBoxPosY, boxWidth, boxHeight), Color.white, stageColor[stageMap [i * width + j]], 0.5f);
			}
		}
		StageEditorClickEvent(startBoxPosX, startBoxPosY);
		GUI.EndGroup ();
	}

	private void StageEditorClickEvent(float startBoxPosX, float startBoxPosY){
		if ((e.type == EventType.MouseDrag || e.type == EventType.MouseDown) && e.mousePosition.x >= startBoxPosX && e.mousePosition.x <= startBoxPosX + width*boxWidth && e.mousePosition.y >= startBoxPosY && e.mousePosition.y <= startBoxPosY + height*boxHeight) {
			int x = (int)((e.mousePosition.x - startBoxPosX) / boxWidth);
			int y = (int)((e.mousePosition.y - startBoxPosY) / boxHeight);
			stageMap [y * width + x] = selectStageComponentIndex;
			Repaint ();
			isEdited = true;
		}
	}

	private void DisplayStageComponent(){
		GUI.BeginGroup (new Rect (0, 640, 600, 60));
		for (int i = 0; i < stageColor.Length; i++) {
			Color flameColor = Color.black;
			if (selectStageComponentIndex == i) {
				flameColor = Color.blue;
			}
			DrawFrameBox(new Rect(colorPaletteWidth * i, 0, colorPaletteWidth, colorPaletteHeight), flameColor, stageColor[i], 1.5f);
		}
		StageComponentClickEvent ();
		GUI.EndGroup ();
	}

	private void StageComponentClickEvent(){
		if (e.type == EventType.MouseDown && (int)(e.mousePosition.x / colorPaletteWidth) != selectStageComponentIndex) {
			selectStageComponentIndex = (int)(e.mousePosition.x / colorPaletteWidth);
			Repaint ();
		}
	}

	private void DisplayStageList(){
		GUI.BeginGroup (new Rect (600, 0, 100, 500));
		GUI.Box (new Rect (0, 0, 100, 500), "");
		for (int i = 0; i < stageMapInfoProp.arraySize; i++) {
			bool flag = (selectStageIndex == i);
			flag = GUILayout.Toggle (flag, "Stage" + i.ToString (), "Ol Elem");
			if (flag != (selectStageIndex == i)) {
				ChangeEditStage (i);
			}
		}
		GUI.EndGroup();
	}

	private void DisplayTools(){
		GUI.BeginGroup (new Rect (600, 500, 100, 200));
		if (GUI.Button (new Rect(0,0,100,20),"Add Stage")) {
			stageMapInfoProp.arraySize = stageMapInfoProp.arraySize + 1;
			stageMapData.ApplyModifiedProperties ();
			stageRuleProp.arraySize = stageRuleProp.arraySize + 1;
			stageRuleData.ApplyModifiedProperties ();
		}
		if (GUI.Button (new Rect(0,20,100,20),"Remove All")) {
			stageMapInfoProp.ClearArray ();
			stageMapData.ApplyModifiedProperties ();
			stageRuleProp.ClearArray ();
			stageRuleData.ApplyModifiedProperties ();
			selectStageIndex = -1;
		}
		if (GUI.Button (new Rect (0, 40, 100, 20), "Save Stage")) {
			SaveStage ();
		}
		GUI.EndGroup ();
	}

	private void MoveStageList(){
		if(e.type == EventType.KeyDown && e.keyCode == KeyCode.DownArrow){
			if (selectStageIndex < stageMapInfoProp.arraySize - 1) {
				ChangeEditStage (selectStageIndex + 1);
			}
			Repaint ();
		} 
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.UpArrow) {
			if (selectStageIndex > 0) {
				ChangeEditStage (selectStageIndex - 1);
			}
			Repaint ();
		}
	}

	private void ChangeEditStage(int stageIndex){
		if (isEdited) {
			bool result = EditorUtility.DisplayDialog ("注意", "変更が保存されていません．保存しますか？", "保存", "破棄");
			if (result) {
				SaveStage ();
			} else {
				isEdited = false;
			}
		}
		selectStageIndex = stageIndex;
		SetStageMapProp (selectStageIndex);
		SetStageRuleProp (selectStageIndex);
		SetEditableValues ();
	}

	private void DrawFrameBox(Rect rect, Color frameColor, Color contentColor, float frameWidth = 1){
		EditorGUI.DrawRect (rect, frameColor);
		EditorGUI.DrawRect (new Rect (rect.x + frameWidth, rect.y + frameWidth, rect.width - 3f * frameWidth, rect.height - 3f * frameWidth), contentColor);
		//なぜ*2以上でないと正しく枠が表示されないのか，これがわからない，
	}
}
