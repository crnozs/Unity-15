using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float damp;

    [Range(1, 20)]
    public float rotationSpeed; // 1-20 aras� d�n�� h�z�.

    float inputX;
    float inputY;

    public Transform model;

    Animator anim;

    Vector3 stickDirection; // Karakterin takip edece�i vector3 pozisyonu. (Bu pozisyona do�ru gidecek b�ylece kameradan da gitti�i y�ne do�ru karakterin d�nd��� g�r�lecek)

    Camera mainCam;


    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
    }
    // G�DECE�� Y�N� VE BU Y�NE TAK�B� GER�EKLE�T�RME
    private void LateUpdate()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");


        stickDirection = new Vector3(inputX, 0, inputY);

        inputMove();
        inputRotation();

    }
    //KARAKTER�M�Z� AN�MASYONLARLA HAREKET ETT�RME
    void inputMove()
    {
        anim.SetFloat("Blend", Vector3.ClampMagnitude(stickDirection, 1).magnitude, damp, Time.deltaTime * 10); // H�z�m�z� 0-1 aras�na ortalama de�er olarak Clamp'liyoruz.

    }


    void inputRotation()
    {
        Vector3 rotOfset = mainCam.transform.TransformDirection(stickDirection); // karakter ile kameran�n ayn� y�ne bakmas�n� sa�layacak
        rotOfset.y = 0; // Karakter yukar� a�a�� hareket etmeyece�i i�in y sini 0 a e�itliyoruz.

        model.forward = Vector3.Slerp(model.forward, rotOfset, Time.deltaTime * rotationSpeed); //Karakterimiz de rotationOfset'e do�ru d�n�� yapacak.

    }
}
