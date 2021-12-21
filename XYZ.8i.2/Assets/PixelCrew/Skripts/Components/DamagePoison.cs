using UnityEngine;

public class DamagePoison : MonoBehaviour
{
    public Assets.PixelCrew.HealthComponent HealthComponent1;
    [SerializeField] private int _damagePoison;

    public void DamagePoins1()
    {
        HealthComponent1.Poins(_damagePoison);
        Debug.Log("отправил");
    }
}
