using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.PixelCrew.Hero.Skripts
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask _GroundLayer;
        [SerializeField] private LayerMask _BarrelLayer;
        private Collider2D _collider2d;

        public bool IsTouchingLayer;

        private void Awake()
        {
            _collider2d = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            IsTouchingLayer = _collider2d.IsTouchingLayers(_GroundLayer ^ _BarrelLayer);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IsTouchingLayer = _collider2d.IsTouchingLayers(_GroundLayer ^ _BarrelLayer);
        }



      
    }
}