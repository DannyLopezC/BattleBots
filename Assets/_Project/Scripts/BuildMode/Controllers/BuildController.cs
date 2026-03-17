using BattleBots.Robot;
using UnityEngine;

namespace BattleBots.BuildMode
{
    public interface IBuildController: IMonoBehaviourController
    {

    }

    public class BuildController : MonoBehaviourController, IBuildController
    {
        private readonly IBuildView view;
        public BuildController(IBuildView view) : base(view)
        {
            this.view = view;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            HandleSelection();
            HandlePlacement();
            HandleRemoval();
            HandleMovement();
        }

        private void HandleSelection()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                view.SelectedPart = view.WheelDefinition;
                Debug.Log($"Selected: Wheel");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                view.SelectedPart = view.SpinnerDefinition;
                Debug.Log($"Selected: Spinner");
            }
        }

        private void HandlePlacement()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (view.SelectedPart == null) return;
            if (!TryGetClickedSocket(out IRobotSocketView socketView)) return;

            bool success = view.RobotView.PlacePart(view.SelectedPart, socketView.SocketId);
            Debug.Log(success
                ? $"Placed {view.SelectedPart.partName} on {socketView.SocketId}"
                : $"Failed to place {view.SelectedPart.partName} on {socketView.SocketId}");
        }

        private void HandleRemoval()
        {
            if (!Input.GetMouseButtonDown(1)) return;
            if (!TryGetClickedSocket(out IRobotSocketView socketView)) return;

            bool success = view.RobotView.RemovePart(socketView.SocketId);

            Debug.Log(success
                ? $"Removed part from {socketView.SocketId}"
                : $"Failed to remove part from {socketView.SocketId}");
        }

        private bool TryGetClickedSocket(out IRobotSocketView socketView)
        {
            socketView = null;

            Ray ray = view.MainCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Collide))
            {
                return false;
            }

            RobotSocketView socket = hit.collider.GetComponentInParent<RobotSocketView>();
            if (socket == null)
            {
                return false;
            }

            socketView = socket;
            return true;
        }

        private void HandleMovement()
        {
            float moveInput = 0f;
            float turnInput = 0f;

            if (Input.GetKey(KeyCode.W)) moveInput = 1f;
            if (Input.GetKey(KeyCode.S)) moveInput = -1f;
            if (Input.GetKey(KeyCode.A)) turnInput = -1f;
            if (Input.GetKey(KeyCode.D)) turnInput = 1f;

            view.RobotView.Move(moveInput, turnInput);
        }
    }
}
