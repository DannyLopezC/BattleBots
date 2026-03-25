using UnityEngine;

namespace BattleBots.Robot
{
    public interface IWeaponController : IMonoBehaviourController
    {
        void SetActive(bool active);
        void Tick(float dt);
    }

    public class WeaponController : MonoBehaviourController, IWeaponController
    {
        private readonly WeaponDefinitionAsset def;
        private readonly WeaponStateModel state;
        private readonly IWeaponView view;

        public WeaponController(IWeaponView view, WeaponDefinitionAsset def) : base(view)
        {
            this.def = def;
            this.view = view;
            this.state = new WeaponStateModel();
        }

        public void SetActive(bool active)
        {
            state.isActive = active;
        }

        public void Tick(float dt)
        {
            float target = state.isActive ? def.maxRpm : 0f;
            float accel = state.isActive ? def.spinUpAcceleration : def.spinDownAcceleration;

            state.currentRpm = Mathf.MoveTowards(state.currentRpm, target, accel * dt);

            float degreesPerSec = state.currentRpm * 360f / 60f;
            view.Spinner.Rotate(Vector3.forward, degreesPerSec * dt, Space.Self);
        }
    }
}
