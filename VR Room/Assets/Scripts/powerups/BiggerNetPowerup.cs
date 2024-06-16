using System.Collections;
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
            StartCoroutine(ActivateTiming(powerupManager));
        }

        public IEnumerator ActivateTiming(PowerupManager manager)
        {
            SizeDouble(manager.netObject);
            yield return new WaitForSeconds(10);
            SizeHalf(manager.netObject);
        }

        public override void ActivateRotten(PowerupManager powerupManager)
        {
            StartCoroutine(ActivateRottenTiming(powerupManager));
        }
        
        public IEnumerator ActivateRottenTiming(PowerupManager manager)
        {
            SizeHalf(manager.netObject);
            yield return new WaitForSeconds(10);
            SizeDouble(manager.netObject);
        }

        private void SizeDouble(GameObject net)
        {
            Vector3 scale = net.transform.localScale;
            scale.x *= 2;
            scale.y *= 2;
            scale.z *= 2;

            Vector3 position = net.transform.localPosition;
            position.x *= 2;
            position.z *= 2;

            net.transform.localScale = scale;
            net.transform.localPosition = position;
        }
        
        private void SizeHalf(GameObject net)
        {
            Vector3 scale = net.transform.localScale;
            scale.x /= 2;
            scale.y /= 2;
            scale.z /= 2;

            Vector3 position = net.transform.localPosition;
            position.x /= 2;
            position.z /= 2;

            net.transform.localScale = scale;
            net.transform.localPosition = position;
        }
    }    
}
