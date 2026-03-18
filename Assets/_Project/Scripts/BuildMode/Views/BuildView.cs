using BattleBots.Bootstrap;
using BattleBots.Robot;
using BattleBots.UI;
using UnityEngine;

namespace BattleBots.BuildMode
{
    public interface IBuildView: IMonoBehaviourView
    {
        PartDefinitionAsset SelectedPart { get; set; }
        PartDefinitionAsset WheelDefinition { get; }
        PartDefinitionAsset SpinnerDefinition{ get; }
        Camera MainCamera { get; }
        IBuildController GetController { get; }
    }

    [DefaultExecutionOrder(-70)]
    public class BuildView : MonoBehaviourView, IBuildView
    {
        private IBuildController controller;
        public IBuildController GetController => controller;

        [SerializeField] private Camera mainCamera;
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

        protected override IMonoBehaviourController Controller()
        {
            return controller;
        }

        protected override void CreateController()
        {
            GarageInstaller installer = FindFirstObjectByType<GarageInstaller>();

            if (installer == null)
            {
                Debug.LogWarning($"Installer not found");
            }

            controller = new BuildController(
                this,
                installer.Get<BuildSelectionModel>(),
                installer.Get<BuildPreviewModel>(),
                installer.Get<BuildSnapService>(),
                installer.Get<PartPlacementValidator>(),
                installer.Get<IBuildPreviewController>(),
                installer.Get<IRobotView>(),
                installer.Get<IRobotStatsUIController>()
                );
        }

        protected override void Start()
        {
            if(mainCamera == null) mainCamera = Camera.main;

            base.Start();
        }
    }
}
