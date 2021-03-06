﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructionWinCondition : MonoBehaviour {

	// Use this for initialization
	public float EnemyCount;
	public Text Instructions;
	public float InstructionsFadeTime = .5f;
	public float InstructionSolidTime = 1f;
	private float StartTime;
	private Color InstructionColor;
	void Start () {
		Instructions.text = "Destroy All Enemies!";
		InstructionColor = Instructions.color;
		EnemyCount = 0;
		StartTime = Time.time + InstructionSolidTime;
		Health[] healths = FindObjectsOfType<Health>();
		foreach (Health health in FindObjectsOfType<Health>()) {
			if (health.GetComponent<ShipController>() == null) {
				EnemyCount++;
				health.OnDeath += DecrementEnemyCount;
			}
		}
	}

	void Update() {
		if ((Time.time - StartTime) / InstructionsFadeTime < 2) {
			InstructionColor.a = Mathf.Lerp(1,0, (Time.time - StartTime) / InstructionsFadeTime);
			Instructions.color = InstructionColor;
		}
		
	}
	

	void DecrementEnemyCount() {
		EnemyCount--;
		if (EnemyCount <= 0) {
			FindObjectOfType<GameManager>().Win();
		}
	}
}
