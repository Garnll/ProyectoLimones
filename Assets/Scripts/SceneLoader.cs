using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	[SerializeField]
	private Slider loadingSlider;
	AsyncOperation async;

	private void Start() {
		StartCoroutine(LoadSceneAsync());
	}

	IEnumerator LoadSceneAsync() {
		async = SceneManager.LoadSceneAsync("Main Level");
		while (!async.isDone) {
			loadingSlider.normalizedValue = async.progress;
			yield return null;
		}
	}
}
