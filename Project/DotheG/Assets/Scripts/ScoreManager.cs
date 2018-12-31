using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	public int scorePoint=0;

	private Text scoreTx;

	void Awake() {
		scoreTx=GameObject.Find("UI").transform.Find("score").GetComponent<Text>();
		scoreTx.text="0";
	}

	public void plusScore(int plusPoint){
		scorePoint+=plusPoint;
		scoreTx.text=scorePoint.ToString("N0"); //N0타입:정수형 표기(천 단위씩 끊어서)
	}
}