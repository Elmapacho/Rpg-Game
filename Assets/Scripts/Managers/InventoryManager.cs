using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ElMapacho
{
    public class InventoryManager : MonoBehaviour
    {
        public int consumableCapacity;
        public List<Consumable> consumableList = new List<Consumable>();
        public int usableCapacity;
        public List<BattleItem> battleItemList = new List<BattleItem>();
        public int weaponCapacity;
        public List<Weapon> weaponList = new List<Weapon>();
        public int armorCapacity;
        public List<Armor> armorList = new List<Armor>();
        public int keyItemCapacity;
        public List<KeyItem> keyItemList = new List<KeyItem>();

        [Space]
        [SerializeField] private Consumable _consumableToLoot;
        [SerializeField] private int _consumableAmountToLoot;
        [SerializeField] private BattleItem _battleItemToLoot;
        [SerializeField] private int _battleItemAmountToLoot;
        [SerializeField] private Weapon _weaponToLoot;
        [SerializeField] private Armor _armorToLoot;
        [SerializeField] private KeyItem _keyItemToLoot;

        #region SINGLETON PATTERN
        private static InventoryManager _instance;
        public static InventoryManager a { get { return _instance; } }
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
            DontDestroyOnLoad(this);
            Awake2();
        }
        #endregion


        private void Awake2()
        {
            for (int i = 0; i < consumableList.Count; i++)
            {
                var clone = Instantiate(consumableList[i]);
                consumableList[i] = clone;
            }
            for (int i = 0; i < battleItemList.Count; i++)
            {
                var clone = Instantiate(battleItemList[i]);
                battleItemList[i] = clone;
            }
            for (int i = 0; i < weaponList.Count; i++)
            {
                var clone = Instantiate(weaponList[i]);
                weaponList[i] = clone;
            }
            for (int i = 0; i < armorList.Count; i++)
            {
                var clone = Instantiate(armorList[i]);
                armorList[i] = clone;
            }
            for (int i = 0; i < keyItemList.Count; i++)
            {
                var clone = Instantiate(keyItemList[i]);
                keyItemList[i] = clone;
            }
        }

        public int CheckConsumableAmount(Consumable consumableToAdd, int amountToAdd)
        {
            Consumable existingConsumable = null;
            for (int i = 0; i < consumableList.Count; i++) // find consumable
            {
                if (consumableList[i].itemName == consumableToAdd.itemName)
                {
                    existingConsumable = consumableList[i];
                    break;
                }
            }
            if (existingConsumable == null ) // needs to check if can be added
            {
                return -999;
            }

            if (existingConsumable.amountStackable >= existingConsumable.amount + amountToAdd) // can add?
            {
                return 1;
            }
            else return 0;
        }
        public int CheckBattleItemAmount(BattleItem battleItemToAdd, int amountToAdd)
        {
            BattleItem existingBattleItem = null;
            for (int i = 0; i < battleItemList.Count; i++) // find battleItem
            {
                if (battleItemList[i].itemName == battleItemToAdd.itemName)
                {
                    existingBattleItem = battleItemList[i];
                    break;
                }
            }
            if (existingBattleItem == null) // needs to check if can be added
            {
                return -999;
            }

            if (existingBattleItem.amountStackable >= existingBattleItem.amount + amountToAdd) // can add?
            {
                return 1;
            }
            else return 0;
        }
        public bool CheckConsumableCapacity()
        {
            if (consumableList.Count < consumableCapacity)
                return true;
            else return false;
        }
        public bool CheckBattleItemCapacity()
        {
            if (battleItemList.Count < usableCapacity)
                return true;
            else return false;
        }
        public bool CheckArmorCapacity()
        {
            if (armorList.Count < armorCapacity)
                return true;
            else return false;
        }
        public bool CheckWeaponCapacity()
        {
            if (weaponList.Count < weaponCapacity)
                return true;
            else return false;
        }
        public bool CheckKeyItemCapacity()
        {
            if (keyItemList.Count < keyItemCapacity)
                return true;
            else return false;
        }

        public void AddConsumable(Consumable consumableToAdd, int amountToAdd)
        {
            for (int i = 0; i < consumableList.Count; i++)
            {
                if (consumableList[i].itemName == consumableToAdd.itemName)
                {
                    Debug.Log("Found a match.");
                    consumableList[i].amount += amountToAdd;
                    if (consumableList[i].amount > consumableList[i].amountStackable)
                    {
                        Debug.LogError(consumableList[i].itemName + " has excedeed the amount stackable");
                    }
                    return;
                }
            }

            if (consumableToAdd.name.Contains("(Clone)"))
            {
                consumableList.Add(consumableToAdd);
                consumableToAdd.amount = 1;
            }
            else
            {
                Debug.Log("Adding a new consumable");
                var clone = Instantiate(consumableToAdd);
                clone.amount = amountToAdd;
                consumableList.Add(clone);
            }
        }
        public void AddBattleItem(BattleItem battleItemToAdd, int amountToAdd)
        {
            for (int i = 0; i < battleItemList.Count; i++)
            {
                if (battleItemList[i].itemName == battleItemToAdd.itemName)
                {
                    Debug.Log("Found a match.");
                    battleItemList[i].amount += amountToAdd;
                    if (consumableList[i].amount > consumableList[i].amountStackable)
                    {
                        Debug.LogError(battleItemList[i].itemName + " has excedeed the amount stackable");
                    }
                    return;
                }
            }

            if (battleItemToAdd.name.Contains("(Clone)"))
            {
                battleItemList.Add(battleItemToAdd);
                battleItemToAdd.amount = 1;
            }
            else
            {
                Debug.Log("Adding a new battleItem");
                var clone = Instantiate(battleItemToAdd);
                clone.amount = amountToAdd;
                battleItemList.Add(clone);
            }
        }
        public void AddArmor(Armor armorToAdd)
        {
            if (armorToAdd.name.Contains("(Clone)"))
            {
                armorList.Add(armorToAdd);
                armorToAdd.amount = 1;
            }
            else
            {
                var clone = Instantiate(armorToAdd);
                armorList.Add(clone);
                clone.amount = 1;
            }
        }
        public void AddWeapon(Weapon weaponToAdd)
        {
            if (weaponToAdd.name.Contains("(Clone)"))
            {
                weaponList.Add(weaponToAdd);
                weaponToAdd.amount = 1;
            }
            else
            {
                var clone = Instantiate(weaponToAdd);
                weaponList.Add(clone);
                clone.amount = 1;
            }
        }
        public void AddKeyItem(KeyItem keyItemToAdd)
        {
            if (keyItemToAdd.name.Contains("(Clone)"))
            {
                keyItemList.Add(keyItemToAdd);
                keyItemToAdd.amount = 1;
            }
            else
            {
                var clone = Instantiate(keyItemToAdd);
                keyItemList.Add(clone);
                clone.amount = 1;
            }
        }

        public void DropConsumable(Consumable consumableToDrop)
        {
            consumableList.Remove(consumableToDrop);
        }
        public void DropBattleItem(BattleItem battleItemToDrop)
        {
            battleItemList.Remove(battleItemToDrop);
        }
        public void DropArmor(Armor armorToDrop)
        {
            if (armorToDrop.armorType == ArmorType.Helmet)
                DropHelmet(armorToDrop);
            else DropChest(armorToDrop);
        }
        public void DropHelmet(Armor helmetToDrop)
        {
            armorList.Remove(helmetToDrop);
        }
        public void DropChest(Armor weaponToDrop)
        {
            armorList.Remove(weaponToDrop);
        }
        public void DropWeapon(Weapon weaponToDrop)
        {
            weaponList.Remove(weaponToDrop);
        }

        public int AmountOfChest()
        {
            int amountOfChests = 0;
            foreach (var item in armorList)
            {
                if (item.armorType == ArmorType.Chest)
                {
                    amountOfChests += 1;
                }
            }
            return amountOfChests;
        }
        public int AmountOfHelmet()
        {
            int amountOfHelmets = 0;
            foreach (var item in armorList)
            {
                if (item.armorType == ArmorType.Helmet)
                {
                    amountOfHelmets += 1;
                }
            }
            return amountOfHelmets;
        }

        public List<Armor> GiveChestList()
        {
            var listOfChests = new List<Armor>();
            foreach (var item in armorList)
            {
                if (item.armorType == ArmorType.Chest && item != null)
                {
                    listOfChests.Add(item);
                }
            }
            return listOfChests;
        }
        public List<Armor> GiveHelmetList()
        {
            var listOfHelmets = new List<Armor>();
            foreach (var item in armorList)
            {
                if (item.armorType == ArmorType.Helmet && item != null)
                {
                    listOfHelmets.Add(item);
                }
            }
            return listOfHelmets;
        }

        public void EquipHelmetFromInventory(FighterStats playerToEquip, Armor helmetToEquip)
        {
            UnEquipHelmet(playerToEquip);
            if (helmetToEquip.name.Contains("(Clone)"))
            {
                playerToEquip.helmetEquipped = helmetToEquip;
                armorList.Remove(helmetToEquip);
            }
            else
            {
                var clone = Instantiate(helmetToEquip);
                playerToEquip.helmetEquipped = clone;
                armorList.Remove(helmetToEquip);
            }
        }
        public void UnEquipHelmet(FighterStats player)
        {
            if (player.helmetEquipped != null)
            {
                AddArmor(player.helmetEquipped);
                player.helmetEquipped = null;
            }
        }

        public void EquipChestFromInventory(FighterStats playerToEquip, Armor ChestToEquip)
        {
            UnEquipChest(playerToEquip);
            if (ChestToEquip.name.Contains("(Clone)"))
            {
                playerToEquip.chestEquipped = ChestToEquip;
                armorList.Remove(ChestToEquip);
            }
            else
            {
                var clone = Instantiate(ChestToEquip);
                playerToEquip.chestEquipped = clone;
                armorList.Remove(ChestToEquip);
            }
        }
        public void UnEquipChest(FighterStats player)
        {
            if (player.chestEquipped != null)
            {
                AddArmor(player.chestEquipped);
                player.chestEquipped = null;
            }
        }

        public void EquipWeaponFromInventory(FighterStats playerToEquip, Weapon weaponToEquip)
        {
            UnEquipWeapon(playerToEquip);
            if (weaponToEquip.name.Contains("(Clone)"))
            {
                playerToEquip.weaponEquipped = weaponToEquip;
                weaponList.Remove(weaponToEquip);
            }
            else
            {
                var clone = Instantiate(weaponToEquip);
                playerToEquip.weaponEquipped = clone;
                weaponList.Remove(weaponToEquip);
            }
        }
        public void UnEquipWeapon(FighterStats player)
        {
            if (player.weaponEquipped != null)
            {
                AddWeapon(player.weaponEquipped);
                player.weaponEquipped = null;
            }
        }
    }
}