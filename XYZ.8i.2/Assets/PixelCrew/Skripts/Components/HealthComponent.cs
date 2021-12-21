using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.PixelCrew
{
    public class HealthComponent : MonoBehaviour
    {
        /*[Range(0, 100)]*/ [SerializeField] private int _health;
        [SerializeField] private int maxsHelth;
        [SerializeField] private Color _calor;
        [SerializeField] private bool _chekHit=false;
        [SerializeField] private UnityEvent _onDamagePoison;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;


        [SerializeField] private int _antidotescore = 0;
        [SerializeField] private int _antidote;
        [SerializeField] private int _poison = 0; //мешает стакаться

        public void ChekHit1()
        {
            _chekHit = true;
        }
        public void ChekHit2()
        {
            _chekHit = false;
        }
        public void ApplyDamage(int damageValye)
        {
            if (_chekHit == false)
            {
                Debug.Log("получил");
                _health -= damageValye;
                _onDamage?.Invoke(); // ?.Invoke(); проверяет не нулл ли дамаг и только после этого вызовет метод


                if (_health <= 0)
                {
                    _onDie?.Invoke();
                }
            }
            else
            {
                _onDamage?.Invoke(); // ?.Invoke(); проверяет не нулл ли дамаг и только после этого вызовет метод
            }
        }

        public IEnumerator DamagePoison(int _damagePoison) //принимаем 1
        {
            Debug.Log("принял2");
                Debug.Log("начинаю ждать");

                yield return new WaitForSeconds(2);  // ждем 2 сек
                Debug.Log("подождал2");

                if (_antidote == 0) //если за эти 2 сек не успели выпить антидот, то 
                {
                    if (_chekHit == false)
                    {
                        _health -= _damagePoison;
                        _onDamagePoison?.Invoke(); // ?.Invoke(); проверяет не нулл ли дамаг и только после этого вызовет метод
                        if (_health <= 0)
                        {
                            _onDie?.Invoke();
                        }
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.2f);  // ждем 0.2 сек
                        Debug.Log("подождал0.2");
                        _health -= 10;
                        _onDamagePoison?.Invoke(); // ?.Invoke(); проверяет не нулл ли дамаг и только после этого вызовет метод
                        if (_health <= 0)
                        {
                            _onDie?.Invoke();
                        }
                    }                
                }
        }

        public IEnumerator PoinsLow(int _damagePoison) //принял 1
        {
            Debug.Log("принял1");

                _antidote = 0;  //обнуляем, если этого не сделать, то антидот подобранный до яда сработает и яда не будет
                for (int p = 0; p < 10; p++) //говорим выполнять отнятие 10 раз
                {
                    if (_antidotescore == 1)
                    {
                        break;
                        _antidotescore = 0; // чтоб опять можно было остановить когда угодно
                        _antidote = 0; // обнуляем что, новый яд работал
                        _poison = 0; //возвращаем в 0 чтоб проверка в Poins сработала когдакоснемся яда вновь
                        this.gameObject.GetComponent<SpriteRenderer>().color = _calor;
                    }
                    if (_antidote == 0) // каждый раз перед отниманием, проверяем, не выпили ли мы антидот и кол во хп
                    {
                        yield return StartCoroutine(DamagePoison(_damagePoison)); //переходим к DamagePoison
                    }

                }
                this.gameObject.GetComponent<SpriteRenderer>().color = _calor;
                _poison = 0; //возвращаем в 0 чтоб проверка в Poins сработала когдакоснемся яда вновь
                _antidote = 0; // обнуляем что новый яд работал
                _antidotescore = 0; // чтоб опять можно было остановить когда угодно
        }

        public void Poins(int _damagePoison)    //принял 1
        {
            Debug.Log("принял0");
            
                _antidote = 0;
                _antidotescore = 0;
                _poison++;
                
                if(_poison==1 && _antidote==0)
                {
                    _calor = this.gameObject.GetComponent<SpriteRenderer>().color;
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }

                if (_poison == 1) //если принял 1 и приняло его когда фор в PoinsLow не работает то
                {
                    StartCoroutine(PoinsLow(_damagePoison));   //переходим к PoinsLow
                }  
        }

        public void Antidote(int hill)
        {
            _antidote = 1;
            _antidotescore = 1;
            _health += hill;

            if (_health > maxsHelth)
            {
                _health = maxsHelth;

            }
        }
    }
        
}

