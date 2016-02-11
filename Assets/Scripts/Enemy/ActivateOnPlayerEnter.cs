using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateOnPlayerEnter : JComponent {
	[SerializeField] private GameObject _toActivate;

	private bool _didActivate = false;

	protected override void OnStart() {
		_toActivate.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {
		bool isPlayer = other.gameObject.layer == PlayerManager.Instance.PlayerLayer;
		if (!_didActivate && isPlayer) {
			_toActivate.SetActive(true);
			_didActivate = true;
		}
	}
}
