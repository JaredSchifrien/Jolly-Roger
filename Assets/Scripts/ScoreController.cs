﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
	Text scoreText;
	int score;
	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text>();
		Health[] healths = FindObjectsOfType<Health>();
		score = healths.Length;
		scoreText.text = "Enemies Left: " + score;
		foreach (Health health in FindObjectsOfType<Health>()) {
			if (health.GetComponent<ShipController>() == null &&
				health.GetComponent<IslandController>() == null) {
				health.OnDeath += DecrementEnemyCount;
			}
		}
	}
	void DecrementEnemyCount() {
		score--;
		scoreText.text = "Enemies Left: " + score;
	}
	
}
