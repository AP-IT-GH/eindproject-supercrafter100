using System;
using UnityEngine;

namespace powerups
{
    public class ExtraLifePowerup: Powerup
    {
        public override Sprite powerupImage { get; set; }
        
        public ExtraLifePowerup(Sprite powerupImage)
        {
            this.powerupImage = powerupImage;
        }
        
        public override void Activate(PowerupManager powerupManager)
        {
            GameManager gm = powerupManager.GetComponent<GameManager>();
            if (gm.lives < 3) gm.lives++;
            gm.UpdateLives();
        }

        public override void ActivateRotten(PowerupManager powerupManager)
        {
            // Shouldn't do anything, we're just going to remove one life
        }
    }
}