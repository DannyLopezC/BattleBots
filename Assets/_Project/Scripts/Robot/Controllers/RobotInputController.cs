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
        Vector2 MoveInput { get; }
        bool WeaponPressed { get; }
    }

    public class RobotInputController : IRobotInputController
    {
        private readonly InputAction moveAction;
        private readonly InputAction weaponPressedAction;

        public Vector2 MoveInput { get; private set; }
        public bool WeaponPressed { get; private set; }

        public RobotInputController(InputAction moveAction, InputAction weaponAction)
        {
            this.moveAction = moveAction;
            this.moveAction.Enable();

            this.weaponPressedAction = weaponAction;
            this.weaponPressedAction.Enable();
        }

        public void Poll()
        {
            MoveInput = moveAction.ReadValue<Vector2>();
            WeaponPressed = weaponPressedAction.IsPressed();
        }

        public void Dispose()
        {
            moveAction.Disable();
            weaponPressedAction.Disable();
        }
    }
}