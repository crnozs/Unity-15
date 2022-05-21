using UnityEngine;

public class StandingState : State // �lk state'imiz.
{
    float playerSpeed;
    float gravityValue; // Character Controller komponenti kulland���m�z i�in bu de�ere ihtiyac�m�z var.

    bool jump;
    //bool crouch;
    bool grounded;
    bool sprint;
    bool drawWeapon;

    Vector3 currentVelocity;
    Vector3 cVelocity;

    // Yine ayn� �ekilde state'leri objelere atama yap�yoruz.
    public StandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }
    
    public override void Enter()
    {
        base.Enter(); // virtual enter fonksiyonu'nu trigger'l�yoruz.

        // State'e ba�lang�� halinde her�eyi s�f�rl�yoruz.
        jump = false;
        //crouch = false;
        sprint = false;
        drawWeapon = false;
        input = Vector2.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        // Character scriptinden gerekli bilgileri �ekiyoruz.
        velocity = character.playerVelocity;
        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }

    public override void HandleInput()
    {
        base.HandleInput(); // virual HandleInput fonksiyonu'nu trigger'l�yoruz.

        //aksiyon map'te tan�mlad���m�z tu�lar� check ediyoruz.
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

        // Aksiyon map'inde tan�mlad���m�z de�erleri okuyup g�ncelliyoruz.
        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        

        // 
        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f; // Karakter ko�arken kamera yukar� a�a�� sallanmas�n diye y'sini 0'a �ekiyoruz.

    }


    // Animator �zerinden kontrol edilen her�eyin kontrol� buradan yap�l�yor.
    public override void LogicUpdate()
    {
        base.LogicUpdate(); //virtual LogicUpdate fonksiyonu'nu trigger'l�yoruz.

        //Input'lardan ald���m�z de�erleri animator'deki speed parametresine at�yoruz.
        character.animator.SetFloat("speed",input.magnitude, character.speedDampTime, Time.deltaTime);

        // Verilen aksiyona g�re state de�i�tiriyoruz.
        if (sprint) //StandingState'ten SprintState'e ge�i�.
        {
            stateMachine.ChangeState(character.sprinting);
        }
        if (jump) // StandingState'ten JumpingState'e ge�i�.
        {
            stateMachine.ChangeState(character.jumping);
        }
        
        if (drawWeapon) // StandingState'ten CombatState'e ge�i�.
        {
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("drawWeapon");
        }
    }

    // Bu fonksiyon bizim karakterimizi hareket ettirmemizi sa�l�yor
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded; // Character Controller komponentinin i�erisinde bulunan isGrounded fonksiyonunu atama yap�yoruz.

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


    // State'ten ��k�� i�in kullan�lan fonksiyon.
    public override void Exit()
    {
        base.Exit();

        gravityVelocity.y = 0f; // di�er state'lerin gravity kontrol� olmamas�na kar��n �nlem olarak gravity de�erini 0 a �ekiyoruz.
        character.playerVelocity = new Vector3(input.x, 0, input.y); // State sonunda h�z�, girilen input'lar neticesinde g�ncelliyoruz.

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity); // State sonunda karakterin en son bakt��� y�ne bakmaya devam etmesini sa�l�yoruz.
        }
    }

}