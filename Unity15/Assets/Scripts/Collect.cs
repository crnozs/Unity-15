using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collect : MonoBehaviour
{
    public static bool Collect1;
    public static bool Collect2;
    public static bool Collect3;
    public static bool Collect4;

    private void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("Quest"));
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (PlayerPrefs.GetInt("Quest") == 2)
            {
                SceneManager.LoadScene(4);
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Collect1"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.SetActive(false);
                Collect1 = true;

            }
        }

        if (other.gameObject.CompareTag("Collect2"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.SetActive(false);
                Collect2 = true;

            }
        }

        if (other.gameObject.CompareTag("Collect3"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.SetActive(false);
                Collect3 = true;

            }
        }

        if (other.gameObject.CompareTag("Collect4"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.SetActive(false);
                Collect4 = true;

            }
        }
    }

}
