using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour {
	public int cnt;
	public int max_cnt=0;
	public int plusPoint=100;

	public bool isChick;

	GenerateMonster gm;
	AudioSource player;

	public AudioClip mole;
	public AudioClip chick;

	// Use this for initialization
	void Start () {
		player = this.gameObject.GetComponent<AudioSource>();
		cnt=0;
		gm = GameObject.Find("GameManager").GetComponent<GenerateMonster>();
	}
	
	// Update is called once per frame
	void Update () {
		if (cnt==max_cnt){
			for (var i = 0; i < gm.chickList.Count; i++)
			{
				if (gameObject == gm.chickList[i])
				{
					gm.chickList.Remove(gameObject);
					Debug.Log("Remove over cnt chick");
					isChick = true;
				}
			}
			for (var i = 0; i < gm.moleList.Count; i++)
			{
				if (gameObject == gm.moleList[i])
				{
					gm.moleList.Remove(gameObject);
					gm.moleCnt++;
					isChick = false;
				}
			}


			if (isChick) {
				gm.makeChickParticleSystem(gameObject.transform.position);
				player.clip = chick;
			}
			else
			{
				gm.makeMoleParticleSystem(gameObject.transform.position);
				player.clip = mole;
			}


			player.Play();
			Debug.Log("PlaySound");
			Destroy(this.gameObject);

			if (gameObject.name == "chick(Clone)" && Random.Range(1, 4) == 1)
			{
				gm.GenerateHeartItem(transform.position);
				Debug.Log("GenerateHeartItem");
			}
			else if (gameObject.name == "chick(Clone)" && Random.Range(1, 4) == 2)
			{
				gm.GenerateAmmoItem(transform.position);
				Debug.Log("GenerateAmmoItem");
			}
			GameObject.Find("GameManager").GetComponent<UIManager>().plusScore(plusPoint);
		}
		if (this.transform.position.y<95){
			Destroy(this.gameObject);
			Debug.Log("Remove object thst position.y is lower then 95");
		}
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Bullet")){
			cnt++;
		}
	}
}
