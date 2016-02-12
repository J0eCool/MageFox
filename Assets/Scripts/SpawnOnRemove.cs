using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnOnRemove : JComponent {
	[SerializeField] private GameObject _toSpawnPrefab = null;

	protected override void OnRemove() {
		GameObject obj = Instantiate(_toSpawnPrefab);
		obj.transform.position = transform.position;
	}
}
