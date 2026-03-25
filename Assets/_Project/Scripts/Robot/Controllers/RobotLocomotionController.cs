using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBots.Robot
{
    public interface IRobotLocomotionController : IMonoBehaviourController
    {
        void Tick(float moveInput, float turnInput);
    }

    public class RobotLocomotionController : MonoBehaviourController, IRobotLocomotionController
    {
        private readonly IRobotView view;
        private readonly RobotModel model;
        private Rigidbody rb;

        private readonly float maxSpeed = 8.0f;
        private readonly float reverseMultiplier = 0.75f;
        private readonly float turnInPlaceMultiplier = 0.6f;
        private readonly float lateralDamping = 3f;

        public RobotLocomotionController(IRobotView view, RobotModel model) : base(view)
        {
            this.view = view;
            this.model = model;
            this.rb = view.RB;
        }

        public void Tick(float moveInput, float turnInput)
        {
            if (rb == null)
            {
                Debug.Log($"RigidBody not found");
                return;
            }
            
            if (model.stats == null)
            {
                Debug.Log($"Stats not found");
                return;
            }

            if (model.stats.drivePower <= 0.0f)
            {
                //Debug.Log($"Not enough drive power to move");
                return;
            }

            ApplyForwardForce(moveInput);
            ApplyTurnTorque(moveInput, turnInput);
            ApplyLateralGrip();
            ClampHorizontalSpeed();
        }

        private void ApplyForwardForce(float moveInput) 
        {
            if (Mathf.Abs(moveInput) < 0.01f) return;

            float moveForce = model.stats.drivePower * view.MoveForceMultiplier;
            float multiplier = moveInput < 0.0f ? reverseMultiplier : 1f;

            Vector3 force = view.Transform.forward * moveInput * moveForce; // * multiplier;
            rb.AddForce(force, ForceMode.Force);
        }

        private void ApplyTurnTorque(float moveInput, float turnInput)
        {
            if (Mathf.Abs(turnInput) < 0.01f) return;

            float turnTorque = model.stats.drivePower * view.TurnTorqueMultiplier;

            float turnFactor = Mathf.Abs(moveInput) > 0.1f ? 1f : turnInPlaceMultiplier;
            float turnDirection = moveInput < 0.0f ? -1f : 1f;

            Vector3 torque = Vector3.up * turnInput * turnTorque * turnFactor * turnDirection;
            rb.AddTorque(torque, ForceMode.Force);
        }

        private void ApplyLateralGrip()
        {
            Vector3 localVelocity = view.Transform.InverseTransformDirection(rb.linearVelocity);
            localVelocity.x = Mathf.Lerp(localVelocity.x, 0f, lateralDamping * Time.fixedDeltaTime);
            rb.linearVelocity = view.Transform.TransformDirection(localVelocity);
        }

        private void ClampHorizontalSpeed()
        {
            Vector3 velocity = rb.linearVelocity;
            Vector3 horizontal = new Vector3(velocity.x, 0f, velocity.z);

            if (horizontal.magnitude > maxSpeed)
            {
                Vector3 limited = horizontal.normalized * maxSpeed;
                rb.linearVelocity = new Vector3(limited.x, velocity.y, limited.z);
            }
        }
    }
}