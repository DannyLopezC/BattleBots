using BattleBots.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleBots.BuildMode
{
    public class BuildInputActions : IRobotInputActions
    {
        public InputAction LeftClickAction { get; }
        public InputAction RightClickAction { get; }
        public InputAction CancelAction { get; }

        public InputAction MoveAction { get; }
        public InputAction WeaponAction { get; }

        public BuildInputActions(
            InputActionReference leftClick,
            InputActionReference rightClick,
            InputActionReference cancel,
            InputActionReference weapon,
            InputActionReference move)
        {
            LeftClickAction = leftClick.action;
            RightClickAction = rightClick.action;
            CancelAction = cancel.action;
            WeaponAction = weapon.action;
            MoveAction = move.action;
        }
    }
}
