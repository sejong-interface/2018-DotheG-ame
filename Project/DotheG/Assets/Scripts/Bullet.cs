using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {

    public float bigBulletSpeed=100.0f;
    
    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody>().AddForce(transform.forward * bigBulletSpeed);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Monster")){
            Destroy(this.gameObject);
        }
    }

    void Update(){
        Destroy(this.gameObject,2.0f);
    }
}