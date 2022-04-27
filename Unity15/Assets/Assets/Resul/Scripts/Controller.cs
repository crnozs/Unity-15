using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Metrics")]
    public float damp;
    [Range(1, 20)]
    public float rotationSpeed; // 1-20 aras� d�n�� h�z�.
    float normalFov;
    public float sprintFov;

    float inputX;
    float inputY;
    float maxSpeed;

    public KeyCode sprintButton = KeyCode.LeftShift;
    public KeyCode walkButton = KeyCode.C;

    public Transform model;

    Animator anim;
    Vector3 stickDirection; // Karakterin takip edece�i vector3 pozisyonu. (Bu pozisyona do�ru gidecek b�ylece kameradan da gitti�i y�ne do�ru karakterin d�nd��� g�r�lecek)
    Camera mainCam;


    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
        normalFov = mainCam.fieldOfView;
    }
    // G�DECE�� Y�N� VE BU Y�NE TAK�B� GER�EKLE�T�RME
    private void LateUpdate()
    {
        inputMove();
        inputRotation();
        Movement();
    }
    //KARAKTER�N HANG� HIZDA HAREKET EDECE��N� BEL�RLEME
    void Movement()
    {
        stickDirection = new Vector3(inputX, 0, inputY);

        if (Input.GetKey(sprintButton))
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, sprintFov, 2 * Time.deltaTime); // Karakter depar atarken h�zlanma efekti vermek i�in.

            maxSpeed = 2;
            inputX = 2* Input.GetAxis("Horizontal");
            inputY = 2* Input.GetAxis("Vertical");

        }else if (Input.GetKey(walkButton))
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, 2 * Time.deltaTime); // Karakter depar atmazken h�zlanma efektini kald�rmak i�in.

            maxSpeed = 0.2f;
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");

        }else
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, 2 * Time.deltaTime); // Karakter depar atmazken h�zlanma efektini kald�rmak i�in.

            maxSpeed = 1;
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
        }
    }

    //KARAKTER�M�Z� AN�MASYONLARLA HAREKET ETT�RME
    void inputMove()
    {
        anim.SetFloat("Blend", Vector3.ClampMagnitude(stickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10); // H�z�m�z� 0 ile belirlenen h�z aras�na ortalama de�er olarak Clamp'liyoruz.
    }


    void inputRotation()
    {
        Vector3 rotOfset = mainCam.transform.TransformDirection(stickDirection); // karakter ile kameran�n ayn� y�ne bakmas�n� sa�layacak
        rotOfset.y = 0; // Karakter yukar� a�a�� hareket etmeyece�i i�in y sini 0 a e�itliyoruz.

        model.forward = Vector3.Slerp(model.forward, rotOfset, Time.deltaTime * rotationSpeed); //Karakterimiz de rotationOfset'e do�ru d�n�� yapacak.
    }
}
