using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ElMapacho
{
    public class Ability : MonoBehaviour
    {
        public string abilityName;
        [TextArea]
        public string description;

        [Header("Porcentages")]
        public int pDamageMultiplier;
        public int mDamageMultiplier;
        public int mHealingMultiplier;

        [Space]
        public bool canPoison;
        public int poisonChance;
        public bool canSleep;
        public int sleepChance;
        [Space]
        public int amountOfEnemiesToHit;
        public int amountOfPlayersToHit;
        public int coolDown;

        [Space]
        public FighterStats fighter;
        public int _maxScore;
        public int minScoreTreshHold;
        public List<BaseCondition> conditions = new List<BaseCondition>();
        public Dictionary<int, BaseCondition> scoreConditions = new Dictionary<int, BaseCondition>();

        public int CheckConditionsEnemySide(FighterStats enemyFighter)
        {
            fighter = enemyFighter;
            scoreConditions.Clear();
            foreach (var condition in conditions)
            {
                scoreConditions.Add(condition.CheckConditionEnemySide(fighter, this), condition);
            }
            _maxScore = scoreConditions.Keys.Max();
            return _maxScore;
        }
        public int CheckConditionsPlayerSide(FighterStats player)
        {
            fighter = player;
            scoreConditions.Clear();
            foreach (var condition in conditions)
            {
                scoreConditions.Add(condition.CheckConditionPlayerSide(fighter, this), condition);
            }
            _maxScore = scoreConditions.Keys.Max();
            return _maxScore;
        }
        public void PerformAbility()
        {
            if (_maxScore >= minScoreTreshHold)
            {
                scoreConditions[_maxScore].PerformAbility(this);
            }
            // MISSING ELSE
        }

        public int CalculateSinglePDamage()
        {
            int dmg = fighter.derivedStats.pAttack * (pDamageMultiplier / 100);
            return dmg;
        }

        public int CalculateSingleMDamage()
        {
            int dmg = fighter.derivedStats.mAttack * (mDamageMultiplier / 100);
            return dmg;
        }

        public int CalculateSingleHealing()
        {
            int healing = fighter.derivedStats.mAttack * (mDamageMultiplier / 100);
            return healing;
        }

    }
}