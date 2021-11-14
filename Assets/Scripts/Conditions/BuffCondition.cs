using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
    [CreateAssetMenu(menuName = "Conditions/Buff Condition")]
    public class BuffCondition : BaseCondition
    {
        public override int CheckConditionEnemySide(FighterStats enemy, Ability ability)
        {
            BaseConditionStarter(enemy, ability);
            throw new System.NotImplementedException();
        }

        public override int CheckConditionPlayerSide(FighterStats player, Ability ability)
        {
            throw new System.NotImplementedException();
        }

        public override void PerformAbility(Ability ability)
        {
            throw new System.NotImplementedException();
        }
    }
}