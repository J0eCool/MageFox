using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using AxisMap = System.Collections.Generic.Dictionary<string, bool>;

public class ButtonManager : SingletonComponent<ButtonManager> {
	public readonly static string FireAxis = "Shoot";
	public readonly static string[] AxisNames = {
		FireAxis,
		"Spell1",
		"Spell2",
		"Spell3",
		"Horizontal",
		"Vertical",
	};

	public readonly static string[] ButtonNames = {
		FireAxis,
	};

	private AxisMap _heldKeys = new AxisMap();
	private AxisMap _pressedKeys = new AxisMap();
	private AxisMap _releasedKeys = new AxisMap();

	protected override void onStart() {
		foreach (string axis in AxisNames) {
			_heldKeys[axis] = false;
		}
	}

	void FixedUpdate() {
		foreach (string axis in ButtonNames) {
			bool isHeld = Input.GetButton(axis);
			bool wasHeld = _heldKeys[axis];
			_heldKeys[axis] = isHeld;
			_pressedKeys[axis] = isHeld && !wasHeld;
			_releasedKeys[axis] = !isHeld && wasHeld;
		}
	}

	private bool tryGet(AxisMap collection, string axisName) {
		bool val;
		collection.TryGetValue(axisName, out val);
		return val;
	}

	public bool GetButton(string axisName) {
		return tryGet(_heldKeys, axisName);
	}
	public bool GetButtonDown(string axisName) {
		return tryGet(_pressedKeys, axisName);
	}
}
