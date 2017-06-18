using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T  : Singleton<T> {

	private static T instance;

	public static T Instance {
		get {
			if (instance == null) {	
				instance = (T)FindObjectOfType (typeof(T));
			}
			if (instance == null) {
				Debug.LogError ("Not Found " + typeof(T).ToString ());
			}
			return instance;
		}
	}

	protected virtual void Awake(){
		CheckInstance ();
	}

	private bool CheckInstance(){
		if (instance == null) {
			instance = (T)this;
			return true;
		} else if (instance == this) {
			return true;
		}
		Destroy (this);
		return false;
	}
}
