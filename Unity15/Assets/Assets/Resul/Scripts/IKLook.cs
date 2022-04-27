using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKLook : MonoBehaviour
{

    Animator anim;
    Camera mainCam;
    
    //Caching.
    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    //KARAKTER�M�Z�N BAKTI�IMIZ Y�NE D�NMES�N� SA�LAYALIM
    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtWeight(.7f,0.5f,1.3f,.5f,.5f);         //Karakterin v�cudunun hangi b�l�m�n�n(bizim i�in �nemli olan v�cut ve ba� d�n���)
                                                          //Ray'den tarafa ne kadar d�nece�ini de�erlerle belirleyelim. 

        Ray lookAtRay = new Ray(transform.position, mainCam.transform.forward);//Karakterin pozisyonundan kameran�n bak�� y�n�ne do�ru bir ���n g�nderelim.
        anim.SetLookAtPosition(lookAtRay.GetPoint(25));//

    }

}
