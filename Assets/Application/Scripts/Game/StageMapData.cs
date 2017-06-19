using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "StageMapData")]
public class StageMapData : ScriptableObject {
	public List<StageMap> stageMap = new List<StageMap> ();
}

[System.Serializable]
public class StageMap{
	public int stageWidth;
	public int stageHeight;
	public int[] stageMap;
}