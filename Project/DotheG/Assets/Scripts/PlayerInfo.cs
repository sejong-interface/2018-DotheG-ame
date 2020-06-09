using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {
	private GameObject gameOverMenu;
	ArmControllerAssaultRifle arm;
	Text ammoTx;
	public int max_health=20;
	public bool isGameStart;
	public float health;
	public bool IsGameOver;
	public Image Img;

	void Awake() {
		isGameStart = false;
		ammoTx = GameObject.Find("max_bullet").GetComponent<Text>();
		arm = GameObject.Find("arms@assault_rifle").GetComponent<ArmControllerAssaultRifle>();
		IsGameOver=false;
		gameOverMenu=GameObject.Find("Canvas").transform.Find("GameOver").gameObject;
	}
	void Start () {
		health=max_health;
		Img=GameObject.Find("bar").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		Img.fillAmount=health/max_health;

		if (transform.position.y < 80) health = 0;
		if (transform.position.y < 103) isGameStart = true;
		if (isGameStart && transform.position.y > 103) health -= (float)0.05;
	}


    private void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.CompareTag("Monster"))
		{
			Debug.Log("CollisionEnter");
			health -= (float)0.1;
		}
	}
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
			Debug.Log("CollisionStay");
			health -= (float)0.01;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag("heart"))
		{
			health += 3;
			Debug.Log("EatHeart");
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag("ammoCnt"))
		{
			arm.AmmoSettings.ammo += 10;
			arm.RefillAmmo();
			Debug.Log("EatHAmmo");
			ammoTx.text = "/" + arm.AmmoSettings.ammo.ToString();
			Destroy(other.gameObject);
		}
	}
}
