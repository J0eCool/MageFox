using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlyForward : JComponent {
	[SerializeField] private Vector3 _velocity;

	protected override void OnFixedUpdate() {
		transform.position += _velocity * Time.fixedDeltaTime;
	}
}
