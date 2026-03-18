using BattleBots.Robot;
using UnityEngine;


namespace BattleBots.BuildMode
{
    public class BuildPreviewModel
    {
        public PartDefinitionAsset SelectedPart {  get; private set; }
        public IRobotSocketView TargetSocketView {  get; private set; }

        public bool IsValidPlacement {  get; private set; }
        public bool HasPreview => SelectedPart != null && TargetSocketView != null;
        public bool HasValidPreview => HasPreview && IsValidPlacement;

        public void SetPreview(PartDefinitionAsset part, IRobotSocketView socketView, bool isValidPlacement)
        {
            SelectedPart = part;
            TargetSocketView = socketView;
            IsValidPlacement = isValidPlacement;
        }

        public void Clear()
        {
            SelectedPart = null;
            TargetSocketView = null;
            IsValidPlacement = false;
        }
    }
}
