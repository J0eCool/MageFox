using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainHitHandler : JComponent {
	[StartParentComponent] private ShipControl _control;

	void OnCollisionEnter(Collision hit) {
		foreach (var contact in hit.contacts) {
			_control.DidCollide(contact.normal);
		}
	}
}
