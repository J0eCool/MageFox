using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mana : LimitedQuantity {
	[SerializeField] private float _regen = 1.5f;

	private float _partial = 0.0f;

	protected override void OnUpdate() {
		_partial += _regen * Time.deltaTime;
		int whole = (int)_partial;
		Current = Mathf.Min(Current + whole, Max);
		_partial -= whole;
	}

	public bool CanSpend(int amount) {
		return amount <= Current;
	}

	public bool Spend(int amount) {
		if (!CanSpend(amount)) {
			return false;
		}

		Current -= amount;
		return true;
	}
}
