using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveChick : MonoBehaviour
{
    private float time;
    private float range, angle;
    public float speed = 2f;
    private Vector3 moveTo;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        range = 4f;
        angle=Random.Range(0, 360);

        moveTo.x = Random.Range(-8,9);
        moveTo.y = this.gameObject.transform.position.y;
        moveTo.z = Random.Range(-11, 6);

        this.gameObject.transform.LookAt(moveTo);
    }

    // Update is called once per frame
    void Update()
    {
        time += 0.1f;
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (time >= 5f || this.gameObject.transform.position == moveTo)
        {
            moveTo.x = range * (float)(Mathf.Cos(angle));
            moveTo.y = this.gameObject.transform.position.y;
            moveTo.z = range * (float)(Mathf.Sin(angle));
            this.gameObject.transform.LookAt(moveTo);
        }
        else if (time >= 10f || this.gameObject.transform.position == moveTo) {
            time = 0f;
            moveTo.x = Random.Range(-5, 6);
            moveTo.y = this.gameObject.transform.position.y;
            moveTo.z = Random.Range(-8, 3);
        }

    }
}
