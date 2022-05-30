using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour

{
    AudioSource ses;
    bool IsMoving;

    void Start()
    {
        ses = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) IsMoving = true;
        else IsMoving = false;

        if (IsMoving && !ses.isPlaying) ses.Play(); 
        if (!IsMoving) ses.Stop(); 
    }
}
