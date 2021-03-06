using System;
using UnityEngine;

namespace Assets.Models
{

    [Serializable]
    public class PlayerData
    {
        public int _coins;
        public int _hp;
        public bool IsArmed;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}