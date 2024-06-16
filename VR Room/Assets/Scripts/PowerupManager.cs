using System;
using System.Collections;
using System.Collections.Generic;
using powerups;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Random = System.Random;

public class PowerupManager : MonoBehaviour
{
    private static Random rnd = new Random();

    public List<Image> powerupImageSlots = new();
    public List<Powerup> powerups = new();
    public Queue<Powerup> queuedPowerups = new();

    public AudioClip usePowerupAudio;
    public GameObject netObject;

    public Sprite biggerNetPowerupSprite;
    public Sprite extraHeartPowerupSprite;

    private void Start()
    {
        this.powerups.Add(new BiggerNetPowerup(biggerNetPowerupSprite));
        this.powerups.Add(new ExtraLifePowerup(extraHeartPowerupSprite));
        UpdateImageSlots();
    }

    public void ReceivePowerup()
    {
        Powerup powerup = powerups[rnd.Next(powerups.Count)];
        queuedPowerups.Enqueue(powerup);
        if (queuedPowerups.Count > 3)
            queuedPowerups.Dequeue(); // Remove first in queue
        UpdateImageSlots();
    }

    public void UsePowerup()
    {
        if (queuedPowerups.Count <= 0) return;
        
        GetComponent<GameManager>().PlayPlayerAudio(usePowerupAudio);
        Powerup powerup = queuedPowerups.Dequeue();
        powerup.Activate(this);
        UpdateImageSlots();
    }

    public void UseRottenPowerup()
    {
        if (queuedPowerups.Count <= 0) return;
        
        Powerup powerup = queuedPowerups.Dequeue();
        powerup.ActivateRotten(this);
        UpdateImageSlots();
    }

    private void UpdateImageSlots()
    {
        Powerup[] queuedPowerupsArray = queuedPowerups.ToArray();
        for (int i = 0; i < powerupImageSlots.Count; i++)
        {
            if (i > queuedPowerupsArray.Length - 1)
            {
                powerupImageSlots[i].sprite = null;
                powerupImageSlots[i].color = new Color(0, 0, 0, 0);
            }
            else
            {
                powerupImageSlots[i].sprite = queuedPowerupsArray[i].powerupImage;
                powerupImageSlots[i].color = Color.white;
            }
        }
    }
}
