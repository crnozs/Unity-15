using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerDENEME : MonoBehaviour
{
    public Transform targetToFollow;
    public Vector3 offsetFromTarget;

    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float yawSpeed = 100f;

    float currentZoom = 10f; //Anl�k zoom miktar�m�z
    float currentYawX = 0f;
    //float currentYawY = 0f; //Yukar� a�a�� denedi�imde �ok sa�ma �eyler oluyor :D

    float pitch = 2f; // Karakterimizin boyu gibi d���nebiliriz


    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; // += yerine -= yapmam�z�n sebebi kamera arkada oldu�u i�in
        //eksenler, yukar� g�t�rd���m�zde zoom out a�a�� indirdi�imizde zoom in olsun diye.

        currentZoom = Mathf.Clamp(currentZoom,minZoom,maxZoom); // anl�k zoom de�erimizi max ve min de�erleri aras�na s�k��t�rd�k.

        currentYawX -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        //currentYawY -= Input.GetAxis("Vertical") * yawSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = targetToFollow.position - offsetFromTarget * currentZoom; // Zoom'u sabitliyoruz.
        transform.LookAt(targetToFollow.position + Vector3.up * pitch); // Kameray� karakterin belli miktar �zerinde sabit tutuyoruz.

        transform.RotateAround(targetToFollow.position, Vector3.up, currentYawX); // A ve D tu�lar�yla kameray� sa�a sola �eviriyoruz.
        //transform.RotateAround(targetToFollow.position, Vector3.right, currentYawY); // W ve S tu�lar�yla kameray� yukar� a�a�� �eviriyoruz.
    }
}
