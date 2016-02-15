using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipControl : JComponent {
	[SerializeField] private float _moveSpeed = 1.0f;
	[SerializeField] private float _dashSpeed = 2.5f;
	[SerializeField] private float _dashDuration = 0.5f;
	[SerializeField] private float _dashInvincibilityDuration = 0.5f;
	[SerializeField] private float _dashForwardDistance = 3.0f;
	[SerializeField] private int _dashManaCost = 12;
	[SerializeField] private float _moveAccelTime = 0.5f;
	[SerializeField] private Vector2 _bounds = Vector2.one;
	[SerializeField] private float _lookAheadDist = 0.5f;
	[SerializeField] private bool _drawGizmos = true;

	[StartChildComponent] private Health _health;
	[StartComponent] private Mana _mana;

	private Vector3 _vel = Vector3.zero;
	private Vector3 _origin = Vector3.zero;
	private float _dashTimer = 0.0f;

	protected override void OnStart() {
		_origin = transform.localPosition;
	}

	void OnDrawGizmosSelected() {
		if (!_drawGizmos) {
			return;
		}

		Gizmos.DrawWireCube(_origin, new Vector3(_bounds.x, _bounds.y, 1.0f));
	}

	protected override void OnUpdate() {
		Vector3 pos = transform.localPosition;
		float dx = Input.GetAxis("Horizontal");
		float dy = Input.GetAxis("Vertical");
		Vector3 delta = new Vector3(dx, dy);

		bool didPressDash = ButtonManager.Instance.GetButtonDown(ButtonManager.DashButton);
		_dashTimer -= Time.deltaTime;
		if (didPressDash && _mana.CanSpend(_dashManaCost)) {
			_mana.Spend(_dashManaCost);
			_dashTimer = _dashDuration;
			_health.SetInvincibleFor(_dashInvincibilityDuration);
		}
		if (_dashTimer > 0.0f) {
			float t = _dashTimer / _dashDuration;
			float speed = t * _moveSpeed + (1.0f - t) * _dashSpeed;
			_vel = delta * speed;

			// t*(t - 1)*(t - 1) = t^3 - 2t^2 + t
			// cubic w/ zeros at 0 and 1
			// also needs to be multiplied by ~7 to have a range near [0,1]
			float t2 = 1.0f - t;
			float s = 7.0f * t2 * (1 + t2 * (-2 + t2));
			pos.z = _origin.z + s * _dashForwardDistance;
			transform.localPosition = pos;
		} else {
			float t = Time.deltaTime / _moveAccelTime;
			float s = 1.0f - t;
			_vel = _vel * s + t * delta * _moveSpeed;
		}

		Vector3 moveLook = new Vector3(_vel.x, _vel.y, _lookAheadDist);
		transform.LookAt(transform.position + moveLook);
	}

	protected override void OnFixedUpdate() {
		Vector3 pos = transform.localPosition;
		pos += _vel * Time.fixedDeltaTime;
		Vector3 halfBound = _bounds / 2;
		pos = VectorUtil.ClampXY(pos, _origin - halfBound, _origin + halfBound);
		transform.localPosition = pos;
	}

	public void DidCollide(Vector3 normal) {
		normal.z = 0.0f;
		normal.Normalize();
		_vel = _moveSpeed * normal;
		_health.TakeDamage(2);
	}
}
