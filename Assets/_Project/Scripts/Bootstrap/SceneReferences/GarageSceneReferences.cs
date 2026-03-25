using BattleBots.BuildMode;
using BattleBots.Robot;
using BattleBots.UI;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GarageSceneReferences : MonoBehaviour
{
    [SerializeField] private BuildView buildView;
    [SerializeField] private BuildPreviewView buildPreviewView;
    [SerializeField] private PartCatalogView partCatalogView;
    [SerializeField] private RobotView robotView;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private List<PartDefinitionAsset> availableParts;
    [SerializeField] private RobotStatsPanelView robotStatsPanelView;

    // input references
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference leftClickAction;
    [SerializeField] private InputActionReference rightClickAction;
    [SerializeField] private InputActionReference cancelAction;

    public BuildView BuildView => buildView;
    public BuildPreviewView BuildPreviewView => buildPreviewView;
    public PartCatalogView PartCatalogView => partCatalogView;
    public RobotView RobotView => robotView;
    public Camera MainCamera => mainCamera;
    public List<PartDefinitionAsset> AvailableParts => availableParts;
    public RobotStatsPanelView RobotStatsPanelView => robotStatsPanelView;
    public InputActionReference MoveAction => moveAction;
    public InputActionReference LeftClickAction => leftClickAction;
    public InputActionReference RightClickAction => rightClickAction;
    public InputActionReference CancelAction => cancelAction;
}
