using System.Numerics;

namespace BattleBots.Robot
{
    public class SocketModel
    {
        public string id;
        public SocketType typeAllowed;
        public RobotPartModel currentPart { get; private set; }
        public RobotPartView currentPartView { get; private set; }
        public bool isOccupied => currentPart != null;
        public Quaternion orientation;

        public SocketModel(string id, SocketType type)
        {
            this.id = id;
            typeAllowed = type;
        }

        public void SetPart(RobotPartModel part, RobotPartView partView)
        {
            currentPart = part;
            currentPartView = partView;
        }

        public void Clear()
        {
            currentPart = null;
            currentPartView = null;
        }
    }
}