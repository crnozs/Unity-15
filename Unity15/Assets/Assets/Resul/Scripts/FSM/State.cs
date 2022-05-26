using UnityEngine;
using UnityEngine.InputSystem;

public class State // Monobihavior de�il State'nin KEND�S�
{
    //Scriptlere referans verdik.
    public Character character;
    public StateMachine stateMachine;
    

    // T�m state'lerden ula�abilmek i�in bu de�erleri "protected" tipinde atad�k.
    // Sadece state function i�inde bulunan state'lerden eri�im sa�lanacak.
    protected Vector3 gravityVelocity;
    protected Vector3 velocity;
    protected Vector2 input;
    

    // PlayerController componentinde bulunan aksiyonlar� referans verdik.
    // Hareket Aksiyonlar�;
    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction jumpAction;
    public InputAction sprintAction;

    //Combat Aksiyonlar�
    public InputAction drawWeaponAction;
    public InputAction attackAction;


    // Ana Constructor'umuzu burada yapt�k. ��erisinde hem StateMachine hemde Character class'lar�n� (objelerini) tutuyor.
    public State(Character _character, StateMachine _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;

        //Edit�rde tan�mlad���m�z aksiyonlar� de�i�kene atad�k.
        moveAction = character.playerInput.actions["Move"];
        lookAction = character.playerInput.actions["Look"];
        jumpAction = character.playerInput.actions["Jump"];
        sprintAction = character.playerInput.actions["Sprint"];
        drawWeaponAction = character.playerInput.actions["DrawWeapon"];
        attackAction = character.playerInput.actions["Attack"];

    }


    // A�a��daki fonksiyonlar�n "virtual" olmas�n�n nedeni herhangi bir state'de override edebilecek olmam�z.
    // Enter         : State'e ilk girdi�imizde 1 kere �al���r.
    // HandleInput   : Her frame'de 1 kere �al���r ve Action Map'te belirlenen input'lar� check eder.
    // LogicUpdate   : State'te kald���m�z s�re boyunca yap�lacaklar� kontrol etmek i�in her bir frame'de 1 kere �al���r.
    // PhysicsUpdate : Fizik kontrollerini yapmak i�in her bir frame'de 1 kere �al���r.
    // Exit          : State'ten ��kmadan �nce 1 kere �al���r ve state sonundaki g�ncellemeleri belirlemek i�in kullan�l�r.
    public virtual void Enter()
    {
        //StateUI.instance.SetStateText(this.ToString());
        Debug.Log("Enter State: " + this.ToString());
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}