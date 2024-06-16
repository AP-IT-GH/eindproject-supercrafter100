using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBouncer : MonoBehaviour
{
    public int speed;
    // Cube moves until it exits the trigger area, once it does, it bounces in the opposite direction, like pong!

    public void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("exit");
        Quaternion currentRotation = transform.localRotation;
        // bounce off the trigger area wall like pong
        transform.localRotation = new Quaternion(currentRotation.x, -currentRotation.y, currentRotation.z, currentRotation.w);
    }
}
