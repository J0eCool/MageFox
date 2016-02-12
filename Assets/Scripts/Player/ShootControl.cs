using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootControl : JComponent {
	[SerializeField] private GameObject _bulletPrefab = null;
	[SerializeField] private GameObject _shotPoint = null;

	protected override void OnStart() {
	}

	protected override void OnUpdate() {
		bool didShoot = ButtonManager.Instance.GetButtonDown(ButtonManager.FireButton);
		if (didShoot) {
			GameObject bulletObj = Instantiate(_bulletPrefab);
			Bullet bullet = bulletObj.GetComponent<Bullet>();
			bullet.Init(_shotPoint.transform.position, transform.forward);
		}
	}
}
