using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour {
	PlayerInfo info;
	public bool IsGameOver;
	public bool GameOverIng;
	private GameObject gameOverMenu;

	void Awake(){
		IsGameOver=false;
		GameOverIng=false;
		gameOverMenu=GameObject.Find("Canvas").transform.Find("GameOver").gameObject;
	}

	void Start(){
		info=GameObject.Find("User").GetComponent<PlayerInfo>();
	}

	void Update() {
		if (info.health==0 && !GameOverIng){
			GameOverIng=true;
			SetGameOver();
		}
	}

	void SetGameOver(){
		if (!IsGameOver){
			Time.timeScale=0;
		}
		else {
			Time.timeScale=1.0f;
		}
		IsGameOver=!IsGameOver;
		gameOverMenu.SetActive(IsGameOver);
	}

	public void MainBt(){
		Debug.Log("메인 메뉴로");
		info.health=info.max_health;
		GameOverIng=false;
		SetGameOver();
		SceneManager.LoadScene("Main");
	}

	public void StartBt(){
		Debug.Log("게임 재시작");
		info.health=info.max_health;
		GameOverIng=false;
		SetGameOver();
		SceneManager.LoadScene("Desert");
	}
}