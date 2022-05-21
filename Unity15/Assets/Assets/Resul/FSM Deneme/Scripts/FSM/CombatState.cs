using UnityEngine;
public class CombatState : State
{
    // Standing State ile oldukça benzer þekilde gerekli ayarlamalarý yapýyoruz.
    // Sadece savaþ mekaniði ile ilgili ekstra eklemelerimizi ayarlýyoruz.

    float gravityValue;
    Vector3 currentVelocity;
    bool grounded;
    bool sheathWeapon;//***
    float playerSpeed;
    bool attack; //***

    Vector3 cVelocity;

    public CombatState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    
    public override void Enter()
    {
        base.Enter();

        sheathWeapon = false;//*** 1
        input = Vector2.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;
        attack = false;//*** 1

        velocity = character.playerVelocity;
        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        // Aksiyon map'te atak aksiyonlarý ile ilgili trigger'lar kontrol ediliyor.
        if (drawWeaponAction.triggered)//*** 2
        {
            sheathWeapon = true;
        }

        if (attackAction.triggered) //*** 2
        {
            attack = true;
        }

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);
         //Aksiyonlara göre animatordeki triggerlar aktifleþtiriliyor.
        if (sheathWeapon) // *** 3
        {
            character.animator.SetTrigger("sheathWeapon");
            stateMachine.ChangeState(character.standing);
        }

        if (attack) // *** 3
        {
            character.animator.SetTrigger("attack");
            stateMachine.ChangeState(character.attacking);
        }
    }

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

    public override void Exit()
    {
        base.Exit();

        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y);

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity);
        }

    }

}
