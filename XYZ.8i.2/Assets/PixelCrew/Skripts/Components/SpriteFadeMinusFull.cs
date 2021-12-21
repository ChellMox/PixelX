using UnityEngine;

namespace Assets.Scripts.Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteFadeMinusFull : MonoBehaviour
    {
        //[SerializeField] private float _speed;
        private SpriteRenderer _renderer;
        [SerializeField] private float finalValue;
        [SerializeField] private float alphPlus;
        private float alph;
        [SerializeField] private int PlusOrMinus;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (PlusOrMinus == 1)
            {
                if (alph < finalValue)
                {
                    var alpha = _renderer.color.g;
                    //var curentAlpha = _renderer.color.a;
                    //var alph = Mathf.Lerp(curentAlpha, finalValue, Time.deltaTime * _speed);
                    alph = alph + alphPlus;
                    _renderer.color = new Color(alph, alph, alph, _renderer.color.a);

                }
            }
            else if (PlusOrMinus == 2)
            {
                if (alph > finalValue)
                {
                    var alpha = _renderer.color.g;
                    //var curentAlpha = _renderer.color.a;
                    //var alph = Mathf.Lerp(curentAlpha, finalValue, Time.deltaTime * _speed);
                    alph = alph - alphPlus;
                    _renderer.color = new Color(alph, alph, alph, _renderer.color.a);
                }
            }
            else if (PlusOrMinus == 3)
            {
                if (alph < finalValue)
                {
                    var alpha = _renderer.color.a;
                    //var curentAlpha = _renderer.color.a;
                    //var alph = Mathf.Lerp(curentAlpha, finalValue, Time.deltaTime * _speed);
                    alph = alph + alphPlus;
                    _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, alph);
                }
            }
        }
    }
}

