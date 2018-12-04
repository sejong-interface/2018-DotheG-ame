using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 5000.0f;

    void Start(){
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    void Update() {

    }

}
