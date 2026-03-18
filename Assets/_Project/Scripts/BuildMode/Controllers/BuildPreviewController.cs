using BattleBots.Core;
using BattleBots.Robot;
using UnityEngine;

namespace BattleBots.BuildMode
{
    public interface IBuildPreviewController: IMonoBehaviourController
    {
        void Show(BuildPreviewModel previewModel);
        void Hide();
    }

    public class BuildPreviewController : MonoBehaviourController, IBuildPreviewController
    {
        private readonly IBuildPreviewView view;

        public BuildPreviewController(IBuildPreviewView view) : base(view)
        {
            this.view = view;
        }

        public void Hide()
        {
            view.HidePreview();
        }

        public void Show(BuildPreviewModel previewModel)
        {
            if (previewModel == null || !previewModel.HasPreview)
            {
                Debug.Log($"Preview model null or does not contain preview");
                Hide();
                return;
            }

            view.ShowPreview(previewModel.SelectedPart,
                previewModel.TargetSocketView,
                previewModel.IsValidPlacement);
        }
    }
}
