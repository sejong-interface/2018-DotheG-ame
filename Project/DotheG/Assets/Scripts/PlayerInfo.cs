using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {
	private GameObject gameOverMenu;
	public int max_health=20;
	public int health;
	public bool IsGameOver;
	public Image Img;

	void Awake() {
		IsGameOver=false;
		gameOverMenu=GameObject.Find("Canvas").transform.Find("GameOver").gameObject;
	}
	void Start () {
		health=max_health;
		Img=GameObject.Find("bar").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		Img.fillAmount=health/(float)max_health;
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Monster")){
			health--;
		}
	}
}
