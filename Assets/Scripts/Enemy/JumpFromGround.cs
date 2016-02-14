using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JumpFromGround : JComponent {
	[SerializeField] private float _jumpTime = 1.5f;
	[SerializeField] private float _fallTime = 0.5f;
	[SerializeField] private float _shootTime = 1.0f;
	[SerializeField] private float _jumpHeight = 10.0f;
	[SerializeField] private float _jumpForwardSpeed = 2.0f;
	[SerializeField] private Vector3 _flyVel;

	[StartComponent] private FireAtPlayer _fire;

	private float _timer = 0.0f;
	private float _startHeight;

	protected override void OnStart() {
		_startHeight = transform.position.y;
		_fire.enabled = false;
	}

	protected override void OnFixedUpdate() {
		_timer += Time.fixedDeltaTime;
		if (_timer < _jumpTime + _fallTime) {
			float t = _timer / _jumpTime - 1.0f;
			float pct = 1.0f - t * t;
			float y = _startHeight + _jumpHeight * pct;
			Vector3 pos = transform.position;
			pos.y = y;
			pos.z += Time.fixedDeltaTime * _jumpForwardSpeed;
			transform.LookAt(pos);
			transform.position = pos;
		} else if (_timer < _jumpTime + _fallTime + _shootTime) {
			_fire.enabled = true;
			transform.LookAt(PlayerManager.Instance.PlayerPosition);
		} else {
			_fire.enabled = false;
			transform.LookAt(transform.position + _flyVel);
			transform.position += Time.fixedDeltaTime * _flyVel;
		}
	}
}
