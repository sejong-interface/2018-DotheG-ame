﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frontTest : MonoBehaviour
{
    
    void Update()
    {

        transform.Translate(Vector3.forward * 1f * Time.deltaTime);
    }
}
