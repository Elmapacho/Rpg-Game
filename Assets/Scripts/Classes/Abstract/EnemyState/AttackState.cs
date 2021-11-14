using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace ElMapacho
{
    public class AttackState : AIState
    {
        public AttackState(Fighter fighterToControl) : base(fighterToControl)
        {
        }

        public override void OnEnter()
        {
            CheckScoresOfEachAbility();
            PerformeAbility();

        }


        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
        }

        public void CheckScoresOfEachAbility()
        {
            fighter.bestScoresOfEachAbility.Clear();
            foreach (var ability in fighter.abilities)
            {
                int score = ability.CheckConditionsPlayerSide(fighter.GetComponent<FighterStats>());
                fighter.bestScoresOfEachAbility.Add(score, ability);
            }
        }
        private void PerformeAbility()
        {
            fighter.bestScoresOfEachAbility[fighter.bestScoresOfEachAbility.Keys.Max()].PerformAbility();
        }
    }
}