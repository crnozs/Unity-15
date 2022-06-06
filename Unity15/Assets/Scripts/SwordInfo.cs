using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInfo : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("UI"))
        {
            var a = other.gameObject.transform.GetChild(0).gameObject;
            a.SetActive(true);
            Debug.Log("E");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("UI"))
        {
            var a = other.gameObject.transform.GetChild(0).gameObject;
            a.SetActive(false);
            Debug.Log("Closer");
        }
    }
}
