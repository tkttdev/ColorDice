using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageTile : int {
	NONE = 0,
	RED = 1,
	BLUE = 2,
	YELLOW = 3,
}

public class StageManager : Singleton<StageManager> {
	[SerializeField] private StageRuleData stageRuleData;
	[SerializeField] private StageMapData stageMapData;
}
