using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameManager GameManager;
    public DateTime entryTime;
    public bool isPendingStart = false;
    public void Update()
    {
        if (isPendingStart)
        {
            TimeSpan ts = DateTime.Now - entryTime;
            if (ts.Seconds >= 3)
            {
                GameManager.StartGame();
                isPendingStart = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Area" && !GameManager.isGameActive)
        {
            // Start countdown
            entryTime = DateTime.Now;
            isPendingStart = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Area")
        {
            // Cancel countdown
            isPendingStart = false;
        }
    }
}
