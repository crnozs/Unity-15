using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float damp;

    [Range(1, 20)]
    public float rotationSpeed; // 1-20 arasý dönüþ hýzý.

    float inputX;
    float inputY;

    public Transform model;

    Animator anim;

    Vector3 stickDirection; // Karakterin takip edeceði vector3 pozisyonu. (Bu pozisyona doðru gidecek böylece kameradan da gittiði yöne doðru karakterin döndüðü görülecek)

    Camera mainCam;


    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
    }
    // GÝDECEÐÝ YÖNÜ VE BU YÖNE TAKÝBÝ GERÇEKLEÞTÝRME
    private void LateUpdate()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");


        stickDirection = new Vector3(inputX, 0, inputY);

        inputMove();
        inputRotation();

    }
    //KARAKTERÝMÝZÝ ANÝMASYONLARLA HAREKET ETTÝRME
    void inputMove()
    {
        anim.SetFloat("Blend", Vector3.ClampMagnitude(stickDirection, 1).magnitude, damp, Time.deltaTime * 10); // Hýzýmýzý 0-1 arasýna ortalama deðer olarak Clamp'liyoruz.

    }


    void inputRotation()
    {
        Vector3 rotOfset = mainCam.transform.TransformDirection(stickDirection); // karakter ile kameranýn ayný yöne bakmasýný saðlayacak
        rotOfset.y = 0; // Karakter yukarý aþaðý hareket etmeyeceði için y sini 0 a eþitliyoruz.

        model.forward = Vector3.Slerp(model.forward, rotOfset, Time.deltaTime * rotationSpeed); //Karakterimiz de rotationOfset'e doðru dönüþ yapacak.

    }
}
