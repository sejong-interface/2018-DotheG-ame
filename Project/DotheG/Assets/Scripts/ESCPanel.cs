﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCPanel : MonoBehaviour {
	private GameObject escMenu;
	public bool IsPause;
	GameOverPanel gameOverPanel;
	private GameObject gameOverMenu;

	void Awake(){
		gameOverPanel = this.gameObject.GetComponent<GameOverPanel>();
		Debug.Log(gameOverPanel.IsGameOver);
		IsPause =false;
		escMenu=GameObject.Find("Canvas").transform.Find("ESCPanel").gameObject;
		gameOverMenu=GameObject.Find("Canvas").transform.Find("GameOver").gameObject;
	}

	void Update() {
		if (!gameOverPanel.IsGameOver){

			if (Input.GetKeyDown(KeyCode.Escape)){
				SetPause();
			}
		}
	}

	void SetPause(){
		if (!IsPause){
			Time.timeScale=0;
		}
		else {
			Time.timeScale=1.0f;
		}
		IsPause=!IsPause;
		if(!gameOverPanel.IsGameOver)escMenu.SetActive(IsPause);
	}

	public void ContinueBt(){
		Debug.Log("게임 재개");
		SetPause();
		}

	public void MainBt(){
		Debug.Log("메인 메뉴로");
		SetPause();
		SceneManager.LoadScene("Start");
	}

	public void StartBt(){
		Debug.Log("게임 재시작");
		SetPause();
		SceneManager.LoadScene("Desert");
	}
}