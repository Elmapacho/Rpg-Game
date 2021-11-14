using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine;
using TMPro;
using System;
using UnityEngine.EventSystems;
using Doozy.Engine.UI;

namespace ElMapacho
{
    public class InventoryMenuUI : MonoBehaviour
    {
        public TMP_Text tabText;
        public Scrollbar scrollbar;
        public GameObject leftArrow;
        public GameObject rightArrow;
        private Animator _leftArrowAnimator;
        private Animator _rightArrowAnimator;

        public List<InventoryButton> inventoryItems = new List<InventoryButton>();

        private InventoryButton _lastConsumableButton;
        private InventoryButton _lastUsableButton;
        private InventoryButton _lastArmorButton;
        private InventoryButton _lastWeaponButton;
        private InventoryButton _lastKeyItemButton;

        private Button _lastOptionButton;

        [Header("Inventory Options")]
        public RectTransform optionsHolder;
        public Button useConsumableButton;
        public Button equipItemButton;
        public Button infoButton;
        public Button dropButton;
        public Button useKeyItemButton;

        private EventSystem _eventSystem;
        public enum InventoryTabs { Consumable, BattleItems, Armor, Weapons, KeyItems }
        public InventoryTabs currentTab;
        public enum InventoryState { Item, Options, Animating,Popup }
        [SerializeField] private InventoryState _inventoryState;
        private InventoryState _previousState;

        private UIPopup _popupToShow;

        private int _scrollbarStep = 0;
        private int _index;
        private Consumable _consumableToUse;
        private Weapon _weaponToEquip;
        private Armor _chestToEquip;
        private Armor _helmetToEquip;

        void Awake()
        {
            currentTab = InventoryTabs.Consumable;
            _inventoryState = InventoryState.Item;
            _rightArrowAnimator = rightArrow.GetComponent<Animator>();
            _leftArrowAnimator = leftArrow.GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _scrollbarStep = 0;
            if (InventoryManager.a.consumableList.Count <= 7)
            {
                scrollbar.numberOfSteps = 1;
                scrollbar.size = 1;
                return;
            }
            else
            {
                scrollbar.numberOfSteps = InventoryManager.a.consumableList.Count - 6;
            }
            Message.AddListener<GameEventMessage>(OnMessage);
        }

        private void OnDisable()
        {
            Message.RemoveListener<GameEventMessage>(OnMessage);
        }
        private void OnMessage(GameEventMessage message)
        {
            if (message == null) return;

            if (message.EventName == UIEvents.InventoryArrowEndAnimation.ToString())
            {
                _inventoryState = _previousState;
            }
        }
        void Update()
        {
            if (Input.GetButtonDown("Back"))
            {
                switch (_inventoryState)
                {
                    case InventoryState.Item:
                        GameEventMessage.SendEvent("Inventory");
                        _lastConsumableButton = null;
                        _lastUsableButton = null;
                        _lastWeaponButton = null;
                        _lastArmorButton = null;
                        _lastKeyItemButton = null;
                        break;
                    case InventoryState.Options:
                        SelectInventoryButton();
                        optionsHolder.gameObject.SetActive(false);
                        _inventoryState = InventoryState.Item;
                        break;
                    case InventoryState.Animating:
                        break;
                    case InventoryState.Popup:
                        _eventSystem.SetSelectedGameObject(_lastOptionButton.gameObject);
                        _inventoryState = InventoryState.Options;
                        break;
                }
            }

            if (Input.GetButtonDown("Left"))
            {
                if (_inventoryState == InventoryState.Item)
                {
                    ChangeTabLeft();
                }
            }
            if (Input.GetButtonDown("Right"))
            {
                if (_inventoryState == InventoryState.Item)
                {
                    ChangeTabRight();
                }
            }
        }

        public void ChangeTabLeft()
        {
            if (_inventoryState == InventoryState.Animating) return;

            switch (currentTab)
            {
                case InventoryTabs.Consumable:
                    currentTab = InventoryTabs.KeyItems;
                    break;
                case InventoryTabs.BattleItems:
                    currentTab = InventoryTabs.Consumable;
                    break;
                case InventoryTabs.Armor:
                    currentTab = InventoryTabs.BattleItems;
                    break;
                case InventoryTabs.Weapons:
                    currentTab = InventoryTabs.Armor;
                    break;
                case InventoryTabs.KeyItems:
                    currentTab = InventoryTabs.Weapons;
                    break;
                default:
                    break;
            }
            ChangeTab();
            _leftArrowAnimator.SetTrigger("Selected");
        }
        public void ChangeTabRight()
        {
            if (_inventoryState == InventoryState.Animating) return;
            switch (currentTab)
            {
                case InventoryTabs.Consumable:
                    currentTab = InventoryTabs.BattleItems;
                    break;
                case InventoryTabs.BattleItems:
                    currentTab = InventoryTabs.Armor;
                    break;
                case InventoryTabs.Armor:
                    currentTab = InventoryTabs.Weapons;
                    break;
                case InventoryTabs.Weapons:
                    currentTab = InventoryTabs.KeyItems;
                    break;
                case InventoryTabs.KeyItems:
                    currentTab = InventoryTabs.Consumable;
                    break;
                default:
                    break;
            }
            ChangeTab();
            _rightArrowAnimator.SetTrigger("Selected");
        }
        private void ChangeTab()
        {
            optionsHolder.gameObject.SetActive(false);
            foreach (var item in inventoryItems)
            {
                if (item.gameObject.activeInHierarchy)
                {
                    item.thisButton.interactable = false;
                }
            }
            UpdateInventory();
            SelectInventoryButton();
            _previousState = _inventoryState;
            _inventoryState = InventoryState.Animating;
            var buttonIndex = _eventSystem.currentSelectedGameObject.transform.GetSiblingIndex();
            StartCoroutine(UpdateScrollBar(buttonIndex));
        }

