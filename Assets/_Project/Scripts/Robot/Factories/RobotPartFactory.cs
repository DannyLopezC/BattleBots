using System;
using UnityEngine;

namespace BattleBots.Robot
{
    public class RobotPartFactory
    {
        public IRobotPartView CreatePart(PartDefinitionAsset definition, Transform parent)
        {
            if(definition == null || definition.prefab == null)
            {
                //Debug.Log($"Prefab {definition.name} has no Robot");
                Debug.LogError($"Invalid part definition or missing prefab.");
                return null;
            }

            GameObject instance = GameObject.Instantiate(definition.prefab, parent);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;

            IRobotPartView partView = instance.GetComponent<IRobotPartView>();

            if(partView != null ) 
            {
                Debug.LogError($"Prefab {definition.name} has no RobotPartView");
                return null;
            }

            partView.Initialize(definition);
            return partView;
        }
    }
}
