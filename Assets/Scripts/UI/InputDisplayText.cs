using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InputDisplayText : JComponent {
	[StartComponent]
	private Text _text;

	void Update() {
		string displayStr = "";
		foreach (string axis in ButtonManager.AxisNames) {
			float input = Input.GetAxis(axis);
			float raw = Input.GetAxisRaw(axis);
			bool button = ButtonManager.Instance.GetButton(axis);
			displayStr += string.Format("{0,-15}: {1,-5:0.0}, {2,-5:0.0}, {3,-5:0.0}\n", axis, input, raw, button);
		}
		_text.text = displayStr;
	}
}
