using UnityEngine;
using System.Collections.Generic;
using BattleBots.Robot;

namespace BattleBots.Physics
{
    public static class CenterOfMassService
    {
        public static void Recalculate(Rigidbody rb, Transform root, IEnumerable<RobotPartView> parts, float baseMass)
        {
            if (rb == null || root == null)
            {
                Debug.Log($"Rigidbody or tranform root not found");
                return;
            }

            float totalMass = baseMass;
            Vector3 weighted = root.position * baseMass;

            foreach (RobotPartView part in parts)
            {
                if (part == null || part.Definition == null)
                {
                    Debug.Log($"Part or part definition not found");
                    return;
                }

                float mass = Mathf.Max(0.01f, part.Definition.mass);
                totalMass += mass;
                weighted += part.transform.position * mass;
            }

            Vector3 worldCenter = weighted / totalMass;
            Vector3 localcenter = root.InverseTransformPoint(worldCenter);

            localcenter.y -= 0.4f;
            rb.centerOfMass = localcenter;
        }
    }
}
