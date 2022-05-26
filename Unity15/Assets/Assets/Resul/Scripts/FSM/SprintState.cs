using UnityEngine;
public class SprintState : State // Karakterimiz ko�arken left shift tu�una basarsa bu state �al��maya ba�layacak.
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

    // Yine ba�lang��ta t�m de�erleri s�f�rl�yoruz.
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

        // Bu k�s�mda biraz farkl� bir ko�ul kulland�k;
        // SprintState'te kalmak i�in bizim left shift'e bas�l� iken ayn� zamanda WASD'ye de basmam�z gerekiyor.
        // Basmad���m�zda input.sqrMagnitude 0 de�erini d�nd�rece�i i�in art�k sprint state'de kalamay�z anlam�na geliyor.
        
        // Ayn� �ekilde sprintAction.triggered'da action map'te pressing or releesing olarak se�ti�imiz i�in
        // shift'i b�rakt���m�zda da ko�ul true d�necek b�ylelikle sprint halinde kalamayaca��z.

        // Her iki ko�ul da false verdi�inde ise sprint halinde kalmaya devam edebilece�iz.
        if (sprintAction.triggered || input.sqrMagnitude == 0f)
        {
            sprint = false;
        }
        else
        {
            sprint = true;
        }
        if (sprint) // Sprint halindeyken maksimum h�z�m�z�n s�n�r� 1 den 1.5 e ��kt��� i�in animat�rdeki speed de�erini artt�r�yoruz.
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

    // Fizik k�sm�nda ise yine StandingState'te oldu�u gibi yer�ekimini, h�z�m�z� ve rotasyonumuzu belirliyoruz.
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
