using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour {

	private void Update(){
		if (Input.GetMouseButtonDown (0)) {
			AppSceneManager.Instance.GoGame ();
		}
	}
}
