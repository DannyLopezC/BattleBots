using BattleBots.BuildMode;
using BattleBots.Robot;
using BattleBots.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleBots.Bootstrap
{
    public abstract class BaseInstaller : MonoBehaviour
    {
        private readonly Dictionary<Type, Func<object>> factories = new();
        private readonly Dictionary<Type, object> services = new();
        private readonly HashSet<Type> currentlyResolving = new();

        protected abstract void RegisterDependencies();

        private void Awake()
        {
            RegisterDependencies();
        }

        protected void Register<T>(Func<T> factory) where T : class
        {
            factories[typeof(T)] = () => factory();
            services.Remove(typeof(T));
        }

        public T Get<T>() where T : class
        {
            Type type = typeof(T);

            if (currentlyResolving.Contains(type))
            {
                Debug.LogError($"{GetType().Name} circular dependency: {type}");
                return null;
            }

            if (!services.ContainsKey(type))
            {
                if (!factories.ContainsKey(type))
                {
                    Debug.LogError($"{GetType().Name} missing factory for type: {type}");
                    return null;
                }

                currentlyResolving.Add(type);
                services[type] = factories[type]();
                currentlyResolving.Remove(type);
            }

            return services[type] as T;
        }
    }
}