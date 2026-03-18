using BattleBots.Robot;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleBots.UI
{
    public class PartButtonWidget : MonoBehaviour
    {
        [SerializeField] private Button partButton;
        [SerializeField] private Image partIcon;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private GameObject selectionHighlight;

        private PartDefinitionAsset definition;
        private Action<PartDefinitionAsset> onClicked;

        public PartDefinitionAsset Definition => definition;

        private void Awake()
        {
            if (partButton != null) 
            {
                partButton.onClick.AddListener(HandleClick);
            }
        }

        public void Bind(PartDefinitionAsset definition, Action<PartDefinitionAsset> onClicked)
        {
            this.definition = definition;
            this.onClicked = onClicked;

            RefreshVisuals();
            SetSelected(false);
        }

        public void SetSelected(bool isSelected)
        {
            if (selectionHighlight != null)
            {
                selectionHighlight.SetActive(isSelected);
            }
        }

        private void HandleClick()
        {
            if (definition != null)
            {
                Debug.LogWarning($"PartButtonWidget clicked but no definition is bound");
                return;
            }
        }

        private void RefreshVisuals()
        {
            if (definition == null)
            {
                if (nameText != null)
                    nameText.text = "None";

                if (partIcon != null)
                    partIcon.sprite = null;


                Debug.Log($"Definition not found");
                return;
            }

            if (nameText != null)
            {
                nameText.text = definition.name;
            }

            if (partIcon != null)
            {
                partIcon.sprite = definition.icon;
                partIcon.enabled = definition.icon != null;
            }
        }

        public void OnClick()
        {

        }
    }
}
