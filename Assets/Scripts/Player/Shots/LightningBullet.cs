using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningBullet : JComponent, IShootable {
	[SerializeField] private int _damage = 5;
	[SerializeField] private float _despawnDelay = 0.7f;
	[SerializeField] private float _defaultDist = 5.0f;
	[SerializeField] private GameObject _segmentPrefab;
	[SerializeField] private float _segmentDeflectMaxRatio = 0.3f;
	[SerializeField] private float _segmentMaxLength = 2.0f;

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

		List<Vector3> path = pathBetween(pos, targetPos);
		for (int i = 0; i < path.Count; ++i) {
			Vector3 cur = path[i];
			Vector3 next = (i + 1 < path.Count) ? path[i+1] : targetPos;
			GameObject segment = Instantiate(_segmentPrefab);
			scaleObjectBetween(segment.transform, cur, next);
			segment.transform.SetParent(transform, true);
		}
	}

	private List<Vector3> pathBetween(Vector3 start, Vector3 end) {
		List<Vector3> path;
		Vector3 delta = end - start;
		float len = delta.magnitude;
		if (len <= _segmentMaxLength) {
			path = new List<Vector3>();
			path.Add(start);
			return path;
		}
		Vector3 mid = (start + end) / 2 + _segmentDeflectMaxRatio * len * VectorUtil.RandUnitVec3();
		path = pathBetween(start, mid);
		path.AddRange(pathBetween(mid, end));
		return path;
	}

	private static void scaleObjectBetween(Transform trans, Vector3 pos, Vector3 targetPos) {
		Vector3 scale = trans.localScale;
		Vector3 delta = targetPos - pos;
		scale.z = delta.magnitude;
		trans.localScale = scale;

		trans.position = (targetPos + pos) / 2;
		trans.LookAt(targetPos);
	}

	protected override void OnUpdate() {
		_timer += Time.deltaTime;
		if (_timer > _despawnDelay) {
			Remove();
		}
	}
}
