using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homeMove : MonoBehaviour {

    void Awake()
    {
    }

    public void StartButton()
    {
        Invoke("home", .5f);
    }

    void startgame()
    {
        Application.LoadLevel("Start");
    }
}
