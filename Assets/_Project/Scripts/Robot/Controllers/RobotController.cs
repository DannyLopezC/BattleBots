using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBots.Robot
{
    public interface IRobotController: IMonoBehaviourController
    {
        bool PlacePart(PartDefinitionAsset definition, string socketId);
        bool RemovePart(string socketId);
        SocketModel GetSocket(string socketId);

        void Move(float moveInput, float turnInput);
    }

    public class RobotController : MonoBehaviourController, IRobotController
    {
        private readonly IRobotView view;
        private readonly RobotModel model;
        private readonly RobotStatsCalculator statsCalculator;

        public RobotController(IRobotView view) : base(view)
        {
            this.view = view;
            statsCalculator = new RobotStatsCalculator();

            List<SocketModel> sockets = new List<SocketModel>();

            foreach(IRobotSocketView socketView in view.SocketViews)
            {
                sockets.Add(new SocketModel(socketView.SocketId, socketView.Type));
            }

            model = new RobotModel(sockets, 15f, 100f);
            RecalculateStats();
        }

        public bool PlacePart(PartDefinitionAsset definition, string socketId)
        {
            if (definition == null)
            {
                Debug.LogError($"Part definition not defined");
                return false;
            }

            SocketModel socketModel = model.sockets.Find(s => s.id == socketId);
            if (socketModel == null)
            {
                Debug.Log($"Socket model not found");
                return false;
            }

            if (socketModel.isOccupied)
            {
                Debug.Log($"Socket model is occupied");
                return false;
            }

            if (socketModel.typeAllowed != definition.socketTypeAllowed)
            {
                Debug.Log($"Socket type not allowed");
                return false;
            }

            IRobotSocketView socketView = view.GetSocketViewInterface(socketId);
            if (socketView == null)
            {
                Debug.Log($"Socket view not found");
                return false;
            }

            GameObject instance = GameObject.Instantiate(definition.prefab, socketView.AttachPoint);
            instance.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            RobotPartView partView = instance.GetComponent<RobotPartView>();
            if(partView == null)
            {
                Debug.Log($"Prefab does not contain and instance of RobotPartView");
                GameObject.Destroy(instance);
                return false;
            }

            partView.Initialize(definition);

            RobotPartModel partModel = new RobotPartModel(definition);
            socketModel.SetPart(partModel, partView);
            model.parts.Add(partModel);

            RecalculateStats();
            return true;
        }

        private void RecalculateStats()
        {
            model.SetStats(statsCalculator.calculate(model));
            Debug.Log($"mass: {model.stats.totalMass} | hp: {model.stats.totalHP}");
        }

        public bool RemovePart(string socketId)
        {
            SocketModel socketModel = model.sockets.Find(s => s.id == socketId);
            if(socketModel == null || !socketModel.isOccupied)
            {
                Debug.Log($"Socket model does not exist or is not occupied");
                return false;
            }

            RobotPartModel partModel = socketModel.currentPart;
            RobotPartView partView = socketModel.currentPartView;
            model.parts.Remove(partModel);

            if(partView != null)
            {
                GameObject.Destroy(partView.gameObject);
            }

            socketModel.Clear();
            RecalculateStats();
            return true;
        }

        public void Move(float moveInput, float turnInput)
        {
            if (view.RB == null) return;
            if (model.stats.drivePower <= 0f) return;

            float moveForce = model.stats.drivePower * view.MoveForceMultiplier;
            float turnTorque = model.stats.drivePower * view.TurnTorqueMultiplier;

            Vector3 forwardForce = view.Transform.forward * moveInput * moveForce;
            view.RB.AddForce(forwardForce, ForceMode.Force);

            float turnFactor = Mathf.Abs(moveInput) > 0.1f ? 1f : 0.35f;
            float turnDirection = moveInput < 0f ? -1f : 1f;
            Vector3 torque = Vector3.up * turnInput * turnTorque * turnFactor * turnDirection;
            view.RB.AddTorque(torque, ForceMode.Force);

            Vector3 horizontalVelocity = view.RB.linearVelocity;
            horizontalVelocity.y = 0f;

            float maxSpeed = 8f;
            if (horizontalVelocity.magnitude > maxSpeed)
            {
                Vector3 limited = horizontalVelocity.normalized * maxSpeed;
                view.RB.linearVelocity = new Vector3(limited.x, view.RB.linearVelocity.y, limited.z);
            }
        }

        public SocketModel GetSocket(string socketId)
        {
            return model.sockets.Find(s => s.id == socketId);
        }
    }
}