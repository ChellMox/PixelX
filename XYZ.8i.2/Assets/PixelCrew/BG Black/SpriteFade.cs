using UnityEngine;

namespace Assets.Scripts.Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteFade : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private SpriteRenderer _renderer;
        [SerializeField] private float finalValue;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            var curentAlpha = _renderer.color.a;
            var alph = Mathf.Lerp(curentAlpha, finalValue, Time.deltaTime * _speed);
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, alph);
        }
    }
}
