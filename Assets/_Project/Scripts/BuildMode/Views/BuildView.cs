using BattleBots.Robot;
using UnityEngine;

namespace BattleBots.BuildMode
{

    public interface IBuildView: IMonoBehaviourView
    {
        PartDefinitionAsset SelectedPart { get; set; }
        PartDefinitionAsset WheelDefinition { get; }
        PartDefinitionAsset SpinnerDefinition{ get; }
        Camera MainCamera { get; }
    }

    public class BuildView : MonoBehaviourView, IBuildView
    {
        private IBuildController controller;

        [SerializeField] private Camera mainCamera;
        [SerializeField] private RobotView robotView;
        [SerializeField] private BuildPreviewView buildPreviewView;
        [SerializeField] private LayerMask socketLayerMask;

        [SerializeField] private PartDefinitionAsset wheelDefinition;
        [SerializeField] private PartDefinitionAsset spinnerDefinition;

        private PartDefinitionAsset selectedPart;

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
            BuildSelectionModel selectionModel = new BuildSelectionModel();
            BuildPreviewModel previewModel = new BuildPreviewModel();

            BuildSnapService snapService = new BuildSnapService(mainCamera, socketLayerMask, 100.0f);
            PartPlacementValidator partPlacementValidator = new PartPlacementValidator();

            IBuildPreviewController buildPreviewController = buildPreviewView.GetController;

            if (buildPreviewController == null)
            {
                Debug.LogError("BuildPreviewController is null. BuildPreviewView may not be initialized yet.");
                return;
            }

            controller = new BuildController(this, selectionModel, previewModel, snapService, partPlacementValidator, buildPreviewController, robotView);
        }

        protected override void Start()
        {
            if(mainCamera == null) mainCamera = Camera.main;

            base.Start();
        }
    }
}
