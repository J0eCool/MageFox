using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipControl : JComponent {
	[SerializeField] private float _moveSpeed = 1.0f;
	[SerializeField] private float _moveAccelTime = 0.5f;
	[SerializeField] private Vector2 _bounds = Vector2.one;
	[SerializeField] private float _lookAheadDist = 0.5f;
	[SerializeField] private bool _drawGizmos = true;

	[StartChildComponent] private Health _health;

	private Vector3 _vel = Vector3.zero;
	private Vector2 _origin = Vector3.zero;

	protected override void OnStart() {
		_origin = transform.position;
	}

	void OnDrawGizmosSelected() {
		if (!_drawGizmos) {
			return;
		}

		Gizmos.DrawWireCube(_origin, new Vector3(_bounds.x, _bounds.y, 1.0f));
	}

	protected override void OnUpdate() {
		Vector3 pos = transform.position;
		float dx = Input.GetAxis("Horizontal");
		float dy = Input.GetAxis("Vertical");
		Vector3 delta = new Vector3(dx, dy) * _moveSpeed;

		Vector3 moveLook = new Vector3(_vel.x, _vel.y, _lookAheadDist);
		transform.LookAt(pos + moveLook);

		float t = Time.deltaTime / _moveAccelTime;
		float s = 1.0f - t;
		_vel = _vel * s + t * delta;
	}

	protected override void OnFixedUpdate() {
		Vector3 pos = transform.position;
		pos += _vel * Time.fixedDeltaTime;
		pos = VectorUtil.ClampXY(pos, _origin - _bounds/2, _origin + _bounds/2);
		transform.position = pos;
	}

	public void DidCollide(Vector3 normal) {
		normal.z = 0.0f;
		normal.Normalize();
		_vel = _moveSpeed * normal;
		_health.TakeDamage(2);
	}
}
