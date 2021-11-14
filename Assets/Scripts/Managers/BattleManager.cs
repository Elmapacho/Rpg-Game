using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Doozy.Engine;
using System;
using System.Linq;

namespace ElMapacho
{
    public class BattleManager : MonoBehaviour
    {
        public List<Transform> playersSpawnPositions = new List<Transform>();
        public List<Transform> enemiesSpawnPositions = new List<Transform>();

        public List<GameObject> playersGameObject = new List<GameObject>();
        public List<Fighter> playersFighter = new List<Fighter>();
        public List<FighterStats> playerStats = new List<FighterStats>();

        public List<GameObject> enemiesGameObject = new List<GameObject>();
        public List<Fighter> enemiesFighter = new List<Fighter>();
        public List<FighterStats> enemyStats = new List<FighterStats>();

        public int turnOrder;
        public List<Fighter> fightersOrder = new List<Fighter>();

        public Fighter currentFighter;

        #region SINGLETON PATTERN
        private static BattleManager _instance;
        public static BattleManager a { get { return _instance; } }
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
            DontDestroyOnLoad(this);
        }
        #endregion

        private void OnEnable()
        {
            Message.AddListener<GameEventMessage>(OnMessage);
        }

        private void OnDisable()
        {
            Message.RemoveListener<GameEventMessage>(OnMessage);
        }

        private void OnMessage(GameEventMessage message)
        {
            if (message == null) return;

            if (message.EventName == GameEvents.NextTurn.ToString())
            {
                NextTurn();
            }

            if (message.EventName == "StartBattle")
            {
                Debug.Log("event Recevied");
                SetUpBattle();
            }
        }

        public void SetUpBattle()
        {
            GetComponents();
            PutFightersInPosition();
            DetermineAttackOrder();
            MakeFightersIdle();
        }
        void GetComponents()
        {
            playerStats.Clear();
            playersFighter.Clear();
            playersGameObject.Add(GameManager.a.player1.gameObject);
            playersGameObject.Add(GameManager.a.player2.gameObject);
            playersGameObject.Add(GameManager.a.player3.gameObject);

            foreach (var player in playersGameObject)
            {
                playerStats.Add(player.GetComponent<FighterStats>());
                playersFighter.Add(player.GetComponent<Fighter>());
            }
            enemyStats.Clear();
            enemiesFighter.Clear();
            foreach (var enemy in enemiesGameObject)
            {
                enemyStats.Add(enemy.GetComponent<FighterStats>());
                enemiesFighter.Add(enemy.GetComponent<Fighter>());
            }
        }
        void PutFightersInPosition()
        {
            for (int i = 0; i < playersFighter.Count; i++)
            {
                playersFighter[i].gameObject.SetActive(true);
                playersFighter[i].transform.position = playersSpawnPositions[i].position;
            }
            for (int i = 0; i < enemiesFighter.Count; i++)
            {
                enemiesFighter[i].gameObject.SetActive(true);
                enemiesFighter[i].transform.position = enemiesSpawnPositions[i].position;
            }
        }
        void DetermineAttackOrder()
        {
            fightersOrder.Clear();
            turnOrder = 0;
            foreach (var fighter in playersFighter)
            {
                fightersOrder.Add(fighter);
            }

            foreach (var fighter in enemiesFighter)
            {
                fightersOrder.Add(fighter);
            }

            fightersOrder.OrderBy(fighter => fighter.fighterStats.orderSpeed);
        }
        void MakeFightersIdle() // not finished
        {
            foreach (var fighter in fightersOrder)
            {
                fighter.SetToIdle();
            }
        }

        public void StartBattle()
        {
            //play animations
        }

        public void NextTurn()
        {
            if (CheckIfEnemiesDied())
            {
                EndBattle();
                return;
            }

            if (turnOrder> fightersOrder.Count)
            {
                turnOrder = 0;
            }

            if (fightersOrder[turnOrder].fighterStats.derivedStats.currentHp <=0)
            {
                turnOrder += 1;
                if (fightersOrder[turnOrder].fighterStats.derivedStats.currentHp <= 0)
                {
                    turnOrder += 1;
                    if (fightersOrder[turnOrder].fighterStats.derivedStats.currentHp <= 0)
                    {
                        turnOrder += 1;
                        if (fightersOrder[turnOrder].fighterStats.derivedStats.currentHp <= 0)
                        {
                            turnOrder += 1;
                            if (fightersOrder[turnOrder].fighterStats.derivedStats.currentHp <= 0)
                            {
                                turnOrder += 1;
                                if (fightersOrder[turnOrder].fighterStats.derivedStats.currentHp <= 0)
                                {
                                    turnOrder += 1;
                                }
                            }
                        }
                    }
                }

            }
            fightersOrder[turnOrder].StartTurn();
        }

        bool CheckIfEnemiesDied()
        {
            foreach (var enemy in enemiesFighter)
            {
                if (enemy.fighterStats.derivedStats.currentHp <= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void AttemptToEscape()
        {
            // calculate the posibilities to escape
            FailEscape();
            SuccesfulEscape();
        }

        void FailEscape()
        {

        }

        void SuccesfulEscape()
        {

        }

        public void EndBattle()
        {

        }
    }
}