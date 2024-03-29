﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : SingletonComponent<PlayerManager> {
	[SerializeField] private GameObject _player;

	public GameObject Player { get { return _player; } }
	public Vector3 PlayerPosition { get { return _player.transform.position; } }
	public int PlayerLayer { get { return _layer; } }

	private int _layer;

	protected override void OnStart() {
		_layer = LayerMask.NameToLayer("Player");
	}
}
