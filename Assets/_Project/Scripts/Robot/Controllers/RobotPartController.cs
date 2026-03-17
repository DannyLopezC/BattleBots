using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBots.Robot
{
    public interface IRobotPartController: IMonoBehaviourController
    {
    }

    public class RobotPartController : MonoBehaviourController, IRobotPartController
    {
        private readonly IRobotPartView view;

        public RobotPartController(IRobotPartView view) : base(view)
        {
            this.view = view;
        }
    }
}