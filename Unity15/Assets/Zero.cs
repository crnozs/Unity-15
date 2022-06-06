using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zero : MonoBehaviour
{
    PortalLevelLoader control;


    void Start()
    {
        PlayerPrefs.SetInt("x", 0);
        control = GameObject.FindObjectOfType<PortalLevelLoader>();
        int x = control.quest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