        #region ScrollBar

        public IEnumerator UpdateScrollBar(int buttonIndex)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            scrollbar.value = 1;
            int amountOfItems = 0;
            switch (currentTab)
            {
                case InventoryTabs.Consumable:
                    amountOfItems = InventoryManager.a.consumableList.Count;
                    break;
                case InventoryTabs.BattleItems:
                    amountOfItems = InventoryManager.a.battleItemList.Count;
                    break;
                case InventoryTabs.Armor:
                    amountOfItems = InventoryManager.a.armorList.Count;
                    break;
                case InventoryTabs.Weapons:
                    amountOfItems = InventoryManager.a.weaponList.Count;
                    break;
                case InventoryTabs.KeyItems:
                    amountOfItems = InventoryManager.a.keyItemList.Count;
                    break;
            }
            ScrollDown(buttonIndex ,amountOfItems);
        }
        private void ScrollDown(int buttonIndex, int amountOfItems)
        {
            _scrollbarStep = 0;
            if (amountOfItems <= 7) // not out view
            {
                scrollbar.numberOfSteps = 1;
                scrollbar.size = 1;
                return;
            }
            else
            {
                scrollbar.numberOfSteps = amountOfItems - 6;
            }

            int x = buttonIndex - (6);
            if (x > 0)
            {
                for (int i = 0; i < x; i++)
                {
                    scrollbar.xMoveDown();
                    _scrollbarStep += 1;
                }
            }

        }

        private void CheckOutOfView(int buttonIndex)
        {
            //Check if scrollbar should move up.
            
            if(buttonIndex<_scrollbarStep)
            {
                scrollbar.xMoveUp();
                _scrollbarStep -= 1;
            }

            //Check if Scrollbar should move down.
            var x = buttonIndex - 6;
            if (x >_scrollbarStep)
            {
                scrollbar.xMoveDown();
                _scrollbarStep += 1;
            }
        }

        #endregion

        /// <summary>
        /// Gets call when the inventory panel gets show by Doozy UI.
        /// </summary>
        public void xOnShowView()
        {
            _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            UpdateInventory();
            SelectInventoryButton();
        }

        /// <summary>
        /// Populates the contents with the corresponding buttons and text labels.
        /// </summary>
        public void UpdateInventory()
        {
            switch (currentTab)
            {
                case InventoryTabs.Consumable:
                    PopulateConsumables();
                    tabText.text = "Consumables";
                    break;
                case InventoryTabs.BattleItems:
                    PopulateUsables();
                    tabText.text = "Battle Items";
                    break;
                case InventoryTabs.Armor:
                    PopulateArmor();
                    tabText.text = "Armor";
                    break;
                case InventoryTabs.Weapons:
                    PopulateWeapons();
                    tabText.text = "Weapons";
                    break;
                case InventoryTabs.KeyItems:
                    PopulateKeyItems();
                    tabText.text = "Key Items";
                    break;
            }

            if (!inventoryItems[0].gameObject.activeSelf)
            {
                optionsHolder.gameObject.SetActive(false);
                _inventoryState = InventoryState.Item;
            }
        }

