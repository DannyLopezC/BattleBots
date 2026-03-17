using UnityEngine;

namespace BattleBots.Robot
{
    [CreateAssetMenu(menuName = "Robot/Part definition")]
    public class PartDefinitionAsset : ScriptableObject
    {
        [SerializeField] public string partName;
        [SerializeField] public PartCategory partType;
        [SerializeField] public float mass;
        [SerializeField] public float maxHp;
        [SerializeField] public GameObject prefab;
        [SerializeField] public Sprite icon;
        [SerializeField] public SocketType socketTypeAllowed;
        [SerializeField] public float drivePower;
        [SerializeField] public float weaponPower;
    }
}
