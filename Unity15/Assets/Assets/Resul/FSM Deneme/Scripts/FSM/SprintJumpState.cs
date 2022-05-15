using UnityEngine;
public class SprintJumpState : State // Bu state'te farkl� olarak RootMotion kulland�k.
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
        // E�er istersek bu state'i, RootMotion kullanan bir animasyon i�in de kullanabilirz.
        character.animator.applyRootMotion = true;
        timePassed = 0f;
        character.animator.SetTrigger("sprintJump");

        jumpTime = 1f; // Z�plama zaman�n� 1 sn olarak ayarlad�k. 
    }

    public override void Exit()
    {
        base.Exit();
        character.animator.applyRootMotion = false; // State'ten ��karken yine eski haline getiriyoruz.
    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();
        // Bu state'te z�plama s�resinden fazla kald���m�z anda state de�i�tirmemiz gerekti�ini tan�ml�yoruz.
        if (timePassed > jumpTime)
        {
            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.sprinting);
        }
        timePassed += Time.deltaTime;
    }



}
