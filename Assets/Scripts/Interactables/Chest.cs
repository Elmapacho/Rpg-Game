using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

namespace ElMapacho
{
    public class Chest : MonoBehaviour, IInteractable
    {
        public Usable usable;

        public LootConsumable consumableToGive;
        public LootBattleItem battleItemToGive;
        public Armor armorToLoot;
        public Weapon weaponToLoot;
        public KeyItem keyItemToLoot;

        void Start()
        {
            usable = GetComponent<Usable>();
        }

        public void Interact()
        {
            
            CheckIfEmpty();

            CheckConsumables();
            CheckBattleItem();
            CheckArmor();
            CheckWeapon();
            CheckKeyItem();

            usable.events.onUse?.Invoke();
        }

        void CheckConsumables()
        {
            if (consumableToGive.consumableToLoot == null)
            {
                ChangeObjectName("Consumable", ""); // no consumalbe dialogue
                return;
            }
            ChangeObjectName("Consumable", consumableToGive.consumableToLoot.itemName);
            ChangeObjectBool("ConsumableType", true);

            int availableAmountSpace = InventoryManager.a.CheckConsumableAmount(consumableToGive.consumableToLoot, consumableToGive.amount);
            if (availableAmountSpace < 0) //need to add a new consumable
            {
                if (!InventoryManager.a.CheckConsumableCapacity()) // cant be added
                {
                    ChangeObjectBool("ConsumableType", false);
                    return;
                }
                else // added
                {
                    ChangeObjectNumber("ConsumableAmount", consumableToGive.amount);
                    InventoryManager.a.AddConsumable(consumableToGive.consumableToLoot, consumableToGive.amount);
                    consumableToGive.consumableToLoot = null;
                }
            }
            if (availableAmountSpace == 0) // cant carry more
            {
                ChangeObjectNumber("ConsumableAmount", -999);
            }
            else if (availableAmountSpace > 0) // add to an existing amount
            {
                ChangeObjectNumber("ConsumableAmount", consumableToGive.amount);
                InventoryManager.a.AddConsumable(consumableToGive.consumableToLoot, consumableToGive.amount);
                consumableToGive.consumableToLoot = null;
            }
        }

        void CheckBattleItem()
        {
            if (battleItemToGive.battleItemToLoot == null)
            {
                ChangeObjectName("BattleItem", ""); // no dialogue
                return;
            }
            ChangeObjectName("BattleItem", battleItemToGive.battleItemToLoot.itemName);
            ChangeObjectBool("BattleItemType", true);

            int availableAmountSpace = InventoryManager.a.CheckBattleItemAmount(battleItemToGive.battleItemToLoot, battleItemToGive.amount);
            if (availableAmountSpace < 0) //need to add a new battle item
            {
                if(InventoryManager.a.CheckBattleItemCapacity()) // can be added
                {
                    ChangeObjectNumber("BattleItemAmount", battleItemToGive.amount);
                    InventoryManager.a.AddBattleItem(battleItemToGive.battleItemToLoot, battleItemToGive.amount);
                    battleItemToGive.battleItemToLoot = null;
                }
                else // cant added
                {
                    ChangeObjectBool("BattleItemType", false);
                }
            }
            if (availableAmountSpace ==0 ) // cant carry more
            {
                ChangeObjectNumber("BattleItemAmount", -999);
            }
            else if (availableAmountSpace >0) // add to an existing amount
            {
                ChangeObjectNumber("BattleItemAmount", battleItemToGive.amount);
                InventoryManager.a.AddBattleItem(battleItemToGive.battleItemToLoot, battleItemToGive.amount);
                battleItemToGive.battleItemToLoot = null;
            }

        }

        void CheckArmor()
        {
            if (armorToLoot == null)
            {
                ChangeObjectName("Armor", ""); // no consumalbe dialogue
                return;
            }
            ChangeObjectName("Armor", armorToLoot.itemName);
            ChangeObjectBool("ArmorType", true);

            if (InventoryManager.a.CheckArmorCapacity())
            {
                InventoryManager.a.AddArmor(armorToLoot);
                armorToLoot = null;
            }
            else
            {
                ChangeObjectBool("ArmorType", false);
            }
        }
        void CheckWeapon()
        {
            if (weaponToLoot == null)
            {
                ChangeObjectName("Weapon", ""); // no weapon dialogue
                return;
            }
            ChangeObjectName("Weapon", weaponToLoot.itemName);
            ChangeObjectBool("WeaponType", true);

            if (InventoryManager.a.CheckWeaponCapacity())
            {
                InventoryManager.a.AddWeapon(weaponToLoot);
                weaponToLoot = null;
            }
            else
            {
                ChangeObjectBool("WeaponType", false);
            }

        }
        void CheckKeyItem()
        {
            if (keyItemToLoot == null)
            {
                ChangeObjectName("KeyItem", ""); // no keyitem dialogue
                return;
            }
            ChangeObjectName("KeyItem", keyItemToLoot.itemName);

            if (InventoryManager.a.CheckKeyItemCapacity())
            {
                InventoryManager.a.AddKeyItem(keyItemToLoot);
                keyItemToLoot = null;
            }
        }
        void CheckIfEmpty()
        {
            if (armorToLoot ==null)
            {
                if (consumableToGive.consumableToLoot == null)
                {
                    if (keyItemToLoot == null)
                    {
                        if (battleItemToGive.battleItemToLoot == null)
                            if (weaponToLoot == null)
                            {
                                DialogueLua.SetActorField("Chest", "IsEmpty", true);
                                return;
                            }
                    }
                }
            }
            ChangeObjectBool("IsEmpty", false);
        }

        void ChangeObjectName (string variableName, string nameToChange)
        {
            DialogueLua.SetActorField("Chest", variableName, nameToChange);
        }
        void ChangeObjectNumber (string variableName , int amount )
        {
            DialogueLua.SetActorField("Chest", variableName, amount);
        }
        void ChangeObjectBool(string variableName , bool boolean)
        {
            DialogueLua.SetActorField("Chest", variableName, boolean);
        }
    }
}