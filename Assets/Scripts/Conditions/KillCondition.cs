using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
    [CreateAssetMenu(menuName = "Conditions/Kill Condition")]
    public class KillCondition : BaseCondition
    {
        [Header("Score to give if")]
        public int killScore;
        public override int CheckConditionEnemySide(FighterStats enemy, Ability ability)
        {
            BaseConditionStarter(enemy, ability);
            CheckKillConditionEnemySide();

            return ReturnHighesScoreValue();
        }

        public override int CheckConditionPlayerSide(FighterStats player, Ability ability)
        {
            throw new System.NotImplementedException();
        }

        void CheckKillConditionEnemySide()
        {
            foreach (var player in availablePlayers)
            {
                int calculatedDamage = player.CalculateTakeDamage(_thisAbility.CalculateSinglePDamage(), _thisAbility.CalculateSingleMDamage());
                if (player.derivedStats.currentHp - calculatedDamage <= 0)
                {
                    scoresDic.Add(killScore, player);
                }
            }
        }
        public override void PerformAbility(Ability ability)
        {
            throw new System.NotImplementedException();
        }

    }
}