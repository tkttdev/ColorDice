using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "StageRuleData")]
public class StageRuleData : ScriptableObject {
	public List<StageRule> stageRule = new List<StageRule>();

	[System.Serializable]
	public class StageRule {
		public int redCount = 0;
		public int blueCount = 0;
		public int yellowCount = 0;
	}
}
