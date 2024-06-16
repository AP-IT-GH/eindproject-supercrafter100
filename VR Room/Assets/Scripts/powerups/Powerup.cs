using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace powerups
{
    public abstract class Powerup: MonoBehaviour
    {
        public abstract Sprite powerupImage { get; set; }
        public abstract void Activate(PowerupManager powerupManager);
        public abstract void ActivateRotten(PowerupManager powerupManager);
    }
}
