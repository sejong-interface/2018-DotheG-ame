using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingHeart : MonoBehaviour
{
    Rigidbody rg;
    void Start()
    {
        rg = this.gameObject.GetComponent<Rigidbody>();
        if (rg.transform.position.y > 100.5)
        {
            rg.useGravity = true;
        }
    }
    void Update()
    {
        if (rg.transform.position.y <= 100.5)
        {
            Vector3 heartYpos;
            heartYpos.x = this.gameObject.transform.position.x;
            heartYpos.y = 100.5f;
            heartYpos.z = this.gameObject.transform.position.z;
            rg.useGravity = false;
            this.gameObject.transform.position = heartYpos;
        }
    }
}
