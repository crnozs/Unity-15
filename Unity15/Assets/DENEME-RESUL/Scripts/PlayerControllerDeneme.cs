using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))] // Ayn� �ekilde burada da request ediyoruz PlayerMotor'u.
public class PlayerControllerDeneme : MonoBehaviour
{
    PlayerMotor motor;
    public LayerMask movementMask;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))// Karakterimiz sa� t�klad���m�z noktaya gidecek
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit ,100 , movementMask))
            {
                motor.Noktayailerle(hit.point); // Karakterimiz sa� t�klad���m�z noktaya gidecek
                Debug.Log("Vurdu�umuz nesne " + hit.collider.name + " Vurdu�umuz nokta" + hit.point); // Hangi noktaya ��klad���m�z� Vector3 olarak g�relim
                // Karakterimizi vurdu�umuz �eye do�ru hareket ettir
                // Hi�bir nesneye focus'lanma.
            }

        }

        if (Input.GetMouseButtonDown(0)) // Karakterimiz sol t�klad���m�z nesneye focus'lanacak
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); //Mouse'nin oldu�u yere kameradan ���n g�nderiyoruz
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // �nteraktif nesneye t�klad�k m� kontrol et.
                Interactible interactible = hit.collider.GetComponent<Interactible>();

                // t�klad�ysak o nesneyi focus al.
                if (interactible!=null)
                {

                }
            }

        }
    }
}
