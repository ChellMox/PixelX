using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.PixelCrew.Scripts.Components
{ 
    public class DestroyObject : MonoBehaviour
    {

        public void DestroyObject1()
        {
            Destroy(this.gameObject);
        }

    }


}