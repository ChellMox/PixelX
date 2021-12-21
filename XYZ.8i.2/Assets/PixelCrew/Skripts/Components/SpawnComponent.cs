using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.PixelCrew
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instantiate = Instantiate(_prefab, _target.position, Quaternion.identity); //берем из таргета позицию относително героя
            instantiate.transform.localScale = _target.lossyScale;
        }
    }
}