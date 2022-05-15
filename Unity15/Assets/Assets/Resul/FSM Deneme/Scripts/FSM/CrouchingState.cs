using UnityEngine;

public class CrouchingState : State
{
    float playerSpeed;
    bool belowCeiling;
    bool crouchHeld;

    bool grounded;
    float gravityValue;
    Vector3 currentVelocity;

    // Constactur'ýmýz.
    public CrouchingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        // Giriþte eðilme trigger'ýný tetikliyoruz.
        character.animator.SetTrigger("crouch");
        belowCeiling = false; // "Karakterin ayaða geri kalkmasýný engelleyen bir collider varmý ?" kontrolü için kullandýðýmýz bool.
        crouchHeld = false;   // Bu deðerin true olmasý demek karakter ayaða kalkabilir demek. baþlangýçta false olmasýnýn sebebi ayaða kalkýp kalkamayacaðýnýn kontrol edilmesi gerek.
        gravityVelocity.y = 0;

        // Character script'inden çekmemiz gereken deðerleri çekip düzenliyoruz.
        playerSpeed = character.crouchSpeed;
        character.controller.height = character.crouchColliderHeight; // Karakterin boyunu eðilirkenki collider'ine atama yapýyoruz.
        character.controller.center = new Vector3(0f, character.crouchColliderHeight / 2f, 0f); // eðilirkenki collider'in merkezini normal boyunun yarýsýna indiriyoruz. Tüm collider'i 3/4'üne sabitliyoruz
        grounded = character.controller.isGrounded; 
        gravityValue = character.gravityValue;


    }

  
    // StandingState'te olduðu gibi bu state'te de eðilirken yürüyebilmek için bir nevi kopyala yapýþtýr yaptýk.
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

    // Animatördeki hýzý set edip, ayaða kalkabiliyorsak StandingState'e geçiþi saðlýyoruz
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);

        if (crouchHeld)
        {
            stateMachine.ChangeState(character.standing);
        }
    }

    // Her state'te olduðu gibi fizik kontrollerini yaptýk.
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        belowCeiling = CheckCollisionOverlap(character.transform.position + Vector3.up * character.normalColliderHeight); // kalkýþý engelleyen nesnenin kontrolünü aþaðýdaki kontrol fonksiyonunu referans alarak  yaptýk.
        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }
        currentVelocity = Vector3.Lerp(currentVelocity, velocity, character.velocityDampTime);

        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);

        if (velocity.magnitude > 0) // hareket vektörümüzün uzunluðu var ise karakterimizin o tarafa doðru dönmüþ oluyor.
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }

    // Yaptýðýmýz collider boyutu ve hýz deðiþikliklerini eski haline getirip animator'deki move trigger'ýný tetikledik. 
    public override void Exit()
    {
        base.Exit();
        character.controller.height = character.normalColliderHeight;
        character.controller.center = new Vector3(0f, character.normalColliderHeight / 2f, 0f);
        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y); // kalkar kalkmaz istediðimiz hýzda hareket edebilmek için bu deðeri normalize ettik.
        character.animator.SetTrigger("move");
    }


    // Karakterin üzerinde ayaða kalkmasýný engelleyecek nesne varsa true, yoksa false döndürecek olan fonksiyon.
    // True ise karakterin ayaða kalkmasý engellenecek, false ise karakter tekrar ayaða kalkabilecek.
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