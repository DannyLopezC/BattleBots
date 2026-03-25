using System;
using UnityEngine;

namespace BattleBots.Robot
{
    public interface IWeaponView : IMonoBehaviourView
    {
        Transform Spinner { get; }
        void SetActive(bool active);
        void Tick(float dt);
    }

    public class WeaponView : MonoBehaviourView, IWeaponView
    {
        private IWeaponController controller;

        [SerializeField] private Transform spinnerVisual;
        [SerializeField] private WeaponDefinitionAsset weaponDefinitionAsset;

        public event Action<Collision> OnWeaponCollision;

        public Transform Spinner => spinnerVisual != null ? spinnerVisual : transform;

        private void OnCollisionEnter(Collision collision)
        {
            OnWeaponCollision?.Invoke(collision);
        }

        protected override IMonoBehaviourController Controller()
        {
            return controller;
        }

        protected override void CreateController()
        {
            controller = new WeaponController(this, weaponDefinitionAsset);
        }

        public void SetActive(bool active)
        {
            controller.SetActive(active);
        }

        public void Tick(float dt)
        {
            controller.Tick(dt);
        }
    }
}
