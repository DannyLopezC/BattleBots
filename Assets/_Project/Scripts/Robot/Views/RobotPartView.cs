using UnityEngine;

namespace BattleBots.Robot
{
    public interface IRobotPartView: IMonoBehaviourView
    {
        PartDefinitionAsset Definition { get; }
        void Initialize(PartDefinitionAsset definition);
    }

    public class RobotPartView : MonoBehaviourView, IRobotPartView
    {
        private IRobotPartController controller;

        private PartDefinitionAsset definition;
        public PartDefinitionAsset Definition => definition;

        protected override IMonoBehaviourController Controller()
        {
            return controller;
        }

        protected override void CreateController()
        {
            controller = new RobotPartController(this);
        }

        public void Initialize(PartDefinitionAsset definition)
        {
            this.definition = definition;
        }
    }
}