using UnityEngine;

public class CrouchingState : State
{
    float playerSpeed;
    bool belowCeiling;
    bool crouchHeld;

    bool grounded;
    float gravityValue;
    Vector3 currentVelocity;

    // Constactur'�m�z.
    public CrouchingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        // Giri�te e�ilme trigger'�n� tetikliyoruz.
        character.animator.SetTrigger("crouch");
        belowCeiling = false; // "Karakterin aya�a geri kalkmas�n� engelleyen bir collider varm� ?" kontrol� i�in kulland���m�z bool.
        crouchHeld = false;   // Bu de�erin true olmas� demek karakter aya�a kalkabilir demek. ba�lang��ta false olmas�n�n sebebi aya�a kalk�p kalkamayaca��n�n kontrol edilmesi gerek.
        gravityVelocity.y = 0;

        // Character script'inden �ekmemiz gereken de�erleri �ekip d�zenliyoruz.
        playerSpeed = character.crouchSpeed;
        character.controller.height = character.crouchColliderHeight; // Karakterin boyunu e�ilirkenki collider'ine atama yap�yoruz.
        character.controller.center = new Vector3(0f, character.crouchColliderHeight / 2f, 0f); // e�ilirkenki collider'in merkezini normal boyunun yar�s�na indiriyoruz. T�m collider'i 3/4'�ne sabitliyoruz
        grounded = character.controller.isGrounded; 
        gravityValue = character.gravityValue;


    }

  
    // StandingState'te oldu�u gibi bu state'te de e�ilirken y�r�yebilmek i�in bir nevi kopyala yap��t�r yapt�k.
    public override void HandleInput()
    {
        base.HandleInput();
        if (crouchAction.triggered && !belowCeiling)
        {
            crouchHeld = true;
        }
        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
    }

    // Animat�rdeki h�z� set edip, aya�a kalkabiliyorsak StandingState'e ge�i�i sa�l�yoruz
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);

        if (crouchHeld)
        {
            stateMachine.ChangeState(character.standing);
        }
    }

    // Her state'te oldu�u gibi fizik kontrollerini yapt�k.
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        belowCeiling = CheckCollisionOverlap(character.transform.position + Vector3.up * character.normalColliderHeight); // kalk��� engelleyen nesnenin kontrol�n� a�a��daki kontrol fonksiyonunu referans alarak  yapt�k.
        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }
        currentVelocity = Vector3.Lerp(currentVelocity, velocity, character.velocityDampTime);

        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);

        if (velocity.magnitude > 0) // hareket vekt�r�m�z�n uzunlu�u var ise karakterimizin o tarafa do�ru d�nm�� oluyor.
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }

    // Yapt���m�z collider boyutu ve h�z de�i�ikliklerini eski haline getirip animator'deki move trigger'�n� tetikledik. 
    public override void Exit()
    {
        base.Exit();
        character.controller.height = character.normalColliderHeight;
        character.controller.center = new Vector3(0f, character.normalColliderHeight / 2f, 0f);
        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y); // kalkar kalkmaz istedi�imiz h�zda hareket edebilmek i�in bu de�eri normalize ettik.
        character.animator.SetTrigger("move");
    }


    // Karakterin �zerinde aya�a kalkmas�n� engelleyecek nesne varsa true, yoksa false d�nd�recek olan fonksiyon.
    // True ise karakterin aya�a kalkmas� engellenecek, false ise karakter tekrar aya�a kalkabilecek.
    public bool CheckCollisionOverlap(Vector3 targetPositon)
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;

        Vector3 direction = targetPositon - character.transform.position;
        if (Physics.Raycast(character.transform.position, direction, out hit, character.normalColliderHeight, layerMask))
        {
            Debug.DrawRay(character.transform.position, direction * hit.distance, Color.red);
            return true;
        }
        else
        {
            Debug.DrawRay(character.transform.position, direction * character.normalColliderHeight, Color.green);
            return false;
        }
    }


}