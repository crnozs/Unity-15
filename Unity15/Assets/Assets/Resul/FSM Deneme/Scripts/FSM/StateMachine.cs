public class StateMachine
{
    public State currentState;

    public void Initialize(State startingState) // başlangıç state'ini init ettik.
    {
        currentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState) // yeni bir state'ye geçmek için eski state'i kapatıp yeni state'i başlattık.
    {
        currentState.Exit();

        currentState = newState;
        newState.Enter();
    }


}