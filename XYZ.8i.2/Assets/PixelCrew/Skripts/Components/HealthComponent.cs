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
        [SerializeField] private int _poison = 0; //������ ���������

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
                Debug.Log("�������");
                _health -= damageValye;
                _onDamage?.Invoke(); // ?.Invoke(); ��������� �� ���� �� ����� � ������ ����� ����� ������� �����


                if (_health <= 0)
                {
                    _onDie?.Invoke();
                }
            }
            else
            {
                _onDamage?.Invoke(); // ?.Invoke(); ��������� �� ���� �� ����� � ������ ����� ����� ������� �����
            }
        }

        public IEnumerator DamagePoison(int _damagePoison) //��������� 1
        {
            Debug.Log("������2");
                Debug.Log("������� �����");

                yield return new WaitForSeconds(2);  // ���� 2 ���
                Debug.Log("��������2");

                if (_antidote == 0) //���� �� ��� 2 ��� �� ������ ������ �������, �� 
                {
                    if (_chekHit == false)
                    {
                        _health -= _damagePoison;
                        _onDamagePoison?.Invoke(); // ?.Invoke(); ��������� �� ���� �� ����� � ������ ����� ����� ������� �����
                        if (_health <= 0)
                        {
                            _onDie?.Invoke();
                        }
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.2f);  // ���� 0.2 ���
                        Debug.Log("��������0.2");
                        _health -= 10;
                        _onDamagePoison?.Invoke(); // ?.Invoke(); ��������� �� ���� �� ����� � ������ ����� ����� ������� �����
                        if (_health <= 0)
                        {
                            _onDie?.Invoke();
                        }
                    }                
                }
        }

        public IEnumerator PoinsLow(int _damagePoison) //������ 1
        {
            Debug.Log("������1");

                _antidote = 0;  //��������, ���� ����� �� �������, �� ������� ����������� �� ��� ��������� � ��� �� �����
                for (int p = 0; p < 10; p++) //������� ��������� ������� 10 ���
                {
                    if (_antidotescore == 1)
                    {
                        break;
                        _antidotescore = 0; // ���� ����� ����� ���� ���������� ����� ������
                        _antidote = 0; // �������� ���, ����� �� �������
                        _poison = 0; //���������� � 0 ���� �������� � Poins ��������� ������������� ��� �����
                        this.gameObject.GetComponent<SpriteRenderer>().color = _calor;
                    }
                    if (_antidote == 0) // ������ ��� ����� ����������, ���������, �� ������ �� �� ������� � ��� �� ��
                    {
                        yield return StartCoroutine(DamagePoison(_damagePoison)); //��������� � DamagePoison
                    }

                }
                this.gameObject.GetComponent<SpriteRenderer>().color = _calor;
                _poison = 0; //���������� � 0 ���� �������� � Poins ��������� ������������� ��� �����
                _antidote = 0; // �������� ��� ����� �� �������
                _antidotescore = 0; // ���� ����� ����� ���� ���������� ����� ������
        }

        public void Poins(int _damagePoison)    //������ 1
        {
            Debug.Log("������0");
            
                _antidote = 0;
                _antidotescore = 0;
                _poison++;
                
                if(_poison==1 && _antidote==0)
                {
                    _calor = this.gameObject.GetComponent<SpriteRenderer>().color;
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }

                if (_poison == 1) //���� ������ 1 � ������� ��� ����� ��� � PoinsLow �� �������� ��
                {
                    StartCoroutine(PoinsLow(_damagePoison));   //��������� � PoinsLow
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

