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

> 게임오브젝트의 위치를 파악하여 오브젝트가 일정 위치에 도달 시 다른 위치로 이동시키는 방법

```CS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {

    public GameObject cloud;
    public float cloud_x=0;
    public float speed = 8.0f;

    //여기서부터 보면 됨
    void Start()
    {
        cloud = GameObject.Find("cloud");
    }

    void Update()
    {
        cloud_x = (cloud.GetComponent<Transform>().position.x);
        //해당 오브젝트의 x position을 찾는 법

        if ( cloud_x >= 100 ) { //만약 오브젝트의 x값이 100이상이라면
            cloud.GetComponent<Transform>().position = new Vector3(-100, -10, 0);
            // cloud의 포지션을 (-100,-10,0) 으로 바꿔줌
        }
        else {
            cloud.GetComponent<Transform>().Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}

```