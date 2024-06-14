using System;
using System.Collections;
using System.Collections.Generic;
using powerups;
using UnityEngine;
using Random = System.Random;

public class PowerupManager : MonoBehaviour
{
    private static Random rnd = new Random();
    
    public List<Powerup> powerups = new() { new BiggerNetPowerup() };
    public Queue<Powerup> queuedPowerups = new();

    public GameObject netObject;
    
    public void ReceivePowerup()
    {
        queuedPowerups.Enqueue(powerups[rnd.Next(powerups.Count)]);
        if (queuedPowerups.Count > 3)
            queuedPowerups.Dequeue(); // Remove first in queue
    }

    public void UsePowerup()
    {
        if (queuedPowerups.Count <= 0) return;
        
        Powerup powerup = queuedPowerups.Dequeue();
        powerup.Activate(this);
    }

    public void UseRottenPowerup()
    {
        if (queuedPowerups.Count <= 0) return;
        
        Powerup powerup = queuedPowerups.Dequeue();
        powerup.ActivateRotten(this);
    }
}
