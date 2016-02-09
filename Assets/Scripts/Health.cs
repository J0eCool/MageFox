using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : JComponent {
	[SerializeField] private int _maxHealth = 10;
	[SerializeField] private bool _isEnemy = true;
	[SerializeField] private Material _damageFlashMaterial;
	[SerializeField] private float _damageFlashDuration = 0.25f;
	[SerializeField] private float _damageFlashInterval = 0.05f;
	[SerializeField] private float _invinciblityDuration = 0.1f;

	public int Max { get { return _maxHealth; } }
	public int Current { get; private set; }
	public float Fraction { get { return (float)Current / _maxHealth; } }
	public bool IsEnemy { get { return _isEnemy; } }

	private float _damageTimer = 0.0f;
	private float _flashTimer = 0.0f;
	private float _invincibleUntil = 0.0f;
	private bool _didFlash = false;
	private Material _baseMaterial;

	[StartComponent] private Renderer _renderer;

	protected override void onStart() {
		Current = _maxHealth;
		_baseMaterial = _renderer.material;
	}

	void FixedUpdate() {
		if (Current <= 0) {
			Destroy(gameObject);
		}
	}

	void Update() {
		if (_damageTimer > 0.0f) {
			_damageTimer -= Time.deltaTime;
			_flashTimer -= Time.deltaTime;
			if (_damageTimer <= 0.0f) {
				stopFlashing();
			} else if (_flashTimer <= 0.0f) {
				toggleFlash();
			}
		}
	}

	private void beginFlashing() {
		_damageTimer = _damageFlashDuration;
		_didFlash = false;
		toggleFlash();
	}

	private void stopFlashing() {
		_flashTimer = 0.0f;
		_renderer.material = _baseMaterial;
	}

	private void toggleFlash() {
		_flashTimer = _damageFlashInterval;
		_renderer.material = _didFlash ? _baseMaterial : _damageFlashMaterial;
		_didFlash = !_didFlash;
	}

	private bool isInvincible() {
		return Time.timeSinceLevelLoad < _invincibleUntil;
	}

	public void TakeDamage(int damage) {
		if (!isInvincible()) {
			Current -= damage;
			_invincibleUntil = Time.timeSinceLevelLoad + _invinciblityDuration;
			beginFlashing();
		}
	}
}
