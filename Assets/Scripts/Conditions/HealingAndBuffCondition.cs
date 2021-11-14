using ElMapacho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/Healing And Buff Condition")]
public class HealingAndBuffCondition : BaseCondition
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
