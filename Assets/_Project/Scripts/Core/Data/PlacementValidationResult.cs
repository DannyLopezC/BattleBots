using UnityEngine;

namespace BattleBots.Core
{
    public struct PlacementValidationResult
    {
        public bool IsValid;
        public string Reason;

        public PlacementValidationResult(bool isValid, string reason)
        {
            IsValid = isValid;
            Reason = reason;
        }

        public static PlacementValidationResult Valid()
        {
            return new PlacementValidationResult(true, "Valid placement");
        }

        public static PlacementValidationResult Invalid(string reason)
        {
            return new PlacementValidationResult(false, reason);
        }
    }
}