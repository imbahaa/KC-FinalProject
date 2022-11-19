public abstract class BaseState
{
    public StateMachine statemachine;
    public Enemy enemy;
    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();    
}