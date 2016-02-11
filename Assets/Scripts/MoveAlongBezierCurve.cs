using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveAlongBezierCurve : JComponent {
	[SerializeField] private BezierCurve _curve;
	[SerializeField] private float _timeToComplete = 5.0f;
	[SerializeField] private float _progressOnCurve = 0.0f;

	protected override void OnFixedUpdate() {
		float speed = 1.0f / _timeToComplete;
		_progressOnCurve += speed * Time.fixedDeltaTime;
		if (_progressOnCurve > 1.0f) {
			float dT = 0.01f;
			Vector3 delta = _curve.GetPointAt(1.0f) - _curve.GetPointAt(1.0f - dT);
			Vector3 vel = delta / (dT * _timeToComplete);
			transform.position += vel * Time.fixedDeltaTime;
		} else if (_progressOnCurve >= 0.0f) {
			transform.position = _curve.GetPointAt(_progressOnCurve);
		}
	}
}
