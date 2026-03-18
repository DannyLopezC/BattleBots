using BattleBots.Robot;
using System.Net.Sockets;
using UnityEngine;

namespace BattleBots.BuildMode
{

    public interface IBuildPreviewView: IMonoBehaviourView
    {
        void ShowPreview(PartDefinitionAsset definition, IRobotSocketView socketView, bool isValid);
        void HidePreview();
        IBuildPreviewController GetController { get; }
    }

    public class BuildPreviewView : MonoBehaviourView, IBuildPreviewView
    {
        private IBuildPreviewController controller;

        [SerializeField] private Material validPreviewMaterial;
        [SerializeField] private Material invalidPreviewMaterial;

        private GameObject currentPreviewInstance;
        private PartDefinitionAsset currentDefinition;

        public IBuildPreviewController GetController => controller;

        protected override IMonoBehaviourController Controller()
        {
            return controller;
        }

        protected override void CreateController()
        {
            controller = new BuildPreviewController(this);
        }

        public void HidePreview()
        {
            if (currentPreviewInstance != null)
            {
                currentPreviewInstance.SetActive(false);
            }
        }

        public void ShowPreview(PartDefinitionAsset definition, IRobotSocketView socketView, bool isValid)
        {
            if (definition == null || socketView == null)
            {
                Debug.Log($"Definition or socket view null");
                HidePreview();
                return;
            }

            bool needsNewInstance = currentPreviewInstance == null || currentDefinition != definition;

            if (needsNewInstance)
            {
                RebuildPreview(definition);
            }

            currentPreviewInstance.transform.SetPositionAndRotation(
                socketView.Transform.position,
                socketView.Transform.rotation);

            ApplyPreviewMaterial(isValid ? validPreviewMaterial : invalidPreviewMaterial);
            currentPreviewInstance.SetActive(true);
        }

        private void RebuildPreview(PartDefinitionAsset definition)
        {
            if (currentPreviewInstance != null)
            {
                Destroy(currentPreviewInstance);
            }

            currentDefinition = definition;
            currentPreviewInstance = Instantiate(definition.prefab);



            DisableColliders(currentPreviewInstance);
            DisableRigidbodies(currentPreviewInstance);
        }

        private void DisableColliders(GameObject root)
        {
            Collider[] colliders = root.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
        }

        private void DisableRigidbodies(GameObject root)
        {
            Rigidbody[] rigidbodies = root.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = true;
                rb.detectCollisions = false;
            }
        }

        private void ApplyPreviewMaterial(Material mat)
        {
            if (currentPreviewInstance == null || mat == null)
            {
                Debug.Log($"Current preview instance or material null");
                return;
            }

            Renderer[] renderers = currentPreviewInstance.GetComponentsInChildren<Renderer>(true);
            foreach(Renderer renderer in renderers)
            {
                renderer.material = mat;
            }
        }
    }
}
