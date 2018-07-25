# **Player(Object) 1인칭 적용**

> 1. 키보드 W,S,A,D로 Object 이동   
> 2. 마우스 회전으로 Object 시점 회전

### **3인칭 게임에서 1인칭 게임으로 변경**
1. Player로 사용 할 Object 불러오기
2. Main Camera를 Player 자식으로 넣음
3. Assets폴더에 Scripts폴더 생성
4. Scripts폴더 안에 C#파일 생성
5. C#파일 두번 선택하여 코드 작성  ( 예시_ FPScontrol.cs )
```C#
//예제
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPScontrol : MonoBehaviour { //
    public float moveSpeed = 5.0f;
    public float rotSpeed = 3.0f;

    public Camera fpsCam;

	void Start () {
      
	}
	
	void Update () {
        MoveCtrl();
        RotCtrl();
    }

    void MoveCtrl() { //키보드 W,S,A,D Player 이동 함수
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

    }

    void RotCtrl() { //마우스 회전 시점이동 함수
        float rotX = Input.GetAxis("Mouse Y") * rotSpeed;
        float rotY = Input.GetAxis("Mouse X") * rotSpeed;

        this.transform.localRotation *= Quaternion.Euler(0, rotY, 0);
        fpsCam.transform.localRotation *= Quaternion.Euler(-rotX, 0, 0);
    }
}
```
6. 유니티로 돌아와서 Hierarchy창 안의 Player를 클릭한 후 해당 C#파일을 Inspector 창으로 끌어당김 > 적용됨
7. 적용된 Script 창에 예제 변수인 FpsCam 목록이 생성 됨
8. Hierarchy창 안의 Main Camera를 끌어와서 FpsCam에 해당 camera가 적용되도록 함
9. 실행