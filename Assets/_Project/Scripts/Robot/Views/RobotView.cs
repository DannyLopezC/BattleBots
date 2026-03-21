using BattleBots.Bootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleBots.Robot
{
    public interface IRobotView : IMonoBehaviourView
    {
        Rigidbody RB { get; }
        List<IRobotSocketView> SocketViews { get; }
        IRobotSocketView GetSocketViewInterface(string socketId);
        bool PlacePart(PartDefinitionAsset definition, string socketId);
        bool RemovePart(string socketId);
        SocketModel GetSocket(string socketId);
        RobotStatsModel GetRobotStats();
        
        float MoveForceMultiplier { get; }
        float TurnTorqueMultiplier { get; }
    }

    [RequireComponent(typeof(Rigidbody))]
    public class RobotView : MonoBehaviourView, IRobotView
    {
        private IRobotController controller;
        private IRobotLocomotionController locomotionController;

        [SerializeField] private Rigidbody rb;
        public Rigidbody RB => rb;

        private List<IRobotSocketView> socketViews = new();
        public List<IRobotSocketView> SocketViews => socketViews;

        public float MoveForceMultiplier => moveForceMultiplier;

        public float TurnTorqueMultiplier => turnTorqueMultiplier;

        [SerializeField] private float moveForceMultiplier = 10f;
        [SerializeField] private float turnTorqueMultiplier = 5f;

        protected override IMonoBehaviourController Controller()
        {
            return controller;
        }

        protected override void CreateController()
        {
            GarageInstaller installer = FindFirstObjectByType<GarageInstaller>();

            if (installer == null)
            {
                Debug.LogWarning($"Installer not found");
            }

            controller = new RobotController(this, installer.Get<InputAction>());
            locomotionController = new RobotLocomotionController(this, controller.GetModel());
        }

        protected override void Awake()
        {
            if(rb == null) rb = GetComponent<Rigidbody>();

            CacheSocketViews();
            base.Awake();
        }

        public IRobotSocketView GetSocketViewInterface(string socketId)
        {
            return socketViews.Find(s => s.SocketId == socketId);
        }

        private void CacheSocketViews()
        {
            socketViews = GetComponentsInChildren<RobotSocketView>()
                .Cast<IRobotSocketView>()
                .ToList();
        }

        public bool PlacePart(PartDefinitionAsset definition, string socketId)
        {
            return controller.PlacePart(definition, socketId);
        }

        public bool RemovePart(string socketId)
        {
            return controller.RemovePart(socketId);
        }

        public SocketModel GetSocket(string socketId)
        {
            return controller.GetSocket(socketId);
        }

        public RobotStatsModel GetRobotStats()
        {
            return controller.GetStats();
        }
    } 
}