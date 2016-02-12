using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootControl : JComponent {
	[SerializeField] private ShotData _fireData;
	[SerializeField] private ShotData _spellData1;
	[SerializeField] private ShotData _spellData2;
	[SerializeField] private ShotData _spellData3;
	[SerializeField] private GameObject _shotPoint = null;

	[StartComponent] private Mana _mana;

	private Dictionary<string, ShotData> _guns = new Dictionary<string, ShotData>();

	protected override void OnStart() {
		_guns[ButtonManager.FireButton] = _fireData;
		_guns[ButtonManager.SpellButton1] = _spellData1;
		_guns[ButtonManager.SpellButton2] = _spellData2;
		_guns[ButtonManager.SpellButton3] = _spellData3;

		foreach (var gun in _guns.Values) {
			if (gun) {
				gun.Init(_shotPoint, _mana);
			}
		}
	}

	protected override void OnUpdate() {
		foreach (var kv in _guns) {
			bool didPress = ButtonManager.Instance.GetButtonDown(kv.Key);
			if (didPress && kv.Value) {
				kv.Value.DidPress(transform.forward);
			}
		}
	}
}
