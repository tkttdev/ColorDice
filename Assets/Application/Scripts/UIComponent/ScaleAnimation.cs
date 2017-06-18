using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour {

	[SerializeField] private float time = 1.0f;
	[SerializeField] private float afterScale = 1.5f;

	private void Start(){
		iTween.ScaleTo (gameObject, iTween.Hash ("time", time, "x", afterScale, "y", afterScale, "easeType", iTween.EaseType.linear, "loopType", iTween.LoopType.pingPong));
	}
}
