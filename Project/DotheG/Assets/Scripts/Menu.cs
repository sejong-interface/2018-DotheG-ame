using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
	private GameObject ESCPanel;
	private bool pause=false;
	
	void Awake() {
		ESCPanel=GameObject.Find("Canvas").transform.Find("ESCPanel").gameObject;
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)){
			pause=true;
		}

		if (pause==true){
			Time.timeScale=0;
			ESCPanel.SetActive(true);
		}
	}

	public void StartBt(){
		Debug.Log("게임 재시작");
		Time.timeScale=1.0f;
		SceneManager.LoadScene("Desert");
	}
	
	public void MainBt(){
		Debug.Log("메인으로");
		Time.timeScale=1.0f;
		SceneManager.LoadScene("Start");
	}

	public void ContinueBt(){
		Debug.Log("게임 진행");
		ESCPanel.SetActive(false);
		Time.timeScale=1.0f;
		pause=false;
	}
}
