using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShowcaseCamera : JComponent {
	[SerializeField] private GameObject _target;
	[SerializeField] private float _rotateSpeed = 90.0f;

	private float _angle;
	private float _dist;
	private float _yOffset;

	protected override void OnStart() {
		Vector3 pos = transform.position;
		Vector3 targetPos = _target.transform.position;
		Vector3 delta = pos - targetPos;
		_yOffset = delta.y;
		_dist = delta.WithY(0.0f).magnitude;
		_angle = Mathf.Atan2(delta.z, delta.x);
	}

	protected override void OnUpdate() {
		_angle += Time.deltaTime * _rotateSpeed * Mathf.Deg2Rad;
		Vector3 delta = new Vector3(_dist * Mathf.Cos(_angle), _yOffset, _dist * Mathf.Sin(_angle));
		transform.position = _target.transform.position + delta;
		transform.LookAt(_target.transform);
	}
}
