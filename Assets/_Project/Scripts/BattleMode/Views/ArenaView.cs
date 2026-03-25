using BattleBots.Bootstrap;
using BattleBots.Robot;
using UnityEngine;

namespace BattleBots.BattleMode
{
    public interface IArenaView : IMonoBehaviourView
    {

    }

    public class ArenaView : MonoBehaviourView, IArenaView
    {
        private IArenaController controller;

        protected override IMonoBehaviourController Controller()
        {
            return controller;
        }

        protected override void CreateController()
        {
            BaseInstaller installer = FindFirstObjectByType<BaseInstaller>();

            if (installer == null)
            {
                Debug.LogWarning($"Installer not found");
            }

            controller = new ArenaController(this,
                installer.Get<Transform>(),
                installer.Get<IRobotView>(),
                installer.Get<IBattleCameraView>());
        }
    }
}
