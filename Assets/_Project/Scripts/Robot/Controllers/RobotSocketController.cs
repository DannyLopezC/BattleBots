using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBots.Robot
{
    public interface IRobotSocketController: IMonoBehaviourController
    {
    }

    public class RobotSocketController : MonoBehaviourController, IRobotSocketController
    {
        private readonly IRobotSocketView view;

        public RobotSocketController(IRobotSocketView view) : base(view)
        {
            this.view = view;
        }
    }
}