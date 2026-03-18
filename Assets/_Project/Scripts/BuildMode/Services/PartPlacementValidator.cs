using BattleBots.Robot;
using BattleBots.Core;
using UnityEngine;

namespace BattleBots.BuildMode
{
    public class PartPlacementValidator
    {
        public PlacementValidationResult Validate(PartDefinitionAsset definition, SocketModel model)
        {
            if (definition == null)
            {
                return PlacementValidationResult.Invalid("No part selected");
            }

            if (model == null)
            {
                return PlacementValidationResult.Invalid("Socket not found");
            }
            
            if (model.isOccupied)
            {
                return PlacementValidationResult.Invalid("Socket is already occupied");
            }

            if (model.typeAllowed != definition.socketTypeAllowed)
            {
                return PlacementValidationResult.Invalid("Socket type is not compatible with this part");
            }

            return PlacementValidationResult.Valid();
        }
    }
}