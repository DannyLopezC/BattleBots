using BattleBots.BuildMode;
using BattleBots.Robot;
using BattleBots.UI;
using UnityEngine;
using System.Collections.Generic;

public class GarageSceneReferences : MonoBehaviour
{
    [SerializeField] private BuildView buildView;
    [SerializeField] private BuildPreviewView buildPreviewView;
    [SerializeField] private PartCatalogView partCatalogView;
    [SerializeField] private RobotView robotView;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private List<PartDefinitionAsset> availableParts;

    public BuildView BuildView => buildView;
    public BuildPreviewView BuildPreviewView => buildPreviewView;
    public PartCatalogView PartCatalogView => partCatalogView;
    public RobotView RobotView => robotView;
    public Camera MainCamera => mainCamera;
    public List<PartDefinitionAsset> AvailableParts => availableParts;
}
