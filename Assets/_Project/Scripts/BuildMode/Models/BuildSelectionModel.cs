using BattleBots.Robot;
using UnityEngine;


namespace BattleBots.BuildMode
{
    public class BuildSelectionModel
    {
        public PartDefinitionAsset SelectedPart {  get; private set; }
        public bool HasSelection => SelectedPart != null;

        public void SetSelectedPart(PartDefinitionAsset part)
        {
            SelectedPart = part;
        }

        public void Clear()
        {
            SelectedPart = null;
        }
    }
}
