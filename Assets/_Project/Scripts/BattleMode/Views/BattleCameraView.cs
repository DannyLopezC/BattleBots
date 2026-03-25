using UnityEngine;

namespace BattleBots.BattleMode
{
    public interface IBattleCameraView : IMonoBehaviourView
    {
        void SetTarget();
    }

    public class BattleCameraView : MonoBehaviourView, IBattleCameraView
    {
        private IBattleCameraController controller;

        public void SetTarget()
        {
            throw new System.NotImplementedException();
        }

        protected override IMonoBehaviourController Controller()
        {
            return controller;
        }

        protected override void CreateController()
        {
            controller = new BattleCameraController(this);
        }
    }
}