using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HealthBar : JComponent {
	[SerializeField] private Health _health;
	[SerializeField] private RectTransform _barImage;

	private float _baseWidth;

	protected override void onStart() {
		_baseWidth = _barImage.localScale.x;
	}

	void Update() {
		float pct = _health ? _health.Fraction : 0.0f;
		pct = Mathf.Clamp01(pct);
		float width = _baseWidth * pct;
		_barImage.localScale = new Vector2(width, _barImage.localScale.y);
	}
}
