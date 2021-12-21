using UnityEngine;


namespace Assets.PixelCrew.Scripts.Components
{
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;

        public void ApplyeDamage(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if(healthComponent!=null)
            {

                healthComponent.ApplyDamage(_damage);
            }
        }
    }
}    