        #region Populate
        private void PopulateConsumables()
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (InventoryManager.a.consumableList.Count > i)
                {
                    if (InventoryManager.a.consumableList[i] != null)
                    {
                        inventoryItems[i].gameObject.SetActive(true);
                        inventoryItems[i].ammountText.text = InventoryManager.a.consumableList[i].amount.ToString();
                        inventoryItems[i].itemText.text = InventoryManager.a.consumableList[i].itemName;
                        if (InventoryManager.a.consumableList[i].icon != null)
                            inventoryItems[i].spriteToShow.sprite = InventoryManager.a.consumableList[i].icon;
                        continue;
                    }
                }
                inventoryItems[i].gameObject.SetActive(false);
            }
        }
        private void PopulateUsables()
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (InventoryManager.a.battleItemList.Count > i)
                {
                    if (InventoryManager.a.battleItemList[i] != null)
                    {
                        inventoryItems[i].gameObject.SetActive(true);
                        inventoryItems[i].ammountText.text = InventoryManager.a.battleItemList[i].amount.ToString();
                        inventoryItems[i].itemText.text = InventoryManager.a.battleItemList[i].itemName;
                        continue;
                    }
                }
                inventoryItems[i].gameObject.SetActive(false);
            }

        }
        private void PopulateArmor()
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (InventoryManager.a.armorList.Count > i)
                {
                    if (InventoryManager.a.armorList[i] != null)
                    {
                        inventoryItems[i].gameObject.SetActive(true);
                        inventoryItems[i].ammountText.text = InventoryManager.a.armorList[i].amount.ToString();
                        inventoryItems[i].itemText.text = InventoryManager.a.armorList[i].itemName;
                        continue;
                    }
                }
                inventoryItems[i].gameObject.SetActive(false);
            }

        }
        private void PopulateWeapons()
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (InventoryManager.a.weaponList.Count > i)
                {
                    if (InventoryManager.a.weaponList[i] != null)
                    {
                        inventoryItems[i].gameObject.SetActive(true);
                        inventoryItems[i].ammountText.text = InventoryManager.a.weaponList[i].amount.ToString();
                        inventoryItems[i].itemText.text = InventoryManager.a.weaponList[i].itemName;
                        continue;
                    }
                }
                inventoryItems[i].gameObject.SetActive(false);
            }

        }
        private void PopulateKeyItems()
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (InventoryManager.a.keyItemList.Count > i)
                {
                    if (InventoryManager.a.keyItemList[i] != null)
                    {
                        inventoryItems[i].gameObject.SetActive(true);
                        inventoryItems[i].ammountText.text = InventoryManager.a.keyItemList[i].amount.ToString();
                        inventoryItems[i].itemText.text = InventoryManager.a.keyItemList[i].itemName;
                        continue;
                    }
                }
                inventoryItems[i].gameObject.SetActive(false);
            }
            if (_lastKeyItemButton != null)
                _eventSystem.SetSelectedGameObject(_lastKeyItemButton.gameObject);
            else _eventSystem.SetSelectedGameObject(inventoryItems[0].gameObject);
        }
        #endregion

        /// <summary>
        /// Selects the last button selected from each tab. If none the first one will be selected.
        /// </summary>
        private void SelectInventoryButton()
        {
            foreach (var item in inventoryItems)
            {
                item.thisButton.interactable = true;
                item.thisButton.OnDeselect(null);
            }
            _eventSystem.Deselect();
            switch (currentTab)
            {
                case InventoryTabs.Consumable:
                    if (_lastConsumableButton != null)
                        _eventSystem.SetSelectedGameObject(_lastConsumableButton.gameObject);
                    else _eventSystem.SetSelectedGameObject(inventoryItems[0].gameObject);
                    break;
                case InventoryTabs.BattleItems:
                    if (_lastUsableButton != null)
                        _eventSystem.SetSelectedGameObject(_lastUsableButton.gameObject);
                    else _eventSystem.SetSelectedGameObject(inventoryItems[0].gameObject);
                    break;
                case InventoryTabs.Armor:
                    if (_lastArmorButton != null)
                        _eventSystem.SetSelectedGameObject(_lastArmorButton.gameObject);
                    else _eventSystem.SetSelectedGameObject(inventoryItems[0].gameObject);
                    break;
                case InventoryTabs.Weapons:
                    if (_lastWeaponButton != null)
                        _eventSystem.SetSelectedGameObject(_lastWeaponButton.gameObject);
                    else _eventSystem.SetSelectedGameObject(inventoryItems[0].gameObject);
                    break;
                case InventoryTabs.KeyItems:
                    if (_lastKeyItemButton != null)
                        _eventSystem.SetSelectedGameObject(_lastKeyItemButton.gameObject);
                    else _eventSystem.SetSelectedGameObject(inventoryItems[0].gameObject);
                    break;
            }
        }


        /// <summary>
        /// Gets call when a Inventory button gets selected. 
        /// </summary>
        /// <param name="inventoryButton"></param>
        public void xOnSelectedInventoryButton(InventoryButton inventoryButton)
        {
            foreach (var item in inventoryItems)
            {
                item.thisButton.interactable = true;
            }

            switch (currentTab)
            {
                case InventoryTabs.Consumable:
                    _lastConsumableButton = inventoryButton;
                    break;
                case InventoryTabs.BattleItems:
                    _lastUsableButton = inventoryButton;
                    break;
                case InventoryTabs.Armor:
                    _lastArmorButton = inventoryButton;
                    break;
                case InventoryTabs.Weapons:
                    _lastWeaponButton = inventoryButton;
                    break;
                case InventoryTabs.KeyItems:
                    _lastKeyItemButton = inventoryButton;
                    break;
            }

            optionsHolder.gameObject.SetActive(false);
            scrollbar.interactable = false;

            CheckOutOfView(inventoryButton.transform.GetSiblingIndex());
        }

        /// <summary>
        /// Gets call when a Inventory button gets clicked. Open the options next to the button. 
        /// </summary>
        /// <param name="inventoryButton"></param>
        public void xClickInventoryButton(InventoryButton inventoryButton)
        {
            foreach (var item in inventoryItems)
            {
                item.thisButton.interactable = true;
            }
            inventoryButton.thisButton.interactable = false;
            _inventoryState = InventoryState.Options;
            ShowOptions(inventoryButton.transform.GetSiblingIndex());
        }
        private void ShowOptions(int index)
        {
            optionsHolder.transform.position = inventoryItems[index].transform.position;

            optionsHolder.gameObject.SetActive(true);
            scrollbar.interactable = false;

            useConsumableButton.gameObject.SetActive(false);
            equipItemButton.gameObject.SetActive(false);
            infoButton.gameObject.SetActive(false);
            dropButton.gameObject.SetActive(false);
            useKeyItemButton.gameObject.SetActive(false);

            _index = index;

            switch (currentTab)
            {
                case InventoryTabs.Consumable:

                    useConsumableButton.gameObject.SetActive(true);
                    Utilities.SetUpButtonNavigation(useConsumableButton, dropButton, infoButton, null, null);

                    infoButton.gameObject.SetActive(true);
                    Utilities.SetUpButtonNavigation(infoButton, useConsumableButton, dropButton, null, null);

                    dropButton.gameObject.SetActive(true);
                    Utilities.SetUpButtonNavigation(dropButton, infoButton, useConsumableButton, null, null);

                    _eventSystem.SetSelectedGameObject(useConsumableButton.gameObject);

                    break;
                case InventoryTabs.BattleItems:

                    infoButton.gameObject.SetActive(true);
                    Utilities.SetUpButtonNavigation(infoButton, dropButton, dropButton, null, null);

                    dropButton.gameObject.SetActive(true);
                    Utilities.SetUpButtonNavigation(dropButton, infoButton, infoButton, null, null);

                    _eventSystem.SetSelectedGameObject(infoButton.gameObject);

                    break;
                case InventoryTabs.Armor:

                    equipItemButton.gameObject.SetActive(true);
                    Utilities.SetUpButtonNavigation(equipItemButton, dropButton, infoButton, null, null);

                    infoButton.gameObject.SetActive(true);
                    Utilities.SetUpButtonNavigation(infoButton, equipItemButton, dropButton, null, null);;

                    dropButton.gameObject.SetActive(true);
                    Utilities.SetUpButtonNavigation(dropButton, infoButton, equipItemButton, null, null);

                    _eventSystem.SetSelectedGameObject(equipItemButton.gameObject);

                    break;
                case InventoryTabs.Weapons:

                    equipItemButton.gameObject.SetActive(true);
                    Utilities.SetUpButtonNavigation(equipItemButton, dropButton, infoButton, null, null);

                    infoButton.gameObject.SetActive(true);
                    Utilities.SetUpButtonNavigation(infoButton, equipItemButton, dropButton, null, null);

                    dropButton.gameObject.SetActive(true);
                    Utilities.SetUpButtonNavigation(dropButton, infoButton, equipItemButton, null, null);

                    _eventSystem.SetSelectedGameObject(equipItemButton.gameObject);
                    break;
                case InventoryTabs.KeyItems:

                    useKeyItemButton.gameObject.SetActive(true);
                    Utilities.SetUpButtonNavigation(useKeyItemButton, infoButton, infoButton, null, null);

                    infoButton.gameObject.SetActive(true);
                    Utilities.SetUpButtonNavigation(infoButton, useKeyItemButton, useKeyItemButton, null, null);

                    _eventSystem.SetSelectedGameObject(useKeyItemButton.gameObject);
                    break;
            }
        }

        public void xRememberOptionButton(Button lastOptionButton)
        {
            _lastOptionButton = lastOptionButton;
        }

        private void ItemUsed()
        {
            UpdateInventory();
            switch (currentTab)
            {
                case InventoryTabs.Consumable:
                    int amountOfConsumable = InventoryManager.a.consumableList.Count;
                    if (amountOfConsumable - 1 >= _index)
                        _eventSystem.SetSelectedGameObject(inventoryItems[_index].gameObject);
                    else
                    {
                        _eventSystem.SetSelectedGameObject(inventoryItems[Mathf.Clamp(_index - 1, 0, 20)].gameObject);
                        _scrollbarStep -= 1;
                    }
                    if (amountOfConsumable <= 7)
                        scrollbar.numberOfSteps = 1;
                    else scrollbar.numberOfSteps = amountOfConsumable - 6;
                    break;
                case InventoryTabs.BattleItems:
                    int amountOfBattleItems = InventoryManager.a.battleItemList.Count;
                    if (amountOfBattleItems - 1 >= _index)
                        _eventSystem.SetSelectedGameObject(inventoryItems[_index].gameObject);
                    else _eventSystem.SetSelectedGameObject(inventoryItems[Mathf.Clamp(_index - 1, 0, 20)].gameObject);
                    if (amountOfBattleItems <= 7)
                        scrollbar.numberOfSteps = 1;
                    else scrollbar.numberOfSteps = amountOfBattleItems - 6;
                    break;
                case InventoryTabs.Armor:
                    int amountOfArmor = InventoryManager.a.armorList.Count;
                    if (amountOfArmor - 1 >= _index)
                        _eventSystem.SetSelectedGameObject(inventoryItems[_index].gameObject);
                    else _eventSystem.SetSelectedGameObject(inventoryItems[Mathf.Clamp( _index - 1,0,20)].gameObject);
                    if (amountOfArmor <= 7)
                        scrollbar.numberOfSteps = 1;
                    else scrollbar.numberOfSteps = amountOfArmor - 6;
                    break;
                case InventoryTabs.Weapons:
                    int amountOfWeapon = InventoryManager.a.weaponList.Count;
                    if (amountOfWeapon - 1 >= _index)
                        _eventSystem.SetSelectedGameObject(inventoryItems[_index].gameObject);
                    else _eventSystem.SetSelectedGameObject(inventoryItems[Mathf.Clamp(_index - 1, 0, 20)].gameObject);
                    if (amountOfWeapon <= 7)
                        scrollbar.numberOfSteps = 1;
                    else scrollbar.numberOfSteps = amountOfWeapon - 6;
                    break;
                case InventoryTabs.KeyItems:
                    Debug.LogError("Trying to use a key item");
                    break;
            }
            _inventoryState = InventoryState.Item;
        }

        public void xUseConsumable()
        {
            _popupToShow = UIPopup.GetPopup("PlayerTarget");
            var x = _popupToShow.Data;
            _consumableToUse = null;

            _consumableToUse = InventoryManager.a.consumableList[_index];

            if (GameManager.a.player1 != null)
            {
                x.Buttons[0].SetLabelText(GameManager.a.player1.nombre);
                Utilities.SetUpButtonNavigation(x.Buttons[3].Button, x.Buttons[0].Button, x.Buttons[0].Button, null, null);
            }
            else Debug.LogError("Missing player1");
            if (GameManager.a.player2 != null)
            {
                _popupToShow.Data.Buttons[1].SetLabelText(GameManager.a.player2.nombre);
                Utilities.SetUpButtonNavigation(x.Buttons[0].Button, x.Buttons[3].Button, x.Buttons[1].Button, null, null);
                Utilities.SetUpButtonNavigation(x.Buttons[3].Button, x.Buttons[1].Button, x.Buttons[0].Button, null, null);
            }
            else
            {
                _popupToShow.Data.Buttons[1].gameObject.SetActive(false);
                Utilities.SetUpButtonNavigation(x.Buttons[0].Button, x.Buttons[3].Button, x.Buttons[3].Button, null, null);
            }
            if (GameManager.a.player3 != null)
            {
                _popupToShow.Data.Buttons[2].SetLabelText(GameManager.a.player3.nombre);
                Utilities.SetUpButtonNavigation(x.Buttons[1].Button, x.Buttons[0].Button, x.Buttons[2].Button, null, null);
                Utilities.SetUpButtonNavigation(x.Buttons[2].Button, x.Buttons[1].Button, x.Buttons[3].Button, null, null);
                Utilities.SetUpButtonNavigation(x.Buttons[3].Button, x.Buttons[2].Button, x.Buttons[0].Button, null, null);
            }
            else
            {
                _popupToShow.Data.Buttons[2].gameObject.SetActive(false);
                Utilities.SetUpButtonNavigation(x.Buttons[1].Button, x.Buttons[0].Button, x.Buttons[3].Button, null, null);
            }

            _popupToShow.AutoSelectPreviouslySelectedButtonAfterHide = false;
            _popupToShow.Data.SetButtonsCallbacks(UseConsumableOnPlayer1, UseConsumableOnPlayer2, UseConsumableOnPlayer3);
            _inventoryState = InventoryState.Popup;
            _popupToShow.Show();
        }

        #region UseConsumable
        private void UseConsumableOnPlayer1()
        {
            GameManager.a.player1.UseConsumableOutOfCombat(_consumableToUse);
            
            ItemUsed();
        }
        private void UseConsumableOnPlayer2()
        {
            GameManager.a.player1.UseConsumableOutOfCombat(_consumableToUse);

            ItemUsed();
        }
        private void UseConsumableOnPlayer3()
        {
            GameManager.a.player1.UseConsumableOutOfCombat(_consumableToUse);

            ItemUsed();
        }
        #endregion  // not finished

        public void xEquipItem()
        {
            switch (currentTab)
            {
                case InventoryTabs.Armor:
                    if (InventoryManager.a.armorList[_index].armorType == ArmorType.Chest)
                        EquipChest();
                    else EquipHelmet();
                    break;
                case InventoryTabs.Weapons:
                    EquipWeapon();
                    break;
                default:
                    Debug.LogError("trying to equip something that isnt a armor nor weapon item.");
                    break;
            }
        }

        private void EquipWeapon()
        {
            _popupToShow = UIPopup.GetPopup("PlayerTarget");
            var x = _popupToShow.Data;
            _weaponToEquip = InventoryManager.a.weaponList[_index];
            if (GameManager.a.player1 != null)
            {
                x.Buttons[0].SetLabelText(GameManager.a.player1.nombre);
                Utilities.SetUpButtonNavigation(x.Buttons[3].Button, x.Buttons[0].Button, x.Buttons[0].Button, null, null);
            }
            else Debug.LogError("Missing player1");
            if (GameManager.a.player2 != null)
            {
                _popupToShow.Data.Buttons[1].SetLabelText(GameManager.a.player2.nombre);
                Utilities.SetUpButtonNavigation(x.Buttons[0].Button, x.Buttons[3].Button, x.Buttons[1].Button, null, null);
                Utilities.SetUpButtonNavigation(x.Buttons[3].Button, x.Buttons[1].Button, x.Buttons[0].Button, null, null);
            }
            else
            {
                _popupToShow.Data.Buttons[1].gameObject.SetActive(false);
                Utilities.SetUpButtonNavigation(x.Buttons[0].Button, x.Buttons[3].Button, x.Buttons[3].Button, null, null);
            }
            if (GameManager.a.player3 != null)
            {
                _popupToShow.Data.Buttons[2].SetLabelText(GameManager.a.player3.nombre);
                Utilities.SetUpButtonNavigation(x.Buttons[1].Button, x.Buttons[0].Button, x.Buttons[2].Button, null, null);
                Utilities.SetUpButtonNavigation(x.Buttons[2].Button, x.Buttons[1].Button, x.Buttons[3].Button, null, null);
                Utilities.SetUpButtonNavigation(x.Buttons[3].Button, x.Buttons[2].Button, x.Buttons[0].Button, null, null);
            }
            else
            {
                _popupToShow.Data.Buttons[2].gameObject.SetActive(false);
                Utilities.SetUpButtonNavigation(x.Buttons[1].Button, x.Buttons[0].Button, x.Buttons[3].Button, null, null);
            }

            _popupToShow.AutoSelectPreviouslySelectedButtonAfterHide = false;
            _popupToShow.Data.SetButtonsCallbacks(EquipWeaponOnPlayer1, EquipWeaponOnPlayer2, EquipWeaponOnPlayer3);
            _inventoryState = InventoryState.Popup;
            _popupToShow.Show();

        }

        #region EquipWeapon
        private void EquipWeaponOnPlayer1()
        {
            InventoryManager.a.EquipWeaponFromInventory(GameManager.a.player1, _weaponToEquip);
            ItemUsed();
        }
        private void EquipWeaponOnPlayer2()
        {
            InventoryManager.a.EquipWeaponFromInventory(GameManager.a.player2, _weaponToEquip);
            ItemUsed();
        }
        private void EquipWeaponOnPlayer3()
        {
            InventoryManager.a.EquipWeaponFromInventory(GameManager.a.player3, _weaponToEquip);
            ItemUsed();
        }
        #endregion

        private void EquipHelmet()
        {
            _popupToShow = UIPopup.GetPopup("PlayerTarget");
            var x = _popupToShow.Data;
            var helmetList = InventoryManager.a.GiveHelmetList();
            _helmetToEquip = helmetList[_index];
            if (GameManager.a.player1 != null)
            {
                x.Buttons[0].SetLabelText(GameManager.a.player1.nombre);
                Utilities.SetUpButtonNavigation(x.Buttons[3].Button, x.Buttons[0].Button, x.Buttons[0].Button, null, null);
            }
            else Debug.LogError("Missing player1");
            if (GameManager.a.player2 != null)
            {
                _popupToShow.Data.Buttons[1].SetLabelText(GameManager.a.player2.nombre);
                Utilities.SetUpButtonNavigation(x.Buttons[0].Button, x.Buttons[3].Button, x.Buttons[1].Button, null, null);
                Utilities.SetUpButtonNavigation(x.Buttons[3].Button, x.Buttons[1].Button, x.Buttons[0].Button, null, null);
            }
            else
            {
                _popupToShow.Data.Buttons[1].gameObject.SetActive(false);
                Utilities.SetUpButtonNavigation(x.Buttons[0].Button, x.Buttons[3].Button, x.Buttons[3].Button, null, null);
            }
            if (GameManager.a.player3 != null)
            {
                _popupToShow.Data.Buttons[2].SetLabelText(GameManager.a.player3.nombre);
                Utilities.SetUpButtonNavigation(x.Buttons[1].Button, x.Buttons[0].Button, x.Buttons[2].Button, null, null);
                Utilities.SetUpButtonNavigation(x.Buttons[2].Button, x.Buttons[1].Button, x.Buttons[3].Button, null, null);
                Utilities.SetUpButtonNavigation(x.Buttons[3].Button, x.Buttons[2].Button, x.Buttons[0].Button, null, null);
            }
            else
            {
                _popupToShow.Data.Buttons[2].gameObject.SetActive(false);
                Utilities.SetUpButtonNavigation(x.Buttons[1].Button, x.Buttons[0].Button, x.Buttons[3].Button, null, null);
            }

            _popupToShow.AutoSelectPreviouslySelectedButtonAfterHide = false;
            _popupToShow.Data.SetButtonsCallbacks(EquipHelmetOnPlayer1, EquipHelmetOnPlayer2, EquipHelmetOnPlayer3);
            _inventoryState = InventoryState.Popup;
            _popupToShow.Show();

        }

        #region EquipHelmet
        private void EquipHelmetOnPlayer1()
        {
            InventoryManager.a.EquipHelmetFromInventory(GameManager.a.player1 , _helmetToEquip);
            ItemUsed();
        }
        private void EquipHelmetOnPlayer2()
        {
            InventoryManager.a.EquipHelmetFromInventory(GameManager.a.player2, _helmetToEquip);
            ItemUsed();
        }
        private void EquipHelmetOnPlayer3()
        {
            InventoryManager.a.EquipHelmetFromInventory(GameManager.a.player3, _helmetToEquip);
            ItemUsed();
        }
        #endregion
        
        public void EquipChest()
        {
            _popupToShow = UIPopup.GetPopup("PlayerTarget");
            var x = _popupToShow.Data;
            var chestList = InventoryManager.a.GiveChestList();
            _chestToEquip = chestList[_index];
            if (GameManager.a.player1 != null)
            {
                x.Buttons[0].SetLabelText(GameManager.a.player1.nombre);
                Utilities.SetUpButtonNavigation(x.Buttons[3].Button, x.Buttons[0].Button, x.Buttons[0].Button, null, null);
            }
            else Debug.LogError("Missing player1");
            if (GameManager.a.player2 != null)
            {
                _popupToShow.Data.Buttons[1].SetLabelText(GameManager.a.player2.nombre);
                Utilities.SetUpButtonNavigation(x.Buttons[0].Button, x.Buttons[3].Button, x.Buttons[1].Button, null, null);
                Utilities.SetUpButtonNavigation(x.Buttons[3].Button, x.Buttons[1].Button, x.Buttons[0].Button, null, null);
            }
            else
            {
                _popupToShow.Data.Buttons[1].gameObject.SetActive(false);
                Utilities.SetUpButtonNavigation(x.Buttons[0].Button, x.Buttons[3].Button, x.Buttons[3].Button, null, null);
            }
            if (GameManager.a.player3 != null)
            {
                _popupToShow.Data.Buttons[2].SetLabelText(GameManager.a.player3.nombre);
                Utilities.SetUpButtonNavigation(x.Buttons[1].Button, x.Buttons[0].Button, x.Buttons[2].Button, null, null);
                Utilities.SetUpButtonNavigation(x.Buttons[2].Button, x.Buttons[1].Button, x.Buttons[3].Button, null, null);
                Utilities.SetUpButtonNavigation(x.Buttons[3].Button, x.Buttons[2].Button, x.Buttons[0].Button, null, null);
            }
            else
            {
                _popupToShow.Data.Buttons[2].gameObject.SetActive(false);
                Utilities.SetUpButtonNavigation(x.Buttons[1].Button, x.Buttons[0].Button, x.Buttons[3].Button, null, null);
            }

            _popupToShow.AutoSelectPreviouslySelectedButtonAfterHide = false;
            _popupToShow.Data.SetButtonsCallbacks(EquipChestOnPlayer1, EquipChestOnPlayer2, EquipChestOnPlayer3);
            _inventoryState = InventoryState.Popup;
            _popupToShow.Show();
        }

        #region EquipChest
        private void EquipChestOnPlayer1()
        {
            InventoryManager.a.EquipChestFromInventory(GameManager.a.player1, _chestToEquip);
            ItemUsed();
        }
        private void EquipChestOnPlayer2()
        {
            InventoryManager.a.EquipChestFromInventory(GameManager.a.player2, _chestToEquip);
            ItemUsed();
        }
        private void EquipChestOnPlayer3()
        {
            InventoryManager.a.EquipChestFromInventory(GameManager.a.player3, _chestToEquip);
            ItemUsed();
        }
        #endregion

        public void xItemInfo()
        {
            _inventoryState = InventoryState.Popup;
            switch (currentTab)
            {
                case InventoryTabs.Consumable:
                    _popupToShow = UIPopupManager.GetPopup(PopUpNames.ItemDescription.ToString());
                    _popupToShow.Data.SetLabelsTexts(InventoryManager.a.consumableList[_index].itemName, InventoryManager.a.consumableList[_index].description);
                    _popupToShow.AutoSelectPreviouslySelectedButtonAfterHide = true;
                    _popupToShow.Data.SetButtonsCallbacks(() => _inventoryState = InventoryState.Options);
                    _popupToShow.Show();
                    break;
                case InventoryTabs.BattleItems:
                    _popupToShow = UIPopupManager.GetPopup(PopUpNames.ItemDescription.ToString());
                    _popupToShow.Data.SetLabelsTexts(InventoryManager.a.battleItemList[_index].itemName, InventoryManager.a.battleItemList[_index].description);
                    _popupToShow.AutoSelectPreviouslySelectedButtonAfterHide = true;
                    _popupToShow.Data.SetButtonsCallbacks(() => _inventoryState = InventoryState.Options);
                    _popupToShow.Show();
                    break;
                case InventoryTabs.Armor:
                    _popupToShow = UIPopupManager.GetPopup(PopUpNames.GearInfo.ToString());
                    int amountOfLabels = _popupToShow.Data.Labels.Count;
                    Armor armorToShowInfo = InventoryManager.a.armorList[_index];
                    if (armorToShowInfo.armorType == ArmorType.Helmet)
                    {
                        _popupToShow.Data.SetLabelsTexts(GetHelmetLabels(amountOfLabels,armorToShowInfo));
                    }
                    else
                    {
                        _popupToShow.Data.SetLabelsTexts(GetChestLabels(amountOfLabels, armorToShowInfo));
                    }
                    _popupToShow.AutoSelectPreviouslySelectedButtonAfterHide = true;
                    _popupToShow.Data.SetButtonsCallbacks(() => _inventoryState = InventoryState.Options);
                    _popupToShow.Show();
                    break;
                case InventoryTabs.Weapons:
                    _popupToShow = UIPopupManager.GetPopup(PopUpNames.GearInfo.ToString());
                    Weapon weaponToShow = InventoryManager.a.weaponList[_index];
                    _popupToShow.Data.SetLabelsTexts(GetWeaponLabels(_popupToShow.Data.Labels.Count, weaponToShow));
                    _popupToShow.AutoSelectPreviouslySelectedButtonAfterHide = true;
                    _popupToShow.Data.SetButtonsCallbacks(() => _inventoryState = InventoryState.Options);
                    _popupToShow.Show();
                    break;
                case InventoryTabs.KeyItems:
                    _popupToShow = UIPopupManager.GetPopup(PopUpNames.ItemDescription.ToString());
                    _popupToShow.Data.SetLabelsTexts(InventoryManager.a.keyItemList[_index].itemName, InventoryManager.a.keyItemList[_index].description);
                    _popupToShow.AutoSelectPreviouslySelectedButtonAfterHide = true;
                    _popupToShow.Data.SetButtonsCallbacks(() => _inventoryState = InventoryState.Options);
                    _popupToShow.Show();
                    break;
            }
        }

        private string[] GetHelmetLabels(int amountOfLabels, Armor helmet)
        {
            List<string> labelsText = new List<string>();
            if (helmet.armorType != ArmorType.Helmet)
            {
                Debug.LogError("Try to to ger info of a helmet using a chest");
            }

            labelsText.Add(helmet.itemName);
            labelsText.Add(helmet.description);
            labelsText.Add("P.Defense");
            labelsText.Add(helmet.addPDefense.ToString());
            labelsText.Add("M.Defense");
            labelsText.Add(helmet.addMDefense.ToString());
            labelsText.Add("Confusion Inm.");
            if (helmet.inmuneConfusion)
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

        private string[] GetChestLabels(int amountOfLabels, Armor chest)
        {
            List<string> labelsText = new List<string>();
            if (chest.armorType != ArmorType.Chest)
            {
                Debug.LogError("Try to to ger info of a chest using a helmet");
            }

            labelsText.Add(chest.itemName);
            labelsText.Add(chest.description);
            labelsText.Add("P.Defense");
            labelsText.Add(chest.addPDefense.ToString());
            labelsText.Add("M.Defense");
            labelsText.Add(chest.addMDefense.ToString());
            labelsText.Add("Confusion Inm.");
            if (chest.inmuneConfusion)
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

        private string[] GetWeaponLabels(int amountOfLabels, Weapon weapon)
        {
            List<string> labelsText = new List<string>();

            labelsText.Add(weapon.itemName);
            labelsText.Add(weapon.description);
            labelsText.Add("P.Damage");
            labelsText.Add(weapon.addPDamage.ToString());
            labelsText.Add("M.Damage");
            labelsText.Add(weapon.addMDamage.ToString());
            labelsText.Add("Health");
            labelsText.Add(weapon.addHealth.ToString());
            labelsText.Add("Mana");
            labelsText.Add(weapon.addMana.ToString());

            int x = labelsText.Count;
            for (int i = 0; i < amountOfLabels - x; i++)
            {
                labelsText.Add("");
            }
            var a = labelsText.ToArray();
            return a;
        }

        public void xDropItem()
        {
            switch (currentTab)
            {
                case InventoryTabs.Consumable:
                    InventoryManager.a.DropConsumable(InventoryManager.a.consumableList[_index]);
                    ItemUsed();
                    break;
                case InventoryTabs.BattleItems:
                    InventoryManager.a.DropBattleItem(InventoryManager.a.battleItemList[_index]);
                    ItemUsed();
                    break;
                case InventoryTabs.Armor:
                    InventoryManager.a.DropArmor(InventoryManager.a.armorList[_index]);
                    ItemUsed();
                    break;
                case InventoryTabs.Weapons:
                    InventoryManager.a.DropWeapon(InventoryManager.a.weaponList[_index]);
                    ItemUsed();
                    break;
                case InventoryTabs.KeyItems:
                    Debug.LogError("Trying to drop a Key Item");
                    break;
            }
        }
    }
}