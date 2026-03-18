using BattleBots.BuildMode;
using BattleBots.Robot;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBots.UI
{
    public interface IPartCatalogUIController : IMonoBehaviourController
    {
        void RefreshCatalog();
    }

    public class PartCatalogUIController : MonoBehaviourController, IPartCatalogUIController
    {
        private readonly IPartCatalogView view;

        private BuildCatalogService buildCatalogService;
        private IBuildController buildController;

        public PartCatalogUIController(IPartCatalogView view, BuildCatalogService buildCatalogService, IBuildController buildController) : base(view)
        {
            this.view = view;
            this.buildCatalogService = buildCatalogService;
            this.buildController = buildController;
        }

        public override void OnStart()
        {
            base.OnStart();

            RefreshCatalog();
        }

        public void RefreshCatalog()
        {
            IReadOnlyList<PartDefinitionAsset> parts = buildCatalogService.GetAvailableParts();
            view.ShowParts(parts, HandlePartClicked);
        }

        private void HandlePartClicked(PartDefinitionAsset part)
        {
            if (part == null)
            {
                Debug.Log($"Definition not found");
                return;
            }

            buildController.SelectPart(part);
            view.SetSelectedPart(part);
        }
    }
}
