using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using AxisMap = System.Collections.Generic.Dictionary<string, bool>;

public class ButtonManager : SingletonComponent<ButtonManager> {
	public readonly static string FireAxis = "Fire1";
	public readonly static string[] AxisNames = {
		FireAxis,
		"Fire2",
		"Fire3",
		"Jump",
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

	public bool GetButton(string axisName) {
		return _heldKeys[axisName];
	}
	public bool GetButtonDown(string axisName) {
		return _pressedKeys[axisName];
	}
}
