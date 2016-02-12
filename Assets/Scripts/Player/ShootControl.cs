using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootControl : JComponent {
	[SerializeField] private ShotData _fireData;
	[SerializeField] private ShotData _spellData;
	[SerializeField] private GameObject _shotPoint = null;

	private Dictionary<string, ShotData> _guns = new Dictionary<string, ShotData>();

	protected override void OnStart() {
		_guns[ButtonManager.FireButton] = _fireData;
		_guns[ButtonManager.SpellButton1] = _spellData;

		foreach (var gun in _guns.Values) {
			gun.Init(_shotPoint);
		}
	}

	protected override void OnUpdate() {
		foreach (var kv in _guns) {
			bool didPress = ButtonManager.Instance.GetButtonDown(kv.Key);
			if (didPress) {
				kv.Value.DidPress(transform.forward);
			}
		}
	}
}

[System.Serializable]
public class ShotData {
	[SerializeField] private GameObject _prefab;

	private GameObject _shotPoint;

	public void Init(GameObject shotPoint) {
		_shotPoint = shotPoint;
	}

	public void DidPress(Vector3 dir) {
		Vector3 pos = _shotPoint.transform.position;
		GameObject bulletObj = GameObject.Instantiate(_prefab);
		Bullet bullet = bulletObj.GetComponent<Bullet>();
		bullet.Init(pos, dir);
	}
}
