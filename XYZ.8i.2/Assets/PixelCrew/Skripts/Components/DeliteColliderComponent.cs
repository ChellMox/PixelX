using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliteColliderComponent : MonoBehaviour
{
    public IEnumerator Delete3(int i) //принимаем 1
    {
        Debug.Log("принял2");
        if (i == 1)
        {
            yield return new WaitForSeconds(2);  // ждем 2 сек
            GetComponent<BoxCollider2D>().enabled = false;

        }
    }

    public IEnumerator Delete2(int j) //принял 1
    {
        Debug.Log("принял1");

        if (j == 1)
        {
                    yield return StartCoroutine(Delete3(1)); //переходим к DamagePoison   
        }
    }

    public void Delete1(int k)    //принял 1
    {
        if(k==1)
        {
            StartCoroutine(Delete2(1));   //переходим к PoinsLow
            k = 0;
        }
                
    }
}
