using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKLook : MonoBehaviour
{
    float weight = 0.7f; // Bizim bakt���m�z y�ne bakmas�n�, sald�r�rken tamamen kapatmak yerine kontrol edebilmek i�in bir de�i�kene ba�layal�m.
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
        anim.SetLookAtWeight(weight,0.5f,1.3f,.5f,.5f);         //Karakterin v�cudunun hangi b�l�m�n�n(bizim i�in �nemli olan v�cut ve ba� d�n���)
                                                                //Ray'den tarafa ne kadar d�nece�ini de�erlerle belirleyelim. 

        Ray lookAtRay = new Ray(transform.position, mainCam.transform.forward);//Karakterin pozisyonundan kameran�n bak�� y�n�ne do�ru bir ���n g�nderelim.
        anim.SetLookAtPosition(lookAtRay.GetPoint(25)); 

    }

    public void artt�r()
    {
        weight = Mathf.Lerp(weight, .7f, Time.fixedDeltaTime); //normal haldeyken kamerayla bakt���m�z y�ne bakacak

    }
    public void azalt() 
    {
        weight = Mathf.Lerp(weight, 0f, Time.fixedDeltaTime); //sava� halindeyken kamerayla bakt���m�z y�ne bakmayacak. Bu da sava� animasyonlar�nda tak�lmay� ortadan kald�racak.
    }

}
