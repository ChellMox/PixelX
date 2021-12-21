using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins_1 : MonoBehaviour
{
    public Assets.PixelCrew.Hero.Skripts.Hero1 hero;
    public void CoinsPlus()
    {
        Debug.Log("DA");
        hero.CoinsScore1(1);
        
    }

}
