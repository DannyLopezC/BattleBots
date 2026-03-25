using BattleBots.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleBots.BattleMode
{
    public class BattleInputActions : IRobotInputActions
    {
        public InputAction MoveAction { get; }
        public InputAction WeaponAction { get; }

        public BattleInputActions(
            InputActionReference move,
            InputActionReference weapon)
        {
            MoveAction = move.action;
            WeaponAction = weapon.action;
        }
    }
}
