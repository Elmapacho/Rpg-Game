using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
    [CreateAssetMenu(menuName = "Conditions/Damage Condition")]
    public class DamageCondition : BaseCondition
    {
        [Header("Score to give if")]
        public int weakToTypeScore;
        public int lowHpScore;

        [Header("TreshHolds (usually %)")]
        public int amountOfWeak;
        public int amountLowHp;

        public override int CheckConditionEnemySide(FighterStats enemy, Ability ability)
        {
            BaseConditionStarter(enemy, ability);
            CheckWeakTypeEnemySide();
            return ReturnHighesScoreValue();
        }

        public override int CheckConditionPlayerSide(FighterStats player, Ability ability)
        {
            throw new System.NotImplementedException();
        }

        public override void PerformAbility(Ability ability)
        {
            throw new System.NotImplementedException();
        }

        private void CheckWeakTypeEnemySide()
        {
            // physical
            if (_thisAbility.pDamageMultiplier > 0 && _thisAbility.mDamageMultiplier == 0)
            {
                int total = 0;
                foreach (var player in availablePlayers)
                {
                    total += player.derivedStats.pDefense;
                }

                var average = total / availablePlayers.Count;
                foreach (var player in availablePlayers)
                {
                    if (Mathf.RoundToInt(average * 0.8f) <= player.derivedStats.pDefense)
                    {
                        scoresDic.Add(weakToTypeScore, player);
                        return;
                    }
                }
            }
            // magic
            if (_thisAbility.mDamageMultiplier > 0 && _thisAbility.pDamageMultiplier == 0)
            {
                int total = 0;
                foreach (var player in availablePlayers)
                {
                    total += player.derivedStats.mDefense;
                }

                var average = total / availablePlayers.Count;
                foreach (var player in availablePlayers)
                {
                    if (Mathf.RoundToInt(average * 0.8f) <= player.derivedStats.mDefense)
                    {
                        scoresDic.Add(weakToTypeScore, player);
                        return;
                    }
                }
            }
        }

    }
}