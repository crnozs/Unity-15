using UnityEngine;

public class StandingState : State // Ýlk state'imiz.
{
    float playerSpeed;
    float gravityValue; // Character Controller komponenti kullandýðýmýz için bu deðere ihtiyacýmýz var.

    bool jump;
    //bool crouch;
    bool grounded;
    bool sprint;
    bool drawWeapon;

    Vector3 currentVelocity;
    Vector3 cVelocity;

    // Yine ayný þekilde state'leri objelere atama yapýyoruz.
    public StandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }
    
    public override void Enter()
    {
        base.Enter(); // virtual enter fonksiyonu'nu trigger'lýyoruz.

        // State'e baþlangýç halinde herþeyi sýfýrlýyoruz.
        jump = false;
        //crouch = false;
        sprint = false;
        drawWeapon = false;
        input = Vector2.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        // Character scriptinden gerekli bilgileri çekiyoruz.
        velocity = character.playerVelocity;
        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }

    public override void HandleInput()
    {
        base.HandleInput(); // virual HandleInput fonksiyonu'nu trigger'lýyoruz.

        //aksiyon map'te tanýmladýðýmýz tuþlarý check ediyoruz.
        if (jumpAction.triggered)
        {
            jump = true;
        }

        if (sprintAction.triggered)
        {
            sprint = true;
        }

        if (drawWeaponAction.triggered)
        {
            drawWeapon = true;
        }

        // Aksiyon map'inde tanýmladýðýmýz deðerleri okuyup güncelliyoruz.
        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        

        // 
        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f; // Karakter koþarken kamera yukarý aþaðý sallanmasýn diye y'sini 0'a çekiyoruz.

    }


    // Animator üzerinden kontrol edilen herþeyin kontrolü buradan yapýlýyor.
    public override void LogicUpdate()
    {
        base.LogicUpdate(); //virtual LogicUpdate fonksiyonu'nu trigger'lýyoruz.

        //Input'lardan aldýðýmýz deðerleri animator'deki speed parametresine atýyoruz.
        character.animator.SetFloat("speed",input.magnitude, character.speedDampTime, Time.deltaTime);

        // Verilen aksiyona göre state deðiþtiriyoruz.
        if (sprint) //StandingState'ten SprintState'e geçiþ.
        {
            stateMachine.ChangeState(character.sprinting);
        }
        if (jump) // StandingState'ten JumpingState'e geçiþ.
        {
            stateMachine.ChangeState(character.jumping);
        }
        
        if (drawWeapon) // StandingState'ten CombatState'e geçiþ.
        {
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("drawWeapon");
        }
    }

    // Bu fonksiyon bizim karakterimizi hareket ettirmemizi saðlýyor
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded; // Character Controller komponentinin içerisinde bulunan isGrounded fonksiyonunu atama yapýyoruz.

        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }

        // Character Controller komponentinden karakterimizi hareket ettiriyoruz.
        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);
        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);

        if (velocity.sqrMagnitude > 0) //
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }

    }


    // State'ten çýkýþ için kullanýlan fonksiyon.
    public override void Exit()
    {
        base.Exit();

        gravityVelocity.y = 0f; // diðer state'lerin gravity kontrolü olmamasýna karþýn önlem olarak gravity deðerini 0 a çekiyoruz.
        character.playerVelocity = new Vector3(input.x, 0, input.y); // State sonunda hýzý, girilen input'lar neticesinde güncelliyoruz.

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity); // State sonunda karakterin en son baktýðý yöne bakmaya devam etmesini saðlýyoruz.
        }
    }

}