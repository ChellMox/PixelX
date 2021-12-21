using System.Collections;
using UnityEngine;

namespace Assets.PixelCrew.Hero.Skripts
{
    public class ArmHeroComponent : MonoBehaviour
    {


        public void ArmHero(GameObject go)
        {
            var hero = go.GetComponent<Hero1>();
            if (hero != null)
            {
                hero.ArmHero();
            }
        }

    }
}