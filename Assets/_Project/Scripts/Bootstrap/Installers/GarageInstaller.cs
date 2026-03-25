using BattleBots.BuildMode;
using BattleBots.Core;
using BattleBots.Robot;
using BattleBots.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleBots.Bootstrap
{
    [DefaultExecutionOrder(-100)]
    public class GarageInstaller : BaseInstaller
    {
        [SerializeField] private GarageSceneReferences sceneReferences;
        [SerializeField] private LayerMask socketLayerMask;
        [SerializeField] private float snapDistance = 100f;

        public GarageSceneReferences SceneReferences => sceneReferences;

        private void Awake()
        {
            RegisterDependencies();
        }

        protected override void RegisterDependencies()
        {
            Register<IBuildController>(() =>
                sceneReferences.BuildView.GetController);
            Register<IBuildView>(() => sceneReferences.BuildView);
            Register<IBuildPreviewView>(() => sceneReferences.BuildPreviewView);
            Register<IPartCatalogView>(() => sceneReferences.PartCatalogView);
            Register<IRobotView>(() => sceneReferences.RobotView);
            Register<Camera>(() => sceneReferences.MainCamera);
            Register<IRobotInputActions>(() => new BuildInputActions(
                sceneReferences.LeftClickAction,
                sceneReferences.RightClickAction,
                sceneReferences.CancelAction,
                sceneReferences.WeaponAction,
                sceneReferences.MoveAction));

            Register<BuildSelectionModel>(() => new BuildSelectionModel());
            Register<BuildPreviewModel>(() => new BuildPreviewModel());

            Register<BuildSnapService>(() =>
                new BuildSnapService(sceneReferences.MainCamera, socketLayerMask, snapDistance));

            Register<PartPlacementValidator>(() => new PartPlacementValidator());

            Register<BuildCatalogService>(() =>
                new BuildCatalogService(sceneReferences.AvailableParts));

            Register<IBuildPreviewController>(() =>
                sceneReferences.BuildPreviewView.GetController);

            Register<IRobotStatsUIController>(() =>
                sceneReferences.RobotStatsPanelView.GetController);

        }
    }
}