using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : JComponent {
	[SerializeField] private float _speed = 30.0f;
	[SerializeField] private float _range = 150.0f;
	[SerializeField] private int _damage = 2;

	private Vector3 _dir;
	private float _distTraveled = 0.0f;
	private bool _didCollide = false;

	public void Init(Vector3 pos, Vector3 dir) {
		transform.position = pos;
		_dir = dir;
		transform.LookAt(pos + dir);
	}

	void FixedUpdate() {
		float dist = _speed * Time.fixedDeltaTime;
		transform.position += _dir * dist;
		_distTraveled += dist;

		if (_distTraveled > _range || _didCollide) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		Health health = other.GetComponent<Health>();
		if (health) {
			health.TakeDamage(_damage);
			_didCollide = true;
		}
	}
}
