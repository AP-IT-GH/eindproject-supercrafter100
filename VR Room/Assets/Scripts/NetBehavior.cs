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
            if (!GameManager.isGameActive) return;

            if (other.gameObject.tag.Contains("shiny"))
            {
                GameManager.CatchFruit(CatchType.SHINY);                
            }
            else if (other.gameObject.tag.Contains("rotten")) 
            {
                GameManager.CatchFruit(CatchType.ROTTEN, other.gameObject.tag.Contains("shiny"));
            }
            else
            { 
                GameManager.CatchFruit(CatchType.NORMAL);
            }
        }
    }
}
