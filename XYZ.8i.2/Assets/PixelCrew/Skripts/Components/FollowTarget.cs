using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _damping;

        private void LateUpdate()
        {
            var distantion = new Vector3(_target.position.x, _target.position.y, _target.position.z);
            transform.position = distantion;
        }
    }
}