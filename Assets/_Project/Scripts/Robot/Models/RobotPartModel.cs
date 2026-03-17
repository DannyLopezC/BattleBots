using System.Numerics;

namespace BattleBots.Robot
{
    public class RobotPartModel
    {
        public string id;
        public PartDefinitionAsset definition;
        public float currentHp;
        public bool destroyed;
        public bool detached;

        public Quaternion localRotation;

        public RobotPartModel(PartDefinitionAsset def)
        {
            definition = def;
            currentHp = def.maxHp;
            localRotation = Quaternion.Identity;
            destroyed = false;
            detached = false;
        }
    }
}