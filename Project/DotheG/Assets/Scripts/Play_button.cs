using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_button : MonoBehaviour {

    void Awake()
    {
    }

    public void StartButton() {
        Invoke("startgame", .5f);
    }

    void startgame() {
        Application.LoadLevel("Desert");
    }
}
