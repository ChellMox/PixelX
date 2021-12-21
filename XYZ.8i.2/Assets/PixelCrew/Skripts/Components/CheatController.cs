using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;

public class CheatController : MonoBehaviour
{

    [SerializeField] private CheatItem[] _cheats; //������ � ������� �������� "��� ����" (��� �� ��������� �� ���� �����)
    [SerializeField] private float _inputTimeToLive;  // ��� ������� ����� ���� _currentInput ( ��� ���� �� ��������� ����, �������� 2 ��� � ������ �����, ��� ���� �������� ������, �� �� ���������)

    private string _currentInput;                     // ������ ������� ���� �� OnTextInput, ������� ����� ����
    private float _inputTime; // ��� ��������� ����� ���������� ������� �����, ��� ��� �� ����� ��� ����� ����������� ��� ��� � �������� �� ������ (���� ������ ���� ���������� ������� �� ������������)


    private void Awake()
    {
        Keyboard.current.onTextInput += OnTextInput; // ������� ��� ������������� �� ������ ����� ������ � �����
    }

    private void OnDestroy()
    {
        Keyboard.current.onTextInput -= OnTextInput; // ������� ��� ��� ���� ������ � ����� ������ �� �����      +=  ��� �������� �� �������, � -= ��� ������� �� �������
    }

    private void OnTextInput(char inputChar) // ����� �� ����� ���-�� ������� �� ���������� � ����� �������� � ���� �����, ���� ������������ ������ ���� �������� ������ _currentInput
    {
        _currentInput += inputChar; // ��������� ����� ���� ������� ������ � ����� � ������
        _inputTime = _inputTimeToLive;  //���������� ����� �� ������ � ������
        FindAnyCheats(); // �������� ����� � ��������� ������ �����-������ �� �����
    }

    private void FindAnyCheats()
    {
        foreach(var cheatItem in _cheats) // foreach - ��� �������� ����� ������� ��������� ���������� ������ ������� ������� ��� ��������� (���������� �� ������� ��-�����
        {
            if(_currentInput.Contains(cheatItem.Name)) // ��������� �� � ����� ������ ��� ����
            {
                cheatItem.Action.Invoke();
                _currentInput = string.Empty;
            }
        }
    }


    private void Update()
    {
        if(_inputTime < 0) // ���� ����� ���� < 0 �� ������� ������
        {
            _currentInput = string.Empty;
        }
        else 
        {
            _inputTime -= Time.deltaTime;
        }
    }

}




[Serializable]
public class CheatItem
{
    [SerializeField]public string Name; // ��� ����� ��������� ������������������ ������ 
    [SerializeField]  public UnityEvent Action; // ��� ������ ��� ����� ������� � �����
}