using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseController : JComponent {
	private float _savedTimeScale;
	private bool _isPaused = false;

	void Update() {
		if (ButtonManager.Instance.GetButtonDown(ButtonManager.PauseButton)) {
			togglePause();
		}
	}

	private void togglePause() {
		float toSet = _isPaused ? _savedTimeScale : 0.0f;
		if (!_isPaused) {
			_savedTimeScale = Time.timeScale;
		}
		Time.timeScale = toSet;
		_isPaused = !_isPaused;
	}
}
