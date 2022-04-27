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

    //KARAKTERÝMÝZÝN BAKTIÐIMIZ YÖNE DÖNMESÝNÝ SAÐLAYALIM
    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtWeight(.7f,0.5f,1.3f,.5f,.5f);         //Karakterin vücudunun hangi bölümünün(bizim için önemli olan vücut ve baþ dönüþü)
                                                          //Ray'den tarafa ne kadar döneceðini deðerlerle belirleyelim. 

        Ray lookAtRay = new Ray(transform.position, mainCam.transform.forward);//Karakterin pozisyonundan kameranýn bakýþ yönüne doðru bir ýþýn gönderelim.
        anim.SetLookAtPosition(lookAtRay.GetPoint(25));//

    }

}
