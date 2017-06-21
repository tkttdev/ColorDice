using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "StageMapData")]
public class StageMapData : ScriptableObject {
	public List<StageMapInfo> stageMapInfo = new List<StageMapInfo> ();
}

[System.Serializable]
public class StageMapInfo{
	public int width;
	public int height;
	public int[] map;
}