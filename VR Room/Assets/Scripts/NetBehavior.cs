using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetBehavior : MonoBehaviour
{
    public GameManager GameManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fruit"))
        {
            Destroy(other.gameObject);
            GameManager.CatchFruit(CatchType.NORMAL);
        }
    }
}
