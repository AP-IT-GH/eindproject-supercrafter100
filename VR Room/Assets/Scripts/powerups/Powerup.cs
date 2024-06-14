using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace powerups
{
    public abstract class Powerup
    {
        public abstract void Activate(PowerupManager powerupManager);
        public abstract void ActivateRotten(PowerupManager powerupManager);
    }
}
