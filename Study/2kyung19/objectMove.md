# **Object 자동 이동**

> 게임 시작 시 원하는 객체가 원하는 방향으로 자동 이동

```cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove2 : MonoBehaviour {
//여기서부터 보면 됨

    public GameObject cloud; //게임 오브젝트 이용
    public float speed = 8.0f; //오브젝트에 줄 스피드 값

   
    void Start()
    {
        cloud = GameObject.Find("cloud");
        // 게임 오브젝트 중 원하는 오브젝트 이름을 찾는다.
    }


    void Update()
    {
        cloud.GetComponent<Transform>().Translate(Vector3.right * speed * Time.deltaTime);
        //오른쪽으로 정한 스피드 값만큼 움직이는 코드 (forword/back/right/left)
    }
}

```