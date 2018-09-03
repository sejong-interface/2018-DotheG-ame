using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	void OnCollisionEnter(Collision Collision)
	{
		//충돌한 상대방에게 데미지를 주는 코드 작성

		
		Destroy(gameObject);
	}
}
