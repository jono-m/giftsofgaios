﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public float degreesPerSecond;
    
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward, degreesPerSecond * Time.deltaTime);
	}
}
