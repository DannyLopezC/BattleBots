using BattleBots.BuildMode;
using BattleBots.Robot;
using BattleBots.UI;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using BattleBots.BattleMode;

namespace BattleBots.Bootstrap
{
    public class ArenaSceneReferences : MonoBehaviour
    {
        // scene
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private ArenaView arenaView;
        [SerializeField] private BattleCameraView battleCameraView;

        // robot
        [SerializeField] private RobotView playerRobotView;

        [SerializeField] private BattleHudView hudView;

        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference weaponAction;

        public Transform PlayerSpawnPoint => playerSpawnPoint;
        public ArenaView ArenaView => arenaView;
        public BattleCameraView BattleCameraView => battleCameraView;
        public RobotView PlayerRobotView => playerRobotView;
        public BattleHudView HudView => hudView;
        public InputActionReference MoveAction => moveAction;
        public InputActionReference WeaponAction => weaponAction;
    }
}