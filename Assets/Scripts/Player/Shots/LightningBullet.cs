using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningBullet : JComponent, IShootable {
	[SerializeField] private int _damage = 5;
	[SerializeField] private float _despawnDelay = 0.7f;
	[SerializeField] private float _defaultDist = 5.0f;

	private float _timer = 0.0f;

	public void Init(Vector3 pos, Vector3 dir) {
		Health target = null;
		Vector3 targetPos = pos + _defaultDist * dir;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		float minDistSquared = float.PositiveInfinity;
		foreach (GameObject enemy in enemies) {
			Health health = enemy.GetComponent<Health>();
			Vector3 enemyDelta = enemy.transform.position - pos;
			bool isForward = Vector3.Dot(enemyDelta, dir) > 0.0f;
			float dist = enemyDelta.sqrMagnitude;
			if (health && health.IsEnemy && isForward && dist < minDistSquared) {
				minDistSquared = dist;
				target = health;
				targetPos = enemy.transform.position;
			}
		}

		if (target) {
			target.TakeDamage(_damage);
		}

		Vector3 scale = transform.localScale;
		Vector3 delta = targetPos - pos;
		scale.z = delta.magnitude;
		transform.localScale = scale;

		transform.position = (targetPos + pos) / 2;
		transform.LookAt(targetPos);
	}

	protected override void OnUpdate() {
		_timer += Time.deltaTime;
		if (_timer > _despawnDelay) {
			Remove();
		}
	}
}
