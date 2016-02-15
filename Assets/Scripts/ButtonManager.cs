using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using AxisMap = System.Collections.Generic.Dictionary<string, bool>;

public class ButtonManager : SingletonComponent<ButtonManager> {
	public readonly static string FireButton = "Shoot";
	public readonly static string SpellButton1 = "Spell1";
	public readonly static string SpellButton2 = "Spell2";
	public readonly static string SpellButton3 = "Spell3";
	public readonly static string DashButton = "Dash";
	public readonly static string PauseButton = "Pause";
	public readonly static string[] AxisNames = {
		FireButton,
		SpellButton1,
		SpellButton2,
		SpellButton3,
		DashButton,
		PauseButton,
		"Horizontal",
		"Vertical",
	};

	public readonly static string[] ButtonNames = {
		FireButton,
		SpellButton1,
		SpellButton2,
		SpellButton3,
		DashButton,
		PauseButton,
	};

	private AxisMap _heldKeys = new AxisMap();
	private AxisMap _pressedKeys = new AxisMap();
	private AxisMap _releasedKeys = new AxisMap();

	protected override void OnStart() {
		foreach (string axis in AxisNames) {
			_heldKeys[axis] = false;
		}
	}

	protected override bool CanBePaused { get { return false; } }
	protected override void OnUpdate() {
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
