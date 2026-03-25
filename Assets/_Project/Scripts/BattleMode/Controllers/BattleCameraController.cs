using UnityEngine;

namespace BattleBots.BattleMode
{
    public interface IBattleCameraController : IMonoBehaviourController
    {

    }

    public class BattleCameraController : MonoBehaviourController, IBattleCameraController
    {
        private readonly IBattleCameraView view;

        public BattleCameraController(IBattleCameraView view) : base(view)
        {
            this.view = view;
        }
    }
}
