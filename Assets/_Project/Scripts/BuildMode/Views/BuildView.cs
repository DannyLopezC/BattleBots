using BattleBots.Robot;
using UnityEngine;

namespace BattleBots.BuildMode
{

    public interface IBuildView: IMonoBehaviourView
    {
        PartDefinitionAsset SelectedPart { get; set; }
        PartDefinitionAsset WheelDefinition { get; }
        PartDefinitionAsset SpinnerDefinition{ get; }
        IRobotView RobotView { get; }
        Camera MainCamera { get; }
    }

    public class BuildView : MonoBehaviourView, IBuildView
    {
        private IBuildController controller;

        [SerializeField] private Camera mainCamera;
        [SerializeField] private RobotView robotView;

        [SerializeField] private PartDefinitionAsset wheelDefinition;
        [SerializeField] private PartDefinitionAsset spinnerDefinition;

        private PartDefinitionAsset selectedPart;
        private IRobotController robotController;

        public PartDefinitionAsset SelectedPart { 
            get { return selectedPart; }
            set { selectedPart = value; }
        }

        public PartDefinitionAsset WheelDefinition => wheelDefinition;

        public PartDefinitionAsset SpinnerDefinition => spinnerDefinition;

        public Camera MainCamera => mainCamera;

        public IRobotView RobotView => robotView;

        protected override IMonoBehaviourController Controller()
        {
            return controller;
        }

        protected override void CreateController()
        {
            controller = new BuildController(this);
        }

        protected override void Start()
        {
            if(mainCamera == null) mainCamera = Camera.main;

            base.Start();
        }
    }
}
