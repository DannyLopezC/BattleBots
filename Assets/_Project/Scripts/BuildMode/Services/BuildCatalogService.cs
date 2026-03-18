using UnityEngine;
using System;
using System.Collections.Generic;
using BattleBots.Robot;

namespace BattleBots.BuildMode
{
    public class BuildCatalogService
    {
        private readonly List<PartDefinitionAsset> parts;

        public BuildCatalogService(List<PartDefinitionAsset> parts)
        {
            this.parts = parts;
        }

        public IReadOnlyList<PartDefinitionAsset> GetAvailableParts()
        {
            return parts;
        }
    }
}
