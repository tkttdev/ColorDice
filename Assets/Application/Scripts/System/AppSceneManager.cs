using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppSceneManager : Singleton<AppSceneManager> {
	private enum SceneType : int {
		TITLE_SCENE = 0,
		GAME_SCENE = 1,
	}

	protected override void Awake () {
		base.Awake ();
		DontDestroyOnLoad (gameObject);
	}

	public void GoTitle(){
		SceneManager.LoadScene ((int)SceneType.TITLE_SCENE);
	}

	public void GoGame(){
		SceneManager.LoadScene ((int)SceneType.GAME_SCENE);
	}
}
