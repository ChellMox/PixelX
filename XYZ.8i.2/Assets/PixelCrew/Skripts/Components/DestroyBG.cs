using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBG : MonoBehaviour
{
    [SerializeField]public float value=3;
    [SerializeField] public int speed = 1;

    private void Update()
    {        
        value-=Time.deltaTime*speed;

        if(value<=0)
        {
            Destroy(this.gameObject);
        }
    }
    
}
