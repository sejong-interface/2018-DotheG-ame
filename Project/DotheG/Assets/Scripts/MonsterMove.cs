using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    private Transform _transform;
    private Transform playerTransform;
    public float speed = 0.5f;
    public float maxSpeed = 2.0f;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        this.gameObject.transform.LookAt(playerTransform);
    }

    private void Update()
    {
        this.gameObject.transform.LookAt(playerTransform);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (speed < maxSpeed) speed += 0.002f;
    }

}
