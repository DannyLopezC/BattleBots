using BattleBots.Core;
using BattleBots.Robot;
using UnityEngine;

namespace BattleBots.BuildMode
{
    public interface IBuildController: IMonoBehaviourController
    {
        void SelectPart(PartDefinitionAsset definition);
        void ClearSelection();
    }

    public class BuildController : MonoBehaviourController, IBuildController
    {
        private readonly IBuildView view;

        private readonly BuildSelectionModel selectionModel;
        private readonly BuildPreviewModel previewModel;
        private readonly BuildSnapService snapService;
        private readonly PartPlacementValidator partPlacementValidator;
        private readonly IBuildPreviewController previewController;
        private readonly IRobotView robotView;
        

        public BuildController(IBuildView view,
            BuildSelectionModel selectionModel,
            BuildPreviewModel previewModel,
            BuildSnapService snapService,
            PartPlacementValidator partPlacementValidator,
            IBuildPreviewController buildPreviewController,
            IRobotView robotView) : base(view)
        {
            this.view = view;

            this.selectionModel = selectionModel;
            this.previewModel = previewModel;
            this.snapService = snapService;
            this.partPlacementValidator = partPlacementValidator;
            this.previewController = buildPreviewController;
            this.robotView = robotView;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            UpdatePreview();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectPart(view.WheelDefinition);
                Debug.Log("Wheel selected");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectPart(view.SpinnerDefinition);
                Debug.Log("Spinner selected");
            }

            if (Input.GetMouseButtonDown(0))
            {
                TryPlacePart();
            }

            if (Input.GetMouseButtonDown(1))
            {
                TryRemovePart();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClearSelection();
            }

            HandleMovement();
        }

        public void SelectPart(PartDefinitionAsset definition)
        {
            selectionModel.SetSelectedPart(definition);
        }

        private void UpdatePreview()
        {
            if (!selectionModel.HasSelection)
            {
                previewModel.Clear();
                previewController.Hide();
                return;
            }

            if (!snapService.TryGetSocketUnderCursor(out IRobotSocketView socketView))
            {
                previewModel.Clear();
                previewController.Hide();
                return;
            }
            
            SocketModel socketModel = robotView.GetSocket(socketView.SocketId);
            if(socketModel == null)
            {
                previewModel.Clear();
                previewController.Hide();
                return;
            }

            PlacementValidationResult result = partPlacementValidator.Validate(selectionModel.SelectedPart, socketModel);

            previewModel.SetPreview(selectionModel.SelectedPart, socketView, result.IsValid);
            previewController.Show(previewModel);
        }

        private void TryPlacePart()
        {
            if (!previewModel.HasValidPreview)
                return;

            if(previewModel.SelectedPart == null || previewModel.TargetSocketView == null)
                return;

            bool placed = robotView.PlacePart(previewModel.SelectedPart, previewModel.TargetSocketView.SocketId);

            if (!placed)
                return;

            previewModel.Clear();
            previewController.Hide();
        }

        private void TryRemovePart()
        {
            if (!snapService.TryGetSocketUnderCursor(out IRobotSocketView socket))
                return;

            bool removed = robotView.RemovePart(socket.SocketId);

            if (!removed)
                return;

            previewModel.Clear();
            previewController.Hide();
        }

        public void ClearSelection()
        {
            selectionModel.Clear();
            previewModel.Clear();
            previewController.Hide();
        }

        private void HandleMovement()
        {
            float moveInput = 0f;
            float turnInput = 0f;

            if (Input.GetKey(KeyCode.W)) moveInput = 1f;
            if (Input.GetKey(KeyCode.S)) moveInput = -1f;
            if (Input.GetKey(KeyCode.A)) turnInput = -1f;
            if (Input.GetKey(KeyCode.D)) turnInput = 1f;

            robotView.Move(moveInput, turnInput);
        }
    }
}
