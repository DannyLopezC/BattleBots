using UnityEngine;

namespace BattleBots.Robot
{
    public interface IRobotSocketView : IMonoBehaviourView
    {
        string SocketId { get; }
        SocketType Type { get; }
        Transform AttachPoint { get; }
    }

    public class RobotSocketView : MonoBehaviourView, IRobotSocketView
    {
        private IRobotSocketController controller;

        [SerializeField] private string socketId;
        public string SocketId => socketId;

        [SerializeField] private SocketType socketType;
        public SocketType Type => socketType;

        public Transform AttachPoint => Transform;

        protected override IMonoBehaviourController Controller()
        {
            return controller;
        }

        protected override void CreateController()
        {
            controller = new RobotSocketController(this);
        }
    }
}