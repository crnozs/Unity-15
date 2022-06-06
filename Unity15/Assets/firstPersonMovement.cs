using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonMovement : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeigh = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f; // 0 da alabilirdik ancak yerde dahi olsak yer çekimi gerçekte durmadýðý için 
                              // küçük bir sayýda çekimi sürdürmek için bu deðeri aldýk
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Vector3 move = new Vector3(x, 0f, z); global eksende hareket ettiriyor.
        Vector3 move = transform.right * x + transform.forward * z; //bu kod ile local eksende hareket ettirdik.

        characterController.Move(move * speed*Time.deltaTime);
       
        velocity.y += gravity * Time.deltaTime;
        // Delta Y = 1/2 * g * t^2 olduðundan zamanýn karesine ihtiyacýmýz var.
        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeigh * -2f * gravity); // V = sqr (h * -2 * g)
        }
    }
}
