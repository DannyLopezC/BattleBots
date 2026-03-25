using BattleBots.Physics;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleBots.Robot
{
    public interface IRobotController: IMonoBehaviourController
    {
        bool PlacePart(PartDefinitionAsset definition, string socketId);
        bool RemovePart(string socketId);
        SocketModel GetSocket(string socketId);
        RobotStatsModel GetStats();
        RobotModel GetModel();
    }

    public class RobotController : MonoBehaviourController, IRobotController
    {
        private readonly IRobotView view;
        private readonly RobotModel model;
        private readonly RobotStatsCalculator statsCalculator;

        private readonly IRobotLocomotionController locomotionController;
        private readonly IRobotInputController inputController;

        public RobotController(IRobotView view, InputAction moveAction) : base(view)
        {
            this.view = view;
            statsCalculator = new RobotStatsCalculator();

            List<SocketModel> sockets = new List<SocketModel>();

            foreach(IRobotSocketView socketView in view.SocketViews)
            {
                sockets.Add(new SocketModel(socketView.SocketId, socketView.Type));
            }

            model = new RobotModel(sockets, 15f, 100f, 100f);

            locomotionController = new RobotLocomotionController(view, model);
            inputController = new RobotInputController(moveAction);

            RecalculateStats();
            RecalculateCenterOfMass();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            inputController.Poll();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            locomotionController.Tick(inputController.MoveInput.y, inputController.MoveInput.x);
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
            RecalculateCenterOfMass();
            return true;
        }

        private void RecalculateStats()
        {
            model.SetStats(statsCalculator.calculate(model));
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
            RecalculateCenterOfMass();
            return true;
        }
        public SocketModel GetSocket(string socketId)
        {
            return model.sockets.Find(s => s.id == socketId);
        }

        public RobotStatsModel GetStats()
        {
            return model.stats;
        }

        public RobotModel GetModel()
        {
            return model;
        }

        private IEnumerable<RobotPartView> GetPlacedPartViews()
        {
            foreach (SocketModel socket in model.sockets)
            {
                if(socket.isOccupied && socket.currentPartView != null)
                {
                    yield return socket.currentPartView;
                }
            }
        }

        private void RecalculateCenterOfMass()
        {
            CenterOfMassService.Recalculate(
                view.RB,
                view.Transform,
                GetPlacedPartViews(),
                model.BaseMass
            );
        }
    }
}