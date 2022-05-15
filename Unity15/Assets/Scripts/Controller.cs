using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
   
    [Header("Metrics")]
    public float damp;
    [Range(1, 20)]
    public float rotationSpeed; // 1-20 arasý DÝRACTÝONAL modda arasý dönüþ hýzý.
    [Range(1, 20)]
    public float strafeTurnSpeed; // 1-20 arasý STRAFE modda dönüþ hýzý.
    float normalFov; // normal kamera açýsý
    public float sprintFov; // sprint kamera açýsý

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
        Movement();
    }

   
    //KARAKTERÝMÝZ ÝÇÝN HAREKET TÝPÝ BELÝRLEME
    public enum MovementType
    {
        Strafe,
        Directional
    };
    public MovementType hareketTipi;


    //KARAKTERÝN HANGÝ HIZDA HAREKET EDECEÐÝNÝ BELÝRLEME
    void Movement()
    {
        if (hareketTipi == MovementType.Strafe) // SAVAÞ ANINDA ÝKEN
        {
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");

            anim.SetFloat("iX", inputX, damp, Time.deltaTime * 10);
            anim.SetFloat("iY", inputY, damp, Time.deltaTime * 10);


            var hareketEdiyor = inputX != 0 || inputY != 0;
            if (hareketEdiyor)
            {
                float yawCamera = mainCam.transform.rotation.eulerAngles.y; // Kameramýzýn y açýsýnda dönüþ miktarýný tutalým
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), strafeTurnSpeed * Time.fixedDeltaTime); // Karakterimzin dönüþü kameranýn dönüþüne eþit oluyor.
                anim.SetBool("isStrafeMoving", true);

            }
            else
            {
                anim.SetBool("isStrafeMoving", false);
            }


        }//Strafe

        if (hareketTipi == MovementType.Directional) //SAVAÞ ANINDA DEÐÝL ÝKEN
        {
            stickDirection = new Vector3(inputX, 0, inputY);

            inputMove();
            inputRotation();

            if (Input.GetKey(sprintButton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, sprintFov, 2 * Time.deltaTime); // Karakter depar atarken hýzlanma efekti vermek için.

                maxSpeed = 2;
                inputX = 2 * Input.GetAxis("Horizontal");
                inputY = 2 * Input.GetAxis("Vertical");

            }
            else if (Input.GetKey(walkButton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, 2 * Time.deltaTime); // Karakter depar atmazken hýzlanma efektini kaldýrmak için.

                maxSpeed = 0.5f;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");

            }
            else
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, 2 * Time.deltaTime); // Karakter depar atmazken hýzlanma efektini kaldýrmak için.

                maxSpeed = 1;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            }
        }//Directional



    }



    //KARAKTERÝMÝZÝ ANÝMASYONLARLA HAREKET ETTÝRME
    void inputMove()
    {
        anim.SetFloat("speed", Vector3.ClampMagnitude(stickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10); // Hýzýmýzý 0 ile belirlenen hýz arasýna ortalama deðer olarak Clamp'liyoruz.
    }

    void inputRotation()
    {
        Vector3 rotOfset = mainCam.transform.TransformDirection(stickDirection); // karakter ile kameranýn ayný yöne bakmasýný saðlayacak
        rotOfset.y = 0; // Karakter yukarý aþaðý hareket etmeyeceði için y sini 0 a eþitliyoruz.

        model.forward = Vector3.Slerp(model.forward, rotOfset, Time.deltaTime * rotationSpeed); //Karakterimiz de rotationOfset'e doðru dönüþ yapacak.
    }


}
