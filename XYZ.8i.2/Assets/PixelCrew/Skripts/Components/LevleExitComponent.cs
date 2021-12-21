using Assets.Models;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.Components
{
    public class LevleExitComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();
            SceneManager.LoadScene(_sceneName); //загружаем сцену передавая ее имя
        }



    }
}

