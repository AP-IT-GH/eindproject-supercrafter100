using UnityEngine;

namespace powerups
{
    public class BiggerNetPowerup : Powerup
    {
        public override Sprite powerupImage { get; set; }

        public BiggerNetPowerup(Sprite powerupImage)
        {
            this.powerupImage = powerupImage;
        }

        public override void Activate(PowerupManager powerupManager)
        {
            
        }

        public override void ActivateRotten(PowerupManager powerupManager)
        {
            // Something something
        }
    }    
}
