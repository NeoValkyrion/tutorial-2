using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;

	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	private int score;

	public Text gameOverText;
	public GameObject restartButton;

	private bool gameOver;

	void Start() {
		score = 0;
		UpdateScore();

		gameOver = false;

		gameOverText.text = "";
		restartButton.SetActive(false);

		StartCoroutine(SpawnWaves());
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);

		while(true) {
			for(int i = 0; i < hazardCount; i++) {

				GameObject hazard = hazards [Random.Range(0, hazards.Length)];

				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;

				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}

			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restartButton.SetActive(true);
				break;
			}
		}
	}

	public void AddScore (int newScoreValue) {
		score += newScoreValue;
		UpdateScore();
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	public void GameOver() {
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

	public void RestartGame() {
		Application.LoadLevel(Application.loadedLevel);
	}
}
