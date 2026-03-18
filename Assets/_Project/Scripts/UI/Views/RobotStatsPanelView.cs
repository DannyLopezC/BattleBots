using BattleBots.Bootstrap;
using BattleBots.BuildMode;
using BattleBots.Robot;
using TMPro;
using UnityEngine;

namespace BattleBots.UI
{
    public interface IRobotStatsPanelView : IMonoBehaviourView
    {
        void SetMass(float value);
        void SetHp(float value);
        void SetDrivePower(float value);
        void SetWeaponPower(float value);
        void SetEnergy(float useValue, float availableValue);
        IRobotStatsUIController GetController { get; }
    }

    [DefaultExecutionOrder(-80)]
    public class RobotStatsPanelView : MonoBehaviourView, IRobotStatsPanelView
    {
        private IRobotStatsUIController controller;
        public IRobotStatsUIController GetController => controller;

        [SerializeField] private TMP_Text massText;
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text drivePowerText;
        [SerializeField] private TMP_Text weaponPowerText;
        [SerializeField] private TMP_Text energyText;

        protected override IMonoBehaviourController Controller()
        {
            return controller;
        }

        protected override void CreateController()
        {
            GarageInstaller installer = FindAnyObjectByType<GarageInstaller>();

            if (installer == null)
            {
                Debug.LogWarning($"Installer not found");
            }

            controller = new RobotStatsUIController(this, installer.Get<IRobotView>());
        }

        public void SetEnergy(float useValue, float availableValue)
        {
            energyText.text = $"Energy: {useValue}/{availableValue}";
        }

        public void SetHp(float value)
        {
            hpText.text = $"HP: {value}";
        }

        public void SetMass(float value)
        {
            massText.text = $"Mass: {value}";

        }

        public void SetDrivePower(float value)
        {
             drivePowerText.text = $"Drive power: {value}";
        }

        public void SetWeaponPower(float value)
        {
            weaponPowerText.text = $"Weapon Power: {value}";
        }
    }
}
