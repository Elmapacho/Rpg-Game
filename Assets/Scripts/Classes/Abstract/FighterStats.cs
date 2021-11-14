using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
    public class FighterStats : MonoBehaviour
    {
        public string nombre;

        [Header("Stats")]
        public StatsPerLevel statsPerLevel;
        public Stats baseStats;
        public Stats derivedStats;
        public List<Buff> activeBuff;
        public List<Debuff> activeDebuff;
        public int level;
        public int orderSpeed;
        public int exp;

        public bool IsPlayer = false;
        public int minLevel;
        public int maxLevel;


        public Armor helmetEquipped;
        public Armor chestEquipped;
        public Weapon weaponEquipped;

        public int flatMArmor;
        public int flatPArmor;
        public int reduceHealing;

        private void Start()
        {
            UpdateStats();
            derivedStats.currentHp  = derivedStats.maxHp;
            derivedStats.currentMana  = derivedStats.maxMana;
        }
        public void SetBaseStats()
        {
            baseStats.maxHp = statsPerLevel.health * level;
            baseStats.maxMana = statsPerLevel.mana * level;
            baseStats.pAttack = statsPerLevel.pAttack * level;
            baseStats.mAttack = statsPerLevel.mAttack * level;
            baseStats.pDefense = statsPerLevel.pDefense * level;
            baseStats.mDefense = statsPerLevel.mDefense * level;
        }

        public void SetDerivedStats()
        {
            derivedStats.maxHp = baseStats.maxHp;
            derivedStats.maxMana = baseStats.maxMana;
            derivedStats.pAttack = baseStats.pAttack;
            derivedStats.mAttack = baseStats.mAttack;
            derivedStats.pDefense = baseStats.pDefense;
            derivedStats.mDefense = baseStats.mDefense;
        }

        public void UpdateStats()
        {
            SetBaseStats();
            SetDerivedStats();

            if (IsPlayer) AddEquipmentStats();

            AddBuffStats();
            AddDebuffStats();
        }

        public void CalculateDerivedStats() 
        {
            if (!IsPlayer)
            {
                level = Random.Range(minLevel, maxLevel + 1);
            }
            SetBaseStats(); // unneccesary?
            SetDerivedStats();

            if (IsPlayer) AddEquipmentStats();

            AddBuffStats();
            AddDebuffStats();
        }

        void AddEquipmentStats()
        {
            if (helmetEquipped != null)
            {
                derivedStats.pDefense += helmetEquipped.addPDefense;
                derivedStats.mDefense += helmetEquipped.addMDefense;

            }
            if (chestEquipped != null)
            {
                derivedStats.pDefense += chestEquipped.addPDefense;
                derivedStats.mDefense += chestEquipped.addMDefense;

            }
            if (weaponEquipped != null)
            {
                derivedStats.pAttack += weaponEquipped.addPDamage;
                derivedStats.mAttack += weaponEquipped.addMDamage;
                derivedStats.maxHp += weaponEquipped.addHealth;
                derivedStats.maxMana += weaponEquipped.addMana;
            }

        }
        void AddBuffStats()
        {
            for (int i = 0; i < activeBuff.Count; i++)
            {
                switch (activeBuff[i].type)
                {
                    case StatType.PATTACK:
                        derivedStats.pAttack += baseStats.pAttack * activeBuff[i].porcentageAmount + activeBuff[i].flatAmount;
                        break;
                    case StatType.MATTACK:
                        derivedStats.mAttack += baseStats.mAttack * activeBuff[i].porcentageAmount + activeBuff[i].flatAmount;
                        break;
                    case StatType.PDEFENSE:
                        derivedStats.pDefense += baseStats.pDefense * activeBuff[i].porcentageAmount + activeBuff[i].flatAmount;
                        break;
                    case StatType.MDEFENSE:
                        derivedStats.mDefense += baseStats.mDefense * activeBuff[i].porcentageAmount + activeBuff[i].flatAmount;
                        break;
                }
            }
        }
        void AddDebuffStats()
        {
            for (int i = 0; i < activeDebuff.Count; i++)
            {
                switch (activeDebuff[i].type)
                {
                    case StatType.PATTACK:
                        derivedStats.pAttack -= baseStats.pAttack * activeDebuff[i].porcentageAmount + activeDebuff[i].flatAmount;
                        break;
                    case StatType.MATTACK:
                        derivedStats.mAttack -= baseStats.mAttack * activeDebuff[i].porcentageAmount + activeDebuff[i].flatAmount;
                        break;
                    case StatType.PDEFENSE:
                        derivedStats.pDefense -= baseStats.pDefense * activeDebuff[i].porcentageAmount + activeDebuff[i].flatAmount;
                        break;
                    case StatType.MDEFENSE:
                        derivedStats.mDefense -= baseStats.mDefense * activeDebuff[i].porcentageAmount + activeDebuff[i].flatAmount;
                        break;
                }
            }
        }

        public void LevelUp()
        {
            level += 1;
            CalculateDerivedStats();
            derivedStats.currentHp += statsPerLevel.health;
            derivedStats.maxMana += statsPerLevel.mana;
        }

        public void GiveExp(int amountToGive)
        {
            exp += amountToGive;
            // check if if the maount of exp is enough to level up
            // exp - exp needed for that level up
            LevelUp();

        }

        public void TickBuffs()
        {
            foreach (var buff in activeBuff)
            {
                buff.duration -= 1;
                if (buff.duration <= 0)
                    activeBuff.Remove(buff);
            }
        }

        public void TickDebuffs()
        {
            foreach (var debuff in activeBuff)
            {
                debuff.duration -= 1;
                if (debuff.duration <= 0)
                    activeBuff.Remove(debuff);
            }
        }

        public int CalculateTakeDamage(int physicalDamage, int magicDamage)
        {
            int damageTaken = 0;
            if (physicalDamage - derivedStats.pDefense > 0)
            {
                damageTaken += physicalDamage - (derivedStats.pDefense * SettingsManager.a.defenseDeminisher /100) - flatPArmor;
            }
            if (magicDamage - derivedStats.pDefense > 0)
            {
                damageTaken += magicDamage - (derivedStats.mDefense * SettingsManager.a.defenseDeminisher / 100) - flatMArmor;
            }
            return damageTaken;
        }

        public int CalculateReceiveHealing(int healingRecive)
        {
            int recivedHeling;
            recivedHeling = Mathf.RoundToInt(healingRecive * ((100 - reduceHealing) / 100));
            return recivedHeling;
        }

        public void UseConsumableOutOfCombat(Consumable consumableToUse)
        {

        }
    }
}