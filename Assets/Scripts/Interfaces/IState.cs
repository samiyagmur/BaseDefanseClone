namespace Interfaces
{
    public interface IState
    {
        void OnEnter();

        void Tick();

        void OnExit();
    }
}