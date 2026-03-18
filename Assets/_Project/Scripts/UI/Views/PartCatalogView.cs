using BattleBots.Bootstrap;
using BattleBots.BuildMode;
using BattleBots.Robot;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBots.UI
{
    public interface IPartCatalogView : IMonoBehaviourView
    {
        void ShowParts(IReadOnlyList<PartDefinitionAsset> parts, Action<PartDefinitionAsset> onPartClicked);
        void SetSelectedPart(PartDefinitionAsset selectedPart);
        void Clear();
    }

    public class PartCatalogView : MonoBehaviourView, IPartCatalogView
    {
        private IPartCatalogUIController controller;

        [SerializeField] private GameObject partButtonWidgetPrefab;
        [SerializeField] private GameObject container;

        private readonly List<PartButtonWidget> spawnedButtons = new List<PartButtonWidget>();

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

            Debug.Log($"Installer: {installer}");
            Debug.Log($"BuildView: {installer.SceneReferences.BuildView}");
            Debug.Log($"BuildController before PartCatalog controller creation: {installer.SceneReferences.BuildView.GetController}");

            controller = new PartCatalogUIController(this,
                installer.Get<BuildCatalogService>(),
                installer.Get<IBuildController>()
                );
        }
        public void Clear()
        {
            for (int i = 0; i < spawnedButtons.Count; i++)
            {
                if (spawnedButtons[i] != null)
                {
                    Destroy(spawnedButtons[i].gameObject);
                }
            }

            spawnedButtons.Clear();
        }

        public void ShowParts(IReadOnlyList<PartDefinitionAsset> parts, Action<PartDefinitionAsset> onPartClicked)
        {
            Clear();

            if (parts == null || partButtonWidgetPrefab == null || container == null)
            {
                Debug.LogWarning("PartCatalogView is missing required references or parts list");
                return;
            }

            for (int i = 0; i < parts.Count; i++)
            {
                PartDefinitionAsset part = parts[i];

                if (part == null)
                    continue;

                GameObject GO = Instantiate(partButtonWidgetPrefab, container.transform);
                PartButtonWidget widget = GO.GetComponent<PartButtonWidget>();
                if (widget == null)
                {
                    Destroy(GO);
                    continue;
                }
                widget.Bind(part, onPartClicked);
                widget.SetSelected(false);

                spawnedButtons.Add(widget);
            }
        }

        public void SetSelectedPart(PartDefinitionAsset selectedPart)
        {
            for (int i = 0;i < spawnedButtons.Count;i++)
            {
                PartButtonWidget widget = spawnedButtons[i];
                bool isSelected = widget.Definition == selectedPart;
                widget.SetSelected(isSelected);
            }
        }
    }
}
