using UnityEngine;

public class LandingState : State
{
    float timePassed;  // Bu state'te ne kadar kald���m�z� tutan de�i�ken.
    float landingTime; // Bu state'te ne kadar kalaca��m�z� belirlemek i�in kulland���m�z de�i�ken.

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
        landingTime = 0.5f; // Bu s�re inspector pannel'de ayarlanabilir. Ancak yere inip inmedi�ini kontrol etmek i�in 0.5 sn yeterli bir s�re.
    }


    // State i�inde ge�irdi�imiz s�reyle olmas� gereken s�re aras�nda ili�ki kuruyoruz.
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

    // StandingState'ten JumpingState'e ge�iyoruz, JumpingState'ten LandingState'e ge�iyoruz.
    // Daha sonra StandingState'e geri d�n��� sa�l�yoruz. B�ylelikle yerde olup olmad���m�z�n kontrol�n� do�ru s�rayla yapm�� oluyoruz.

}