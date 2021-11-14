using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Doozy.Engine;
using Doozy.Engine.UI;

namespace ElMapacho
{
    public class CharacterMenuUI : MonoBehaviour
    {
        public enum CharacterUIState { player, equipment, options, equip, info, unequip }
        private CharacterUIState _UIState;
        public enum GearType { Helmet, Chest, Weapon }
        private GearType _gearType;
        [Header("Players Panel")]
        public Button player1Button;
        public TMP_Text player1ButtonLabel;

        public Button player2Button;
        public TMP_Text player2ButtonLabel;

        public Button player3Button;
        public TMP_Text player3ButtonLabel;


        [Header("Character Stats")]
        public TMP_Text hpValue;
        public TMP_Text manaValue;
        public TMP_Text levelValue;
        public TMP_Text speed;
        public TMP_Text pAttackValue;
        public TMP_Text pDefenseValue;
        public TMP_Text mAttackValue;
        public TMP_Text mDefenseValue;

        [Header("Gear Panel")]
        public Button helmetButton;
        public TMP_Text helmetLabel;

        public Button chestButton;
        public TMP_Text chestLabel;

        public Button weaponButton;
        public TMP_Text weaponLabel;


        [Header("Options")]
        public RectTransform optionsContainer;
        public Button equipButton;
        public Button infoButton;
        public Button unequipButton;
        public RectTransform helmetPosition1;
        public RectTransform chestPosition2;
        public RectTransform weaponPosition3;

        [Header("Equip")]
        public RectTransform equipContainer;
        public List<InventoryButton> inventoryButtons = new List<InventoryButton>();
        public Button backEquipButton;

        private FighterStats _currentPlayer;
        private EventSystem _eventSystem;
        private Button _lastPlayerButton;
        private Button _lastEquipmentButton;
        private Button _lastOptionsButton;

        private UIView _uIView;

        private UIPopup _popupToShow;
        void Start()
        {
            _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            _uIView = GetComponent<UIView>();
            _UIState = CharacterUIState.player;
        }

