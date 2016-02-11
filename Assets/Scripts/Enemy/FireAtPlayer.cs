using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireAtPlayer : JComponent {
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private float _burstInterval = 1.5f;
	[SerializeField] private int _burstShots = 3;
	[SerializeField] private float _burstDuration = 0.5f;

	private float _shotTimer = 0.0f;
	private float _burstTimer = float.PositiveInfinity;
	private int _shotsFired = 0;

	protected override void OnStart() {
		initializeShotTimers();
	}

	protected override void OnUpdate() {
		_shotTimer += Time.deltaTime;
		if (_shotTimer > _burstInterval) {
			_burstTimer += Time.deltaTime;
			float timePerBurst = _burstDuration / _burstShots;
			if (_burstTimer > timePerBurst) {
				shoot();
				if (_shotsFired >= _burstShots) {
					initializeShotTimers();
				}
			}
		}
	}

	private void initializeShotTimers() {
		_shotTimer = 0.0f;
		_burstTimer = float.PositiveInfinity;
		_shotsFired = 0;
	}

	private void shoot() {
		_burstTimer = 0.0f;
		_shotsFired++;

		GameObject bulletObj = Instantiate(_bulletPrefab);
		Bullet bullet = bulletObj.GetComponent<Bullet>();
		Vector3 delta = PlayerManager.Instance.PlayerPosition - transform.position;
		bullet.Init(transform.position, delta);
	}
}
