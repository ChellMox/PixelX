using UnityEngine;
using UnityEngine.InputSystem;


namespace Assets.PixelCrew.Hero.Skripts
{

    public class hero_movement : MonoBehaviour
    {
        [SerializeField] private Hero1 _hero;


        public void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            var derectionX = context.ReadValue<float>();
            _hero.SetDirectionX(derectionX);
        }

        public void OnSaySomething(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.SaySomething();
            }
        }

        public void OnVerticalMovement(InputAction.CallbackContext context)
        {
            var directionY = context.ReadValue<float>();
           _hero.SetDirectionY(directionY);
        }

        public void OnInteract(InputAction.CallbackContext context)//—◊»“¿À» —  À¿¬€ ¡” ¬” ≈
        {
            if (context.canceled)
            {
                _hero.Interact();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)           //—◊»“¿À» —  Ã€ÿ » À Ã
        {
            if (context.canceled)
            {
                Debug.Log("¬˚ Ì‡Ê‡ÎË À Ã");
                _hero.Attack();
            }
        }

    }
}

