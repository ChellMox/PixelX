using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace Assets.Scripts.Components
{
    public class EnterTriggerNew : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;
        [SerializeField] private string _tag;

        private Collider2D _collider2d;

        private void Awake()
        {
            _collider2d = GetComponent<Collider2D>();
        }

        public void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.gameObject.CompareTag(_tag))
            {
                _action?.Invoke();
            }

        }
    }
}