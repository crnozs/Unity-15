using UnityEngine;
using UnityEngine.InputSystem;

public class State // Monobihavior deðil State'nin KENDÝSÝ
{
    //Scriptlere referans verdik.
    public Character character;
    public StateMachine stateMachine;
    

    // Tüm state'lerden ulaþabilmek için bu deðerleri "protected" tipinde atadýk.
    // Sadece state function içinde bulunan state'lerden eriþim saðlanacak.
    protected Vector3 gravityVelocity;
    protected Vector3 velocity;
    protected Vector2 input;
    

    // PlayerController componentinde bulunan aksiyonlarý referans verdik.
    // Hareket Aksiyonlarý;
    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction jumpAction;
    public InputAction sprintAction;

    //Combat Aksiyonlarý
    public InputAction drawWeaponAction;
    public InputAction attackAction;


    // Ana Constructor'umuzu burada yaptýk. Ýçerisinde hem StateMachine hemde Character class'larýný (objelerini) tutuyor.
    public State(Character _character, StateMachine _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;

        //Editörde tanýmladýðýmýz aksiyonlarý deðiþkene atadýk.
        moveAction = character.playerInput.actions["Move"];
        lookAction = character.playerInput.actions["Look"];
        jumpAction = character.playerInput.actions["Jump"];
        sprintAction = character.playerInput.actions["Sprint"];
        drawWeaponAction = character.playerInput.actions["DrawWeapon"];
        attackAction = character.playerInput.actions["Attack"];

    }


    // Aþaðýdaki fonksiyonlarýn "virtual" olmasýnýn nedeni herhangi bir state'de override edebilecek olmamýz.
    // Enter         : State'e ilk girdiðimizde 1 kere çalýþýr.
    // HandleInput   : Her frame'de 1 kere çalýþýr ve Action Map'te belirlenen input'larý check eder.
    // LogicUpdate   : State'te kaldýðýmýz süre boyunca yapýlacaklarý kontrol etmek için her bir frame'de 1 kere çalýþýr.
    // PhysicsUpdate : Fizik kontrollerini yapmak için her bir frame'de 1 kere çalýþýr.
    // Exit          : State'ten çýkmadan önce 1 kere çalýþýr ve state sonundaki güncellemeleri belirlemek için kullanýlýr.
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