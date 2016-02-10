using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class BeginSelected : JComponent {
	void OnEnable() {
		EventSystem.current.SetSelectedGameObject(gameObject);
	}
}
