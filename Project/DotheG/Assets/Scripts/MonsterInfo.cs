using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour {
	private int cnt;
	public int max_cnt=0;

	// Use this for initialization
	void Start () {
		cnt=0;
	}
	
	// Update is called once per frame
	void Update () {
		if (cnt==max_cnt){
			Destroy(this.gameObject);
		}
		if (gameObject.transform.position.y<98){}
		if (this.transform.position.y<95){
			Destroy(this.gameObject);
		}
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Bullet")){
			cnt++;
		}
	}
}
