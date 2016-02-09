using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : JComponent {
	[SerializeField] private int _maxHealth = 10;

	public int Max { get { return _maxHealth; } }
	public int Current { get; private set; }
	public float Fraction { get { return (float)Current / _maxHealth; } }

	protected override void onStart() {
		Current = _maxHealth;
	}

	void FixedUpdate() {
		if (Current <= 0) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		Bullet bullet = other.GetComponent<Bullet>();
		if (bullet) {
			Current -= bullet.Damage;
		}
	}
}
