using UnityEngine;

public class Antidote : MonoBehaviour
{
    public Assets.PixelCrew.HealthComponent HealthComponent1;
    [SerializeField] private int hill;
    public void Antidote1()
    {
        HealthComponent1.Antidote(hill);
        Debug.Log("отправил");
    }
}
