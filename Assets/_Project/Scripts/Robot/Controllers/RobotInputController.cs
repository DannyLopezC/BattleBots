using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleBots.Robot
{
    public interface IRobotInputController
    {
        void Poll();
        void Dispose();
        public Vector2 MoveInput { get; }
    }

    public class RobotInputController : IRobotInputController
    {
        private readonly InputAction moveAction;

        public Vector2 MoveInput { get; private set; }

        public RobotInputController(InputAction moveAction)
        {
            this.moveAction = moveAction;
            this.moveAction.Enable();
        }

        public void Poll()
        {
            MoveInput = moveAction.ReadValue<Vector2>();
        }

        public void Dispose()
        {
            moveAction.Disable();
        }
    }
}