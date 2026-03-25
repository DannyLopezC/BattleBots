using UnityEngine;

namespace BattleBots.Physics
{
    public class CenterOfMassDebugDrawer : MonoBehaviour
    {
        [SerializeField] private Rigidbody targetRb;
        [SerializeField, Range(0, 1)] private float sphereRadius = 0.08f;

        private void OnDrawGizmos()
        {
            if (targetRb == null)
            {
                Debug.Log($"Target rigidbody not found");
                return;
            }

            Vector3 worldCenter = targetRb.worldCenterOfMass;

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(worldCenter, sphereRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(worldCenter, worldCenter + Vector3.up * 0.3f);
        }
    }
}
