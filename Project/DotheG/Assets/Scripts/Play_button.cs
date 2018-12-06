using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_button : MonoBehaviour {

    void Start(){
    }

    void Update(){
        Cursor.lockState=CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartButton() {
        Invoke("startgame", .5f);
    }

    void startgame() {
        Application.LoadLevel("Desert");
    }
}
