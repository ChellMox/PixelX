using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins_10 : MonoBehaviour
{
    public Assets.PixelCrew.Hero.Skripts.Hero1 hero;
    public void CoinsPlus()
    {
        hero.CoinsScore10(1);
        Debug.Log("DA");
    }

}