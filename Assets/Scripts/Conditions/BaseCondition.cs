using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ElMapacho
{
    public abstract class BaseCondition : ScriptableObject
    {
        protected int BestScore;
        [HideInInspector] public FighterStats _thisFighter;
        [HideInInspector] public Ability _thisAbility;
        protected Dictionary<int, FighterStats> scoresDic = new Dictionary<int, FighterStats>();
        protected List<FighterStats> availableEnemies;
        protected List<FighterStats> availablePlayers;
        public abstract int CheckConditionEnemySide(FighterStats enemy, Ability ability);

        public abstract int CheckConditionPlayerSide(FighterStats player, Ability ability);

        public abstract void PerformAbility(Ability ability);
        public int ReturnHighesScoreValue()
        {
            int value = scoresDic.Keys.Max();
            return value;
        }
        public void BaseConditionStarter(FighterStats fighter, Ability ability)
        {
            _thisAbility = ability;
            _thisFighter = fighter;
            availablePlayers.Clear();
            availableEnemies.Clear();

            scoresDic.Clear(); //??/

            foreach (var player in BattleManager.a.playerStats)
            {
                availablePlayers.Add(player);
            }
            foreach (var enemy in BattleManager.a.enemyStats)
            {
                availableEnemies.Add(enemy);
            }
        }
    }
}