using BattleBots.BattleMode;
using BattleBots.BuildMode;
using BattleBots.Robot;
using BattleBots.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleBots.Bootstrap
{
    [DefaultExecutionOrder(-100)]
    public class ArenaInstaller : BaseInstaller
    {
        [SerializeField] private ArenaSceneReferences sceneReferences;

        public ArenaSceneReferences SceneReferences => sceneReferences;

        private void Awake()
        {
            RegisterDependencies();
        }

        protected override void RegisterDependencies()
        {
            Register<IRobotView>(() => sceneReferences.PlayerRobotView);
            Register<IArenaView>(() => sceneReferences.ArenaView);
            Register<IBattleCameraView>(() => sceneReferences.BattleCameraView);
            Register<BattleHudView>(() => sceneReferences.HudView);
            Register<Transform>(() => sceneReferences.PlayerSpawnPoint);
        }
    }
}