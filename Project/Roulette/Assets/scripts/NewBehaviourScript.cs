using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    float rotspeed = 0;

    void Start () {

	}
	
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            this.rotspeed = 10;
        }

        transform.Rotate(0, 0, this.rotspeed);

        this.rotspeed *= 0.98f;
	}
}
