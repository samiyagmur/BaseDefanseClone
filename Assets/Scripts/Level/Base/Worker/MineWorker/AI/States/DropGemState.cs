using AI.MinerAI;
using Interfaces;

namespace AI.States
{
    public class DropGemState : IState
    {
        private readonly MinerAIBrain _minerAIBrain;
        public bool IsGemDropped => _isGemDropped;
        private bool _isGemDropped;

        public DropGemState(MinerAIBrain minerAIBrain)
        {
            _minerAIBrain = minerAIBrain;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _isGemDropped = true;
        }

        public void OnExit()
        {
            _minerAIBrain.SetTargetForMine();
        }
    }
}