using UnityEngine;

public class LandingState : State
{
    float timePassed;  // Bu state'te ne kadar kaldýðýmýzý tutan deðiþken.
    float landingTime; // Bu state'te ne kadar kalacaðýmýzý belirlemek için kullandýðýmýz deðiþken.

    public LandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        timePassed = 0f;
        character.animator.SetTrigger("land");
        landingTime = 0.5f; // Bu süre inspector pannel'de ayarlanabilir. Ancak yere inip inmediðini kontrol etmek için 0.5 sn yeterli bir süre.
    }


    // State içinde geçirdiðimiz süreyle olmasý gereken süre arasýnda iliþki kuruyoruz.
    public override void LogicUpdate()
    {

        base.LogicUpdate();
        if (timePassed > landingTime)
        {
            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.standing);
        }
        timePassed += Time.deltaTime;
    }

    // StandingState'ten JumpingState'e geçiyoruz, JumpingState'ten LandingState'e geçiyoruz.
    // Daha sonra StandingState'e geri dönüþü saðlýyoruz. Böylelikle yerde olup olmadýðýmýzýn kontrolünü doðru sýrayla yapmýþ oluyoruz.

}