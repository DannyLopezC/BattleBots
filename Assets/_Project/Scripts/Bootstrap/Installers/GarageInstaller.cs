using BattleBots.BuildMode;
using BattleBots.Robot;
using BattleBots.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBots.Bootstrap
{
    [DefaultExecutionOrder(-100)]
    public class GarageInstaller : MonoBehaviour
    {
        [SerializeField] private GarageSceneReferences sceneReferences;
        [SerializeField] private LayerMask socketLayerMask;
        [SerializeField] private float snapDistance = 100f;

        private readonly Dictionary<Type, Func<object>> factories = new();
        private readonly Dictionary<Type, object> services = new();
        private readonly HashSet<Type> currentlyResolving = new();

        public GarageSceneReferences SceneReferences => sceneReferences;

        private void Awake()
        {
            RegisterDependencies();
        }

        public void Register<T>(Func<T> factory) where T : class
        {
            factories[typeof(T)] = () => factory();
            services.Remove(typeof(T));
        }

        public T Get<T>() where T : class
        {
            Type type = typeof(T);

            if (currentlyResolving.Contains(type))
            {
                Debug.LogError($"GarageInstaller circular dependency: {type}");
                return null;
            }

            if (!services.ContainsKey(type))
            {
                if (!factories.ContainsKey(type))
                {
                    Debug.LogError($"GarageInstaller missing factory for type: {type}");
                    return null;
                }

                currentlyResolving.Add(type);
                services[type] = factories[type]();
                currentlyResolving.Remove(type);
            }

            return services[type] as T;
        }

        private void RegisterDependencies()
        {
            Register<IBuildController>(() =>
                sceneReferences.BuildView.GetController);
            Register<IBuildView>(() => sceneReferences.BuildView);
            Register<IBuildPreviewView>(() => sceneReferences.BuildPreviewView);
            Register<IPartCatalogView>(() => sceneReferences.PartCatalogView);
            Register<IRobotView>(() => sceneReferences.RobotView);
            Register<Camera>(() => sceneReferences.MainCamera);

            Register<BuildSelectionModel>(() => new BuildSelectionModel());
            Register<BuildPreviewModel>(() => new BuildPreviewModel());

            Register<BuildSnapService>(() =>
                new BuildSnapService(sceneReferences.MainCamera, socketLayerMask, snapDistance));

            Register<PartPlacementValidator>(() => new PartPlacementValidator());

            Register<BuildCatalogService>(() =>
                new BuildCatalogService(sceneReferences.AvailableParts));

            Register<IBuildPreviewController>(() =>
                sceneReferences.BuildPreviewView.GetController);

        }
    }
}