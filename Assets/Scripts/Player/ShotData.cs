using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ShotData : ScriptableObject {
	[SerializeField] private GameObject _prefab;
	[SerializeField] private int _manaCost = 0;
	[SerializeField] private int _numToFire = 1;
	[SerializeField] private float _randomSpread = 0.0f;
	[SerializeField] private float _cooldown = 0.0f;

	private GameObject _shotPoint;
	private Mana _mana;
	private float _cooledDownTime;

	public void Init(GameObject shotPoint, Mana mana) {
		_shotPoint = shotPoint;
		_mana = mana;
		_cooledDownTime = 0.0f;
	}

	public void DidPress(Vector3 dir) {
		if (!canShoot()) {
			return;
		}

		Shoot(dir);
	}

	private bool canShoot() {
		return _mana.CanSpend(_manaCost) && Time.time >= _cooledDownTime;
	}

	protected void Shoot(Vector3 dir) {
		Vector3 pos = _shotPoint.transform.position;
		for (int i = 0; i < _numToFire; ++i) {
			GameObject bulletObj = GameObject.Instantiate(_prefab);
			Bullet bullet = bulletObj.GetComponent<Bullet>();
			Vector3 randomAxis = VectorUtil.RandUnitVec3();
			float spreadAmt = Vector3.Cross(randomAxis, dir).magnitude * _randomSpread;
			Vector3 spreadDir = Quaternion.AngleAxis(spreadAmt, randomAxis) * dir;
			bullet.Init(pos, spreadDir);
		}

		_mana.Spend(_manaCost);
		_cooledDownTime = Time.time + _cooldown;
	}

	[MenuItem("Assets/Create/ShotData")]
	public static void CreateShotData() {
		ScriptableObjectUtil.CreateAsset<ShotData>();
	}
}
