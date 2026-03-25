using UnityEngine;

namespace BattleBots.Robot
{
    [CreateAssetMenu(menuName = "Robot/Weapon Definition")]
    public class WeaponDefinitionAsset : ScriptableObject
    {
        public float maxRpm = 1000f;
        public float spinUpAcceleration = 2000f;
        public float spinDownAcceleration = 3000f;

        public float baseDamage = 10f;
        public float hitCooldown = 0.1f;
    }
}
