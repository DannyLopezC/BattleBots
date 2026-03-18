using BattleBots.Robot;
using UnityEngine;

namespace BattleBots.UI
{
    public interface IRobotStatsUIController: IMonoBehaviourController
    {
        void Refresh();
    }

    public class RobotStatsUIController : MonoBehaviourController, IRobotStatsUIController
    {
        private readonly IRobotStatsPanelView view;
        private readonly IRobotView robotView;

        public RobotStatsUIController(IRobotStatsPanelView view, IRobotView robotView) : base(view)
        {
            this.view = view;
            this.robotView = robotView;
        }

        public override void OnStart()
        {
            base.OnStart();
            Refresh();
        }

        public void Refresh()
        {
            RobotStatsModel stats = robotView.GetRobotStats();
            if (stats == null)
            {
                Debug.LogError($"Stats are null");
                return;
            }

            view.SetMass(stats.totalMass);
            view.SetHp(stats.totalHP);
            view.SetDrivePower(stats.drivePower);
            view.SetWeaponPower(stats.weaponPower);
            view.SetEnergy(stats.energyUse, stats.energyAvailable);
        }
    }
}