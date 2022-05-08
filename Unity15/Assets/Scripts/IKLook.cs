using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKLook : MonoBehaviour
{
    float weight = 0.7f; // Bizim baktýðýmýz yöne bakmasýný, saldýrýrken tamamen kapatmak yerine kontrol edebilmek için bir deðiþkene baðlayalým.
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
        anim.SetLookAtWeight(weight,0.5f,1.3f,.5f,.5f);         //Karakterin vücudunun hangi bölümünün(bizim için önemli olan vücut ve baþ dönüþü)
                                                                //Ray'den tarafa ne kadar döneceðini deðerlerle belirleyelim. 

        Ray lookAtRay = new Ray(transform.position, mainCam.transform.forward);//Karakterin pozisyonundan kameranýn bakýþ yönüne doðru bir ýþýn gönderelim.
        anim.SetLookAtPosition(lookAtRay.GetPoint(25)); 

    }

    public void arttýr()
    {
        weight = Mathf.Lerp(weight, .7f, Time.fixedDeltaTime); //normal haldeyken kamerayla baktýðýmýz yöne bakacak

    }
    public void azalt() 
    {
        weight = Mathf.Lerp(weight, 0f, Time.fixedDeltaTime); //savaþ halindeyken kamerayla baktýðýmýz yöne bakmayacak. Bu da savaþ animasyonlarýnda takýlmayý ortadan kaldýracak.
    }

}
