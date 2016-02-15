using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RemoveAfterTime : JComponent {
	[SerializeField] private float _timeToRemove = 10.0f;

	private float _timer = 0.0f;

	protected override void OnUpdate() {
		_timer += Time.deltaTime;
		if (_timer >= _timeToRemove) {
			Remove();
		}
	}
}
