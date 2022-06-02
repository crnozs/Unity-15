using UnityEngine;

public class JumpingState : State
{
    // �htiyac�m�z olan de�i�kenler.
    bool grounded;

    float gravityValue;
    float jumpHeight;
    float playerSpeed;

    Vector3 airVelocity;


    // Constructor'�m�z.
    public JumpingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    // State giri�inde; Character scriptinden t�m de�erleri �ekiyoruz.
    public override void Enter()
    {
        base.Enter();

        grounded = false;
        gravityValue = character.gravityValue;
        jumpHeight = character.jumpHeight;
        playerSpeed = character.playerSpeed;
        gravityVelocity.y = 0;

        // Animator'de bulunan idle ve run i�in jump trigger'�n� tetikleyip Jump fonksiyonunu ba�lat�yoruz.
        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("jump");
        Jump();
        // z�plarken "�h"'lama sesi gelebilir.
    }

    // State giri�inin hemen ard�ndan Action Map �zerinden input de�erlerini al�yoruz.
    public override void HandleInput()
    {
        base.HandleInput();

        input = moveAction.ReadValue<Vector2>();
    }

    // State her tur tamamland���nda z�plaman�n ard�ndan tekrar yere oturabilmesi i�in gereken jump animasyonunun
    // hemen ��k���nda bulunan land animasyonuna ge�mesi i�in state'i her tur i�in 1 kere de�i�tiriyoruz.
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (grounded)
        {
            stateMachine.ChangeState(character.landing);
        }
    }


    // Havada iken ger�ekle�mesi gereken fizik d�zenlemelerini yap�yoruz.
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!grounded)
        {

            velocity = character.playerVelocity;
            airVelocity = new Vector3(input.x, 0, input.y);

            velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
            velocity.y = 0f;

            // Havada iken bir miktar da olsa hareket edebilmek i�in gerekli fizik i�lemlerini yap�yoruz. (airVelocity !=0)
            airVelocity = airVelocity.x * character.cameraTransform.right.normalized + airVelocity.z * character.cameraTransform.forward.normalized;
            airVelocity.y = 0f; // Havada hareket edebiliyorken TEKRAR z�plamamak i�in .y'sini 0 a e�itliyoruz.
            character.controller.Move(gravityVelocity * Time.deltaTime + (airVelocity * character.airControl + velocity * (1 - character.airControl)) * playerSpeed * Time.deltaTime);
        }

        gravityVelocity.y += gravityValue * Time.deltaTime; // smooth'lu�u sa�l�yoruz.
        grounded = character.controller.isGrounded; // grounded bool'umuzu ger�ekten yerdemiyiz diye kontrol edip de�erini g�ncelliyoruz.
    }

    void Jump()
    {
        gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.5f * gravityValue); // z�playabilmemiz i�in gerekli olan de�eri hesapl�yoruz.
    }

}