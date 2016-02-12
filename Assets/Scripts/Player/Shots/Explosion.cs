using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosion : JComponent {
	[SerializeField] private float _growTime = 0.2f;
	[SerializeField] private float _pauseTime = 0.1f;
	[SerializeField] private float _shrinkTime = 0.5f;
	[SerializeField] private int _damage = 5;

	private Vector3 _baseScale;
	private float _timer = 0.0f;
	private HashSet<Health> _collided = new HashSet<Health>();

	protected override void OnStart() {
		_baseScale = transform.localScale;
		transform.localScale = Vector3.zero;
	}

	protected override void OnUpdate() {
		_timer += Time.deltaTime;
		if (_timer < _growTime) {
			// Grow
			float t = _timer / _growTime;
			transform.localScale = t * _baseScale;
		} else if (_timer < _growTime + _pauseTime) {
			// Pause
			transform.localScale = _baseScale;
		} else if (_timer < _growTime + _pauseTime + _shrinkTime) {
			// Shrink
			float t = (_timer - _growTime - _pauseTime) / _shrinkTime;
			transform.localScale = (1.0f - t) * _baseScale;
		} else {
			// Done
			Remove();
		}
	}

	void OnTriggerEnter(Collider other) {
		Health health = other.gameObject.GetComponent<Health>();
		if (health && !_collided.Contains(health)) {
			_collided.Add(health);

			health.TakeDamage(_damage);
		}
	}
}
