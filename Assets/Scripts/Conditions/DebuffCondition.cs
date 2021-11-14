using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
    [CreateAssetMenu(menuName = "Conditions/Debuff Condition")]
    public class DebuffCondition : BaseCondition
    {
        public override int CheckConditionEnemySide(FighterStats enemy, Ability ability)
        {
            BaseConditionStarter(enemy, ability);
            return 0;
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