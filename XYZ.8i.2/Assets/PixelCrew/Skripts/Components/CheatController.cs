using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;

public class CheatController : MonoBehaviour
{

    [SerializeField] private CheatItem[] _cheats; //массив в котором хранятся "чит коды" (тут сы ссылаемся на новй класс)
    [SerializeField] private float _inputTimeToLive;  // вот столько будет жить _currentInput ( тип если не правильно ввел, подождал 2 сек и заново начал, или если медленно сделал, то не сработало)

    private string _currentInput;                     // храним текущий ввод из OnTextInput, которая будет жить
    private float _inputTime; // эта переменна будет сбрасывать текущий инпут, так как мы будем все время отлавливать чит код в нажатиях на кнопки (чтоб строка куда записываем нажатия не разросталась)


    private void Awake()
    {
        Keyboard.current.onTextInput += OnTextInput; // говорим что подписываемся на чтение ввода текста с клавы
    }

    private void OnDestroy()
    {
        Keyboard.current.onTextInput -= OnTextInput; // говорим что тут ввод текста с клавы БОЛЬШЕ НЕ БУДЕТ      +=  это подписка на события, а -= это отписка от события
    }

    private void OnTextInput(char inputChar) // когда мы будем что-то воодить на клавиатуре м будем попадать в этот метод, чтоб обрабатывать текщий ввод создадим строку _currentInput
    {
        _currentInput += inputChar; // добавляем лобой знак который читали с клавы в строку
        _inputTime = _inputTimeToLive;  //сбрасываем время до сброса с строки
        FindAnyCheats(); // пытаемся найти в полученой строке какой-нибудь из читов
    }

    private void FindAnyCheats()
    {
        foreach(var cheatItem in _cheats) // foreach - это ключевое слово которое позволяет перебирать каждый элемент массива или коллекции (проходимся по каждому эл-менту
        {
            if(_currentInput.Contains(cheatItem.Name)) // находится ла и вашей строке код чита
            {
                cheatItem.Action.Invoke();
                _currentInput = string.Empty;
            }
        }
    }


    private void Update()
    {
        if(_inputTime < 0) // если инпут тайм < 0 то сбросим строку
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
    [SerializeField]public string Name; // тут будем содержать последовательность клавиш 
    [SerializeField]  public UnityEvent Action; // тут храним что нужно сделать в ответ
}