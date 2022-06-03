using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInfo : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("UI"))
        {
            other.gameObject.SetActive(true);
            Debug.Log("E");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("UI"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("Closer");
        }
    }
}
