using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour {
	PlayerInfo info;
    UIManager totalScore;
	public bool IsGameOver;
	public bool GameOverIng;
	private GameObject gameOverMenu;
    private GameObject gameOverMenuScore;

	void Awake(){
		IsGameOver=false;
		GameOverIng=false;
		gameOverMenu=GameObject.Find("Canvas").transform.Find("GameOver").gameObject;
        gameOverMenuScore = GameObject.Find("Canvas").transform.Find("GameOver").transform.Find("back_g").transform.Find("score").gameObject;
	}

	void Start(){
		info=GameObject.Find("User").GetComponent<PlayerInfo>();
        totalScore = GameObject.Find("GameManager").GetComponent<UIManager>();
	}

	void Update() {
		if (info.health<=0 && !GameOverIng){
			GameOverIng=true;
			Cursor.visible = true;
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
        gameOverMenuScore.GetComponent<Text>().text = (totalScore.scorePoint).ToString();
        Debug.Log("gameOverScore : "+(totalScore.scorePoint).ToString());
		gameOverMenu.SetActive(IsGameOver);
	}

	public void MainBt(){
		Debug.Log("나가기");
		info.health=info.max_health;
		GameOverIng=false;
		SetGameOver();
		SceneManager.LoadScene("Start");
	}

	public void StartBt(){
		Debug.Log("다시하기");
		info.health=info.max_health;
		GameOverIng=false;
		SetGameOver();
        Cursor.visible = false;
		SceneManager.LoadScene("Desert");
	}
}