public class StateMachine
{
    public State currentState;

    public void Initialize(State startingState) // ba�lang�� state'ini init ettik.
    {
        currentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState) // yeni bir state'ye ge�mek i�in eski state'i kapat�p yeni state'i ba�latt�k.
    {
        currentState.Exit();

        currentState = newState;
        newState.Enter();
    }


}