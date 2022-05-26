using UnityEngine;
public class AttackState : State
{
    float timePassed;
    float clipLength;
    float clipSpeed;
    bool attack;
    public AttackState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        attack = false;
        character.animator.applyRootMotion = true; // Animasyonda rootmotion kulland���m�z i�in gerekli komponente ula�t�k.
        timePassed = 0f;
        character.animator.SetTrigger("attack");
        character.animator.SetFloat("speed", 0f);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (attackAction.triggered)
        {
            attack = true;
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        timePassed += Time.deltaTime;
        clipLength = character.animator.GetCurrentAnimatorClipInfo(1)[0].clip.length; // Animasyon s�resinin uzunlu�ununa ula�t�k 
        clipSpeed = character.animator.GetCurrentAnimatorStateInfo(1).speed; //Animasyonun h�z�na ula�t�k.


        // animasyonda kald���m�z s�re ile animasyon s�releri aras�ndaki ili�kiyi gerekli i�lemlerle kontrol ettik.
        if (timePassed >= clipLength / clipSpeed && attack)
        {
            stateMachine.ChangeState(character.attacking);
        }
        if (timePassed >= clipLength / clipSpeed)
        {
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("move");
        }

    }
    public override void Exit()
    {
        base.Exit();
        character.animator.applyRootMotion = false;
    }
}
