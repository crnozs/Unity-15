using UnityEngine;
public class SprintJumpState : State // Bu state'te farklý olarak RootMotion kullandýk.
{
    float timePassed;
    float jumpTime;

    public SprintJumpState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        // Eðer istersek bu state'i, RootMotion kullanan bir animasyon için de kullanabilirz.
        character.animator.applyRootMotion = true;
        timePassed = 0f;
        character.animator.SetTrigger("sprintJump");

        jumpTime = 1f; // Zýplama zamanýný 1 sn olarak ayarladýk. 
    }

    public override void Exit()
    {
        base.Exit();
        character.animator.applyRootMotion = false; // State'ten çýkarken yine eski haline getiriyoruz.
    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();
        // Bu state'te zýplama süresinden fazla kaldýðýmýz anda state deðiþtirmemiz gerektiðini tanýmlýyoruz.
        if (timePassed > jumpTime)
        {
            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.sprinting);
        }
        timePassed += Time.deltaTime;
    }



}
