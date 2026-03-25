using BattleBots.Robot;
using UnityEngine;

namespace BattleBots.BattleMode
{
    public interface IArenaController : IMonoBehaviourController
    {
        void Initialize();
    }

    public class ArenaController : MonoBehaviourController, IArenaController
    {
        private readonly IArenaView view;

        private IRobotView robotView;
        private IBattleCameraView battleCameraView;
        private Transform playerSpawnPoint;

        public ArenaController(IArenaView view,
            Transform playerSpawnPoint,
            IRobotView robotView,
            IBattleCameraView battleCameraView) : base(view)
        {
            this.view = view;

            this.playerSpawnPoint = playerSpawnPoint;
            this.robotView = robotView;
            this.battleCameraView = battleCameraView;
        }

        public void Initialize()
        {
            robotView.Transform.SetPositionAndRotation(
                playerSpawnPoint.position,
                playerSpawnPoint.rotation);

            robotView.RB.linearVelocity = Vector3.zero;
            robotView.RB.angularVelocity = Vector3.zero;

            battleCameraView.SetTarget();
        }
    }
}
