using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliteColliderComponent : MonoBehaviour
{
    public IEnumerator Delete3(int i) //��������� 1
    {
        Debug.Log("������2");
        if (i == 1)
        {
            yield return new WaitForSeconds(2);  // ���� 2 ���
            GetComponent<BoxCollider2D>().enabled = false;

        }
    }

    public IEnumerator Delete2(int j) //������ 1
    {
        Debug.Log("������1");

        if (j == 1)
        {
                    yield return StartCoroutine(Delete3(1)); //��������� � DamagePoison   
        }
    }

    public void Delete1(int k)    //������ 1
    {
        if(k==1)
        {
            StartCoroutine(Delete2(1));   //��������� � PoinsLow
            k = 0;
        }
                
    }
}
