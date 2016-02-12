using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class FireballData : ShotData {
	public override void DidPress(Vector3 dir) {
		if (!_lastShotObject) {
			base.DidPress(dir);
		} else {
			JComponent.Remove(_lastShotObject);
		}
	}

	[MenuItem("Assets/Create/Shots/FireballData")]
	public static void CreateFireballData() {
		ScriptableObjectUtil.CreateAsset<FireballData>();
	}
}
