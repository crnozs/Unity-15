using UnityEngine;

public class JumpingState : State
{
    // Ýhtiyacýmýz olan deðiþkenler.
    bool grounded;

    float gravityValue;
    float jumpHeight;
    float playerSpeed;

    Vector3 airVelocity;


    // Constructor'ýmýz.
    public JumpingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    // State giriþinde; Character scriptinden tüm deðerleri çekiyoruz.
    public override void Enter()
    {
        base.Enter();

        grounded = false;
        gravityValue = character.gravityValue;
        jumpHeight = character.jumpHeight;
        playerSpeed = character.playerSpeed;
        gravityVelocity.y = 0;

        // Animator'de bulunan idle ve run için jump trigger'ýný tetikleyip Jump fonksiyonunu baþlatýyoruz.
        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("jump");
        Jump();
        // zýplarken "ýh"'lama sesi gelebilir.
    }

    // State giriþinin hemen ardýndan Action Map üzerinden input deðerlerini alýyoruz.
    public override void HandleInput()
    {
        base.HandleInput();

        input = moveAction.ReadValue<Vector2>();
    }

    // State her tur tamamlandýðýnda zýplamanýn ardýndan tekrar yere oturabilmesi için gereken jump animasyonunun
    // hemen çýkýþýnda bulunan land animasyonuna geçmesi için state'i her tur için 1 kere deðiþtiriyoruz.
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (grounded)
        {
            stateMachine.ChangeState(character.landing);
        }
    }


    // Havada iken gerçekleþmesi gereken fizik düzenlemelerini yapýyoruz.
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!grounded)
        {

            velocity = character.playerVelocity;
            airVelocity = new Vector3(input.x, 0, input.y);

            velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
            velocity.y = 0f;

            // Havada iken bir miktar da olsa hareket edebilmek için gerekli fizik iþlemlerini yapýyoruz. (airVelocity !=0)
            airVelocity = airVelocity.x * character.cameraTransform.right.normalized + airVelocity.z * character.cameraTransform.forward.normalized;
            airVelocity.y = 0f; // Havada hareket edebiliyorken TEKRAR zýplamamak için .y'sini 0 a eþitliyoruz.
            character.controller.Move(gravityVelocity * Time.deltaTime + (airVelocity * character.airControl + velocity * (1 - character.airControl)) * playerSpeed * Time.deltaTime);
        }

        gravityVelocity.y += gravityValue * Time.deltaTime; // smooth'luðu saðlýyoruz.
        grounded = character.controller.isGrounded; // grounded bool'umuzu gerçekten yerdemiyiz diye kontrol edip deðerini güncelliyoruz.
    }

    void Jump()
    {
        gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.5f * gravityValue); // zýplayabilmemiz için gerekli olan deðeri hesaplýyoruz.
    }

}