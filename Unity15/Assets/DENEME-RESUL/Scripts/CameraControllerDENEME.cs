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

    float currentZoom = 10f; //Anlýk zoom miktarýmýz
    float currentYawX = 0f;
    //float currentYawY = 0f; //Yukarý aþaðý denediðimde çok saçma þeyler oluyor :D

    float pitch = 2f; // Karakterimizin boyu gibi düþünebiliriz


    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; // += yerine -= yapmamýzýn sebebi kamera arkada olduðu için
        //eksenler, yukarý götürdüðümüzde zoom out aþaðý indirdiðimizde zoom in olsun diye.

        currentZoom = Mathf.Clamp(currentZoom,minZoom,maxZoom); // anlýk zoom deðerimizi max ve min deðerleri arasýna sýkýþtýrdýk.

        currentYawX -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        //currentYawY -= Input.GetAxis("Vertical") * yawSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = targetToFollow.position - offsetFromTarget * currentZoom; // Zoom'u sabitliyoruz.
        transform.LookAt(targetToFollow.position + Vector3.up * pitch); // Kamerayý karakterin belli miktar üzerinde sabit tutuyoruz.

        transform.RotateAround(targetToFollow.position, Vector3.up, currentYawX); // A ve D tuþlarýyla kamerayý saða sola çeviriyoruz.
        //transform.RotateAround(targetToFollow.position, Vector3.right, currentYawY); // W ve S tuþlarýyla kamerayý yukarý aþaðý çeviriyoruz.
    }
}