        void Update()
        {
            if (Input.GetButtonDown("Back"))
            {
                switch (_UIState)
                {
                    case CharacterUIState.player:
                        GameEventMessage.SendEvent("Characters");
                        break;
                    case CharacterUIState.equipment:
                        _lastPlayerButton.interactable = true;
                        _eventSystem.SetSelectedGameObject(_lastPlayerButton.gameObject);
                        _UIState = CharacterUIState.player;
                        break;
                    case CharacterUIState.options:
                        _lastEquipmentButton.interactable = true;
                        _eventSystem.SetSelectedGameObject(_lastEquipmentButton.gameObject);
                        optionsContainer.gameObject.SetActive(false);
                        _UIState = CharacterUIState.equipment;
                        break;
                    case CharacterUIState.info:
                        _UIState = CharacterUIState.options;
                        break;
                    case CharacterUIState.equip:
                        _UIState = CharacterUIState.options;
                        _eventSystem.SetSelectedGameObject(_lastOptionsButton.gameObject);
                        equipContainer.gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
            }

        }

        /// <summary>
        /// Update buttons label to respective players and set up navigations.
        /// </summary>
        public void xShowUIView()
        {
            if (GameManager.a.player1 != null)
            {
                player1ButtonLabel.text = GameManager.a.player1.nombre;
            }
            else
            {
                player1Button.gameObject.SetActive(false);
            }
            if (GameManager.a.player2 != null)
            {
                player2ButtonLabel.text = GameManager.a.player2.nombre;
            }
            else
            {
                player2Button.gameObject.SetActive(false);
            }
            if (GameManager.a.player3 != null)
            {
                player3ButtonLabel.text = GameManager.a.player3.nombre;
            }
            else
            {
                player3Button.gameObject.SetActive(false);
            }

            var x = new Navigation();
            x.mode = Navigation.Mode.Explicit;
            if (player2Button.gameObject.activeSelf)
            {
                x.selectOnDown = player2Button;
            }
            player1Button.navigation = x;

            var y = new Navigation();
            y.mode = Navigation.Mode.Explicit;
            if (player3Button.gameObject.activeSelf)
            {
                y.selectOnDown = player3Button;
            }
            y.selectOnUp = player1Button;
            player2Button.navigation = y;

            var z = new Navigation();
            z.mode = Navigation.Mode.Explicit;
            z.selectOnUp = player2Button;
            player3Button.navigation = z;

            _currentPlayer = GameManager.a.player1; 
            UpdatingStats(_currentPlayer);
        }

        /// <summary>
        /// Remembers the last player selected.
        /// </summary>
        /// <param name="buttonSelected"></param>
        public void xSelectedPlayerButton(Button buttonSelected)
        {
            int PlayerNumber = buttonSelected.transform.GetSiblingIndex() + 1;
            switch (PlayerNumber)
            {
                case 1:
                    _currentPlayer = GameManager.a.player1;
                    UpdatingStats(_currentPlayer);
                    break;
                case 2:
                    _currentPlayer = GameManager.a.player2;
                    UpdatingStats(_currentPlayer);
                    break;
                case 3:
                    _currentPlayer = GameManager.a.player3;
                    UpdatingStats(_currentPlayer);
                    break;
            }

            if (_lastPlayerButton != null)
                _lastPlayerButton.interactable = true;
            if (_lastEquipmentButton != null)
                _lastEquipmentButton.interactable = true;
            if (_lastOptionsButton != null)
                _lastOptionsButton.interactable = true;
            _UIState = CharacterUIState.player;
            _lastPlayerButton = buttonSelected;
            optionsContainer.gameObject.SetActive(false);

        }

        /// <summary>
        /// Update the values of the player stats.
        /// </summary>
        /// <param name="player"></param>
        private void UpdatingStats(FighterStats player)
        {
            if (player == null)
            {
                Debug.LogError("Missing Player");
                return;
            }

            player.UpdateStats();

            hpValue.text = player.derivedStats.currentHp.ToString() + "/" + player.derivedStats.maxHp.ToString();
            manaValue.text = player.derivedStats.currentMana + "/" + player.derivedStats.maxMana.ToString();
            levelValue.text = player.level.ToString();
            speed.text = player.orderSpeed.ToString();
            pAttackValue.text = player.derivedStats.pAttack.ToString();
            pDefenseValue.text = player.derivedStats.pDefense.ToString();
            mAttackValue.text = player.derivedStats.mAttack.ToString();
            mDefenseValue.text = player.derivedStats.mDefense.ToString();

            if (player.helmetEquipped == null)
            {
                helmetLabel.text = "EMPTY";
            }
            else helmetLabel.text = player.helmetEquipped.itemName;

            if (player.chestEquipped == null)
            {
                chestLabel.text = "EMPTY";
            }
            else chestLabel.text = player.chestEquipped.itemName;

            if (player.weaponEquipped == null)
            {
                weaponLabel.text = "EMPTY";
            }
            else weaponLabel.text = player.weaponEquipped.itemName;
        }


        /// <summary>
        /// Change into down panel.
        /// </summary>
        public void xClickPlayerButton(Button buttonPressed)
        {
            _eventSystem.SetSelectedGameObject(helmetButton.gameObject);
            _lastPlayerButton.interactable = false;
            _UIState = CharacterUIState.equipment;
        }

        /// <summary>
        /// Remembers the last Equiment Button.
        /// </summary>
        /// <param name="equipmentButtonSelected"></param>
        public void xSelectEquipmentButton(Button equipmentButtonSelected)
        {
            if (_lastOptionsButton != null)
                _lastOptionsButton.interactable = true;
            _UIState = CharacterUIState.equipment;
            if (_lastEquipmentButton != null)
                _lastEquipmentButton.interactable = true;
            _lastEquipmentButton = equipmentButtonSelected;
            optionsContainer.gameObject.SetActive(false);
        }

        /// <summary>
        /// Show options about the equipt selected.
        /// </summary>
        public void xClickEquipmentButton(Button equipmentButtonClicked)
        {
            _lastEquipmentButton.interactable = false;
            _UIState = CharacterUIState.options;
            optionsContainer.gameObject.SetActive(true);
            if (helmetButton.gameObject == equipmentButtonClicked.gameObject)
            {
                optionsContainer.position = helmetPosition1.position;
                _gearType = GearType.Helmet;
                if (_currentPlayer.helmetEquipped == null)
                {
                    infoButton.gameObject.SetActive(false);
                    unequipButton.gameObject.SetActive(false);
                }
                else
                {
                    infoButton.gameObject.SetActive(true);
                    unequipButton.gameObject.SetActive(true);
                }
            }
            else if (chestButton.gameObject == equipmentButtonClicked.gameObject)
            {
                optionsContainer.position = chestPosition2.position;
                _gearType = GearType.Chest;
                if (_currentPlayer.chestEquipped == null)
                {
                    infoButton.gameObject.SetActive(false);
                    unequipButton.gameObject.SetActive(false);
                }
                else
                {
                    infoButton.gameObject.SetActive(true);
                    unequipButton.gameObject.SetActive(true);
                }
            }
            else if (weaponButton.gameObject == equipmentButtonClicked.gameObject)
            {
                optionsContainer.position = weaponPosition3.position;
                _gearType = GearType.Weapon;
                if (_currentPlayer.weaponEquipped == null)
                {
                    infoButton.gameObject.SetActive(false);
                    unequipButton.gameObject.SetActive(false);
                }
                else
                {
                    infoButton.gameObject.SetActive(true);
                    unequipButton.gameObject.SetActive(true);
                }
            }
            _eventSystem.SetSelectedGameObject(equipButton.gameObject);
        }

        #region OptionsButton

        /// <summary>
        /// Remember the last options selected.
        /// </summary>
        /// <param name="optionsButtonSelected"></param>
        public void xSelectedOptionsButton(Button optionsButtonSelected)
        {
            _lastOptionsButton = optionsButtonSelected;
            _UIState = CharacterUIState.options;
        }

        /// <summary>
        /// Open the inventory with only that gear type.
        /// </summary>
        public void xClickEquip()
        {
            _UIState = CharacterUIState.equip;

            equipContainer.gameObject.SetActive(true);
            foreach (var inventoryButton in inventoryButtons)
            {
                var f = new Navigation();
                f.mode = Navigation.Mode.Vertical;
                inventoryButton.thisButton.navigation = f;
                inventoryButton.gameObject.SetActive(false);
            }

            switch (_gearType)
            {
                case GearType.Helmet:
                    var a = InventoryManager.a.GiveHelmetList();
                    for (int i = 0; i < a.Count; i++)
                    {
                        inventoryButtons[i].ammountText.text = a[i].amount.ToString();
                        inventoryButtons[i].itemText.text = a[i].itemName.ToString();
                        inventoryButtons[i].spriteToShow.sprite = a[i].icon;
                        inventoryButtons[i].gameObject.SetActive(true);
                    }
                    if (a.Count > 1)
                        Utilities.SetUpButtonNavigation(inventoryButtons[a.Count - 1].thisButton, inventoryButtons[a.Count - 2].thisButton, backEquipButton, null, null);
                    else
                    {
                        Utilities.SetUpButtonNavigation(inventoryButtons[a.Count - 1].thisButton, null, backEquipButton, null, null);
                    }
                    Utilities.SetUpButtonNavigation(backEquipButton, inventoryButtons[a.Count - 1].thisButton, null, null, null);
                    break;
                case GearType.Chest:
                    var b = InventoryManager.a.GiveChestList();
                    for (int i = 0; i < b.Count; i++)
                    {
                        inventoryButtons[i].ammountText.text = b[i].amount.ToString();
                        inventoryButtons[i].itemText.text = b[i].itemName.ToString();
                        inventoryButtons[i].spriteToShow.sprite = b[i].icon;
                        inventoryButtons[i].gameObject.SetActive(true);
                    }
                    if (b.Count > 1)
                        Utilities.SetUpButtonNavigation(inventoryButtons[b.Count - 1].thisButton, inventoryButtons[b.Count - 2].thisButton, backEquipButton, null, null);
                    else
                    {
                        Utilities.SetUpButtonNavigation(inventoryButtons[b.Count - 1].thisButton, null, backEquipButton, null, null);
                    }
                    Utilities.SetUpButtonNavigation(backEquipButton, inventoryButtons[b.Count - 1].thisButton, null, null, null);
                    break;
                case GearType.Weapon:
                    var c = InventoryManager.a.weaponList;
                    for (int i = 0; i < c.Count; i++)
                    {

                        inventoryButtons[i].ammountText.text = c[i].amount.ToString();
                        inventoryButtons[i].itemText.text = c[i].itemName.ToString();
                        inventoryButtons[i].spriteToShow.sprite = c[i].icon;
                        inventoryButtons[i].gameObject.SetActive(true);
                    }
                    if (c.Count > 1)
                        Utilities.SetUpButtonNavigation(inventoryButtons[c.Count - 1].thisButton, inventoryButtons[c.Count - 2].thisButton, backEquipButton, null, null);
                    else
                    {
                        Utilities.SetUpButtonNavigation(inventoryButtons[c.Count - 1].thisButton, null, backEquipButton, null, null);
                    }
                    Utilities.SetUpButtonNavigation(backEquipButton, inventoryButtons[c.Count - 1].thisButton, null, null, null);
                    break;
            }
            if (inventoryButtons[0].gameObject.activeSelf)
                _eventSystem.SetSelectedGameObject(inventoryButtons[0].gameObject);
            else
            {
                Utilities.SetUpButtonNavigation(backEquipButton, null, null, null, null);
                _eventSystem.SetSelectedGameObject(backEquipButton.gameObject);
            }
        }

        /// <summary>
        /// Close the inventory tab.
        /// </summary>
        public void xCloseEquip()
        {
            _UIState = CharacterUIState.options;
            _eventSystem.SetSelectedGameObject(_lastOptionsButton.gameObject);
            equipContainer.gameObject.SetActive(false);
        }

        public void xClickEquipItem(Button button)
        {
            var index = button.gameObject.transform.GetSiblingIndex();
            switch (_gearType)
            {
                case GearType.Helmet:
                    var helmetList = InventoryManager.a.GiveHelmetList();
                    if (_currentPlayer.helmetEquipped != null)
                    {
                        var lastHelmet = _currentPlayer.helmetEquipped;
                        InventoryManager.a.AddArmor(lastHelmet);
                    }
                    _currentPlayer.helmetEquipped = helmetList[index];
                    InventoryManager.a.armorList.Remove(helmetList[index]);
                    break;
                case GearType.Chest:
                    var chestList = InventoryManager.a.GiveChestList();
                    if (_currentPlayer.chestEquipped != null)
                    {
                        var lastChest = _currentPlayer.chestEquipped;
                        InventoryManager.a.AddArmor(lastChest);
                    }
                    _currentPlayer.chestEquipped = chestList[index];
                    InventoryManager.a.armorList.Remove(chestList[index]);
                    break;
                case GearType.Weapon:
                    var weaponList = InventoryManager.a.weaponList;
                    InventoryManager.a.EquipWeaponFromInventory(_currentPlayer, weaponList[index]);
                    break;
            }

            _UIState = CharacterUIState.options;
            _eventSystem.SetSelectedGameObject(_lastOptionsButton.gameObject);
            equipContainer.gameObject.SetActive(false);
            xClickEquipmentButton(_lastEquipmentButton);// warning
            UpdatingStats(_currentPlayer);
        }


        /// <summary>
        /// Gets call when a info button is click. Show info about that gear;
        /// </summary>
        public void xClickChangeInfo()
        {
            _UIState = CharacterUIState.info;
            _popupToShow = UIPopup.GetPopup(PopUpNames.GearInfo.ToString());
            int amountOfLabels = _popupToShow.Data.Labels.Count;
            switch (_gearType)
            {
                case GearType.Helmet:
                    _popupToShow.Data.SetLabelsTexts(GetHelmetLabels(amountOfLabels));
                    break;
                case GearType.Chest:
                    _popupToShow.Data.SetLabelsTexts(GetChestLabels(amountOfLabels));
                    break;
                case GearType.Weapon:
                    _popupToShow.Data.SetLabelsTexts(GetWeaponLabels(amountOfLabels));
                    break;
            }
            _popupToShow.AutoSelectPreviouslySelectedButtonAfterHide = true;
            _popupToShow.Data.SetButtonsCallbacks(SetStateToOptions);
            _popupToShow.Show();
        }

        private string[] GetHelmetLabels(int amountOfLabels)
        {
            List<string> labelsText = new List<string>();
            labelsText.Add(_currentPlayer.helmetEquipped.itemName);
            labelsText.Add(_currentPlayer.helmetEquipped.description);
            labelsText.Add("P.Defense");
            labelsText.Add(_currentPlayer.helmetEquipped.addPDefense.ToString());
            labelsText.Add("M.Defense");
            labelsText.Add(_currentPlayer.helmetEquipped.addMDefense.ToString());
            labelsText.Add("Confusion Inm.");
            if (_currentPlayer.helmetEquipped.inmuneConfusion)
                labelsText.Add("Yes");
            else labelsText.Add("No");

            int x = labelsText.Count;
            for (int i = 0; i < amountOfLabels - x; i++)
            {
                labelsText.Add("");
            }
            var a = labelsText.ToArray();
            return a;
        }

        private string[] GetChestLabels(int amountOfLabels)
        {
            List<string> labelsText = new List<string>();
            labelsText.Add(_currentPlayer.chestEquipped.itemName);
            labelsText.Add(_currentPlayer.chestEquipped.description);
            labelsText.Add("P.Defense");
            labelsText.Add(_currentPlayer.chestEquipped.addPDefense.ToString());
            labelsText.Add("M.Defense");
            labelsText.Add(_currentPlayer.chestEquipped.addMDefense.ToString());
            labelsText.Add("Confusion Inm.");
            if (_currentPlayer.chestEquipped.inmuneConfusion)
                labelsText.Add("Yes");
            else labelsText.Add("No");

            int x = labelsText.Count;
            for (int i = 0; i < amountOfLabels - x; i++)
            {
                labelsText.Add("");
            }
            var a = labelsText.ToArray();
            return a;
        }

        private string[] GetWeaponLabels(int amountOfLabels)
        {
            List<string> labelsText = new List<string>();
            labelsText.Add(_currentPlayer.weaponEquipped.itemName);
            labelsText.Add(_currentPlayer.weaponEquipped.description);
            labelsText.Add("P.Damage");
            labelsText.Add(_currentPlayer.weaponEquipped.addPDamage.ToString());
            labelsText.Add("M.Damage");
            labelsText.Add(_currentPlayer.weaponEquipped.addMDamage.ToString());
            labelsText.Add("Health");
            labelsText.Add(_currentPlayer.weaponEquipped.addHealth.ToString());
            labelsText.Add("Mana");
            labelsText.Add(_currentPlayer.weaponEquipped.addMana.ToString());

            int x = labelsText.Count;
            for (int i = 0; i < amountOfLabels - x; i++)
            {
                labelsText.Add("");
            }
            var a = labelsText.ToArray();
            return a;
        }

        /// <summary>
        /// Gets call when a info button is click. Unequip 
        /// </summary>
        public void xClickUnequip()
        {
            switch (_gearType)
            {
                case GearType.Helmet:
                    var helmet = _currentPlayer.helmetEquipped;
                    InventoryManager.a.AddArmor(helmet);
                    _currentPlayer.helmetEquipped = null;
                    break;
                case GearType.Chest:
                    var chest = _currentPlayer.chestEquipped;
                    InventoryManager.a.AddArmor(chest);
                    _currentPlayer.chestEquipped = null;
                    break;
                case GearType.Weapon:
                    var weapon = _currentPlayer.weaponEquipped;
                    InventoryManager.a.AddWeapon(weapon);
                    _currentPlayer.weaponEquipped = null;
                    break;
            }
            _eventSystem.SetSelectedGameObject(equipButton.gameObject);
            unequipButton.gameObject.SetActive(false);
            infoButton.gameObject.SetActive(false);
            Debug.Log("Unequip");
            UpdatingStats(_currentPlayer);
        }
        private void SetStateToOptions()
        {
            _UIState = CharacterUIState.options;
        }
        #endregion
    }
}