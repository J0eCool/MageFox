using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlightBob : JComponent {
	[SerializeField] private float _x1 = 2.0f;
	[SerializeField] private float _y1 = 5.0f;
	[SerializeField] private float _x2 = 0.5f;
	[SerializeField] private float _y2 = 0.5f;
	[SerializeField] private float _xBob = 1.0f;
	[SerializeField] private float _yBob = 0.5f;
	[SerializeField] private float _bobSpeed = 1.0f;
	[SerializeField] private bool _drawGizmos = true;

	private float _t = 0.0f;
	private Vector3 _basePos;

	void OnDrawGizmosSelected() {
		if (!_drawGizmos) {
			return;
		}

		float maxTime = 12.0f * _bobSpeed;
		float forwardDist = 5.0f;
		int numSegments = 700;
		float dT = maxTime / numSegments;
		float dX = forwardDist / numSegments;
		Vector3 pos = transform.position;
		for (int i = 0; i < numSegments; ++i) {
			float t = i * dT;
			Vector3 p1 = pos + offsetAtTime(t) + i * dX * Vector3.forward;
			Vector3 p2 = pos + offsetAtTime(t + dT) + (i + 1) * dX * Vector3.forward;
			Gizmos.DrawLine(p1, p2);
		}
	}

	private Vector3 offsetAtTime(float t) {
		float x = Mathf.Cos(_x1 * t) + Mathf.Cos(_x2 * t);
		float y = Mathf.Sin(_y1 * t) + Mathf.Sin(_y2 * t);
		return new Vector3(x * _xBob, y * _yBob, 0.0f);
	}

	public Vector3 Offset() {
		return offsetAtTime(_t);
	}

	protected override void OnStart() {
		_basePos = transform.localPosition;
	}

	protected override void OnUpdate() {
		_t += Time.deltaTime * _bobSpeed;
		transform.localPosition = _basePos + Offset();
	}
}
