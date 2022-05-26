using UnityEngine;
public class SprintState : State // Karakterimiz koþarken left shift tuþuna basarsa bu state çalýþmaya baþlayacak.
{
    float gravityValue;
    Vector3 currentVelocity;

    bool grounded;
    bool sprint;
    float playerSpeed;
    //bool sprintJump;
    Vector3 cVelocity;
    public SprintState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    // Yine baþlangýçta tüm deðerleri sýfýrlýyoruz.
    public override void Enter()
    {
        base.Enter();
        
        sprint = false;
        //sprintJump = false;
        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;
        

        playerSpeed = character.sprintSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }
    public override void Exit()
    {
        base.Exit();
        sprint = false;
        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;


        playerSpeed = character.sprintSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }

    public override void HandleInput()
    {
        base.Enter();
        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;

        // Bu kýsýmda biraz farklý bir koþul kullandýk;
        // SprintState'te kalmak için bizim left shift'e basýlý iken ayný zamanda WASD'ye de basmamýz gerekiyor.
        // Basmadýðýmýzda input.sqrMagnitude 0 deðerini döndüreceði için artýk sprint state'de kalamayýz anlamýna geliyor.
        
        // Ayný þekilde sprintAction.triggered'da action map'te pressing or releesing olarak seçtiðimiz için
        // shift'i býraktýðýmýzda da koþul true dönecek böylelikle sprint halinde kalamayacaðýz.

        // Her iki koþul da false verdiðinde ise sprint halinde kalmaya devam edebileceðiz.
        if (sprintAction.triggered || input.sqrMagnitude == 0f)
        {
            sprint = false;
        }
        else
        {
            sprint = true;
        }
        if (sprint) // Sprint halindeyken maksimum hýzýmýzýn sýnýrý 1 den 1.5 e çýktýðý için animatördeki speed deðerini arttýrýyoruz.
        {
            character.animator.SetFloat("speed", input.magnitude + 0.5f, character.speedDampTime, Time.deltaTime);
        }
        
        else
        {
            stateMachine.ChangeState(character.standing);
        }

    }
    
    public override void LogicUpdate()
    {
        
    }

    // Fizik kýsmýnda ise yine StandingState'te olduðu gibi yerçekimini, hýzýmýzý ve rotasyonumuzu belirliyoruz.
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }
        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);

        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);


        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }
}
