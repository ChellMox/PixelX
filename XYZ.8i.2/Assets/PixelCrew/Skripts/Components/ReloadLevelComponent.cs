using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using Unity;


namespace Assets.Scripts.Components
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {
            //var session = FindObjectOfType<GameSession>();
            //session.LoadLastSave();

            //var scene = SceneManager.GetActiveScene();
            //SceneManager.LoadScene(scene.name);

            //System.Threading.Thread.Sleep(1000);
            Debug.Log("перезагружаемся");
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}






            



