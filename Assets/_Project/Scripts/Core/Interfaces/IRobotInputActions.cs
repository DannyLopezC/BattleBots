using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleBots.Core
{
    public interface IRobotInputActions
    {
        InputAction MoveAction { get; }
        InputAction WeaponAction { get; }
    }
}
