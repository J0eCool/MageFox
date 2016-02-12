using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RemoveOnStart : JComponent {
	protected override void OnStart() {
		Remove();
	}
}
