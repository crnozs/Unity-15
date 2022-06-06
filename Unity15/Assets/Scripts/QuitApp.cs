using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApp : MonoBehaviour
{

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("escaped");
        
    }
}
