using BattleBots.Robot;
using UnityEngine;

namespace BattleBots.BuildMode
{
    public class BuildSnapService
    {
        private readonly Camera buildCamera;
        private readonly LayerMask socketLayerMask;
        private readonly float maxDistance;

        public BuildSnapService(Camera buildCamera, LayerMask socketLayerMask, float maxDistance)
        {
            this.buildCamera = buildCamera;
            this.socketLayerMask = socketLayerMask;
            this.maxDistance = maxDistance;
        }

        public bool TryGetSocketUnderCursor(out IRobotSocketView socketView)
        {
            socketView = null;

            Ray ray = buildCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, maxDistance, socketLayerMask))
            {
                return false;
            }

            socketView = hit.collider.GetComponentInParent<IRobotSocketView>();
            return socketView != null;
        }
    }
}
