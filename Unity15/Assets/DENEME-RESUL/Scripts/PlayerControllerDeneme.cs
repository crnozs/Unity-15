using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))] // Ayný þekilde burada da request ediyoruz PlayerMotor'u.
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
        if (Input.GetMouseButtonDown(1))// Karakterimiz sað týkladýðýmýz noktaya gidecek
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit ,100 , movementMask))
            {
                motor.Noktayailerle(hit.point); // Karakterimiz sað týkladýðýmýz noktaya gidecek
                Debug.Log("Vurduðumuz nesne " + hit.collider.name + " Vurduðumuz nokta" + hit.point); // Hangi noktaya çýkladýðýmýzý Vector3 olarak görelim
                // Karakterimizi vurduðumuz þeye doðru hareket ettir
                // Hiçbir nesneye focus'lanma.
            }

        }

        if (Input.GetMouseButtonDown(0)) // Karakterimiz sol týkladýðýmýz nesneye focus'lanacak
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); //Mouse'nin olduðu yere kameradan ýþýn gönderiyoruz
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // Ýnteraktif nesneye týkladýk mý kontrol et.
                Interactible interactible = hit.collider.GetComponent<Interactible>();

                // týkladýysak o nesneyi focus al.
                if (interactible!=null)
                {

                }
            }

        }
    }
}
