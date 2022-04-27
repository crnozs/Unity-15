using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Metrics")]
    public float damp;
    [Range(1, 20)]
    public float rotationSpeed; // 1-20 arasý dönüþ hýzý.
    float normalFov;
    public float sprintFov;

    float inputX;
    float inputY;
    float maxSpeed;

    public KeyCode sprintButton = KeyCode.LeftShift;
    public KeyCode walkButton = KeyCode.C;

    public Transform model;

    Animator anim;
    Vector3 stickDirection; // Karakterin takip edeceði vector3 pozisyonu. (Bu pozisyona doðru gidecek böylece kameradan da gittiði yöne doðru karakterin döndüðü görülecek)
    Camera mainCam;


    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
        normalFov = mainCam.fieldOfView;
    }
    // GÝDECEÐÝ YÖNÜ VE BU YÖNE TAKÝBÝ GERÇEKLEÞTÝRME
    private void LateUpdate()
    {
        inputMove();
        inputRotation();
        Movement();
    }
    //KARAKTERÝN HANGÝ HIZDA HAREKET EDECEÐÝNÝ BELÝRLEME
    void Movement()
    {
        stickDirection = new Vector3(inputX, 0, inputY);

        if (Input.GetKey(sprintButton))
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, sprintFov, 2 * Time.deltaTime); // Karakter depar atarken hýzlanma efekti vermek için.

            maxSpeed = 2;
            inputX = 2* Input.GetAxis("Horizontal");
            inputY = 2* Input.GetAxis("Vertical");

        }else if (Input.GetKey(walkButton))
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, 2 * Time.deltaTime); // Karakter depar atmazken hýzlanma efektini kaldýrmak için.

            maxSpeed = 0.2f;
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");

        }else
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, 2 * Time.deltaTime); // Karakter depar atmazken hýzlanma efektini kaldýrmak için.

            maxSpeed = 1;
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
        }
    }

    //KARAKTERÝMÝZÝ ANÝMASYONLARLA HAREKET ETTÝRME
    void inputMove()
    {
        anim.SetFloat("Blend", Vector3.ClampMagnitude(stickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10); // Hýzýmýzý 0 ile belirlenen hýz arasýna ortalama deðer olarak Clamp'liyoruz.
    }


    void inputRotation()
    {
        Vector3 rotOfset = mainCam.transform.TransformDirection(stickDirection); // karakter ile kameranýn ayný yöne bakmasýný saðlayacak
        rotOfset.y = 0; // Karakter yukarý aþaðý hareket etmeyeceði için y sini 0 a eþitliyoruz.

        model.forward = Vector3.Slerp(model.forward, rotOfset, Time.deltaTime * rotationSpeed); //Karakterimiz de rotationOfset'e doðru dönüþ yapacak.
    }
}
