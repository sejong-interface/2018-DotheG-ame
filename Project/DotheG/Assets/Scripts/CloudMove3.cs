using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove3 : MonoBehaviour {

    public GameObject cloud;
    public float cloud_x = 0;
    public float speed = 8.0f;

    // Use this for initialization
    void Start () {
        cloud = GameObject.Find("cloud0");
    }
	
	// Update is called once per frame
	void Update () {
        cloud_x = (cloud.GetComponent<Transform>().position.x);

        if (cloud_x >= 100)
        {
            cloud.GetComponent<Transform>().position = new Vector3(-100, -10, 0);
        }
        else
        {
            cloud.GetComponent<Transform>().Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
