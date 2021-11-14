using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Doozy.Engine;

namespace ElMapacho
{
    public class Spawner : MonoBehaviour
    {
        public List<EnemyToSpawn> enemiesPoll = new List<EnemyToSpawn>();
        public List<EnemyToSpawn> _enemiesToSpawn = new List<EnemyToSpawn>();
        public List<GameObject> enemiesSpawning = new List<GameObject>();

        public int battleChance;
        public int maxEnemies;
        public int probability1Enemy;
        public int probability2Enemies;
        public int probability3Enemies;
        public int amountsOfEnemies;

        public int _indexer;
        public bool TESTER;

        #region SINGLETON PATTERN
        private static Spawner _instance;
        public static Spawner a { get { return _instance; } }
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
        void Start()
        {
            enemiesSpawning.Clear();
        }
        private void FixedUpdate()
        {
            //if (TESTER)
            //  SpawnCheck();
        }

        public void AddEnemies(List<EnemyToSpawn> enemies)
        {
            enemiesPoll.Clear();
            for (int i = 0; i < enemies.Count; i++)
            {
                enemiesPoll.Add(enemies[i]);
            }
        }
        public void ClearEnemiesPoll()
        {
            enemiesPoll.Clear();

        }
        [ContextMenu("laputamdrequeteremilpario")]
        public void AAA()
        {
            _enemiesToSpawn.RemoveAt(2);
        }

        public void SpawnCheck()
        {
            if (UnityEngine.Random.Range(0, 100) > battleChance || battleChance == 0)
            {
                return;
            }
            ClearAndFillEnemyPoll();
            amountsOfEnemies = HowManyEnemiesToSpawn();
            WhichEnemyToSpawn();
            SpawnEnemies();
        }

        private void ClearAndFillEnemyPoll()
        {
            _enemiesToSpawn.Clear();
            foreach (var item in enemiesPoll)
            {
                _enemiesToSpawn.Add(item);
            }
        }
        private int HowManyEnemiesToSpawn()
        {
            var oneEnemy = probability1Enemy;
            var twoEnemies = probability1Enemy + probability2Enemies;
            var threeEnemies = probability1Enemy + probability2Enemies + probability3Enemies;

            var x = UnityEngine.Random.Range(0, threeEnemies + 1);
            if (x < oneEnemy && oneEnemy != 0)
            {
                return 1;
            }
            else if (x < twoEnemies && twoEnemies != 0)
            {
                return 2;
            }
            else if (x <= threeEnemies && threeEnemies != 0)
            {
                return 3;
            }
            Debug.Log(x);
            Debug.Log("Error in how many enemies");
            return 0;
        }
        public void IndexEnemiesToSpawn()
        {
            _indexer = 0;
            if (_enemiesToSpawn.Count == 0)
                return;
            for (int i = 0; i < _enemiesToSpawn.Count; i++)
            {
                _indexer += _enemiesToSpawn[i].spawnChance;
                _enemiesToSpawn[i].indexer = _indexer;
            }
        }
        private void WhichEnemyToSpawn()
        {
            enemiesSpawning.Clear();
            for (int x = 0; x < amountsOfEnemies; x++)
            {
                IndexEnemiesToSpawn();
                var index = UnityEngine.Random.Range(0, _indexer + 1);
                for (int i = 0; i < _enemiesToSpawn.Count; i++)
                {
                    if (_enemiesToSpawn[i].indexer >= index && _enemiesToSpawn[i].indexer != 0)
                    {
                        if (_enemiesToSpawn[i].isAlone && enemiesSpawning.Count > 0)
                        {
                            _enemiesToSpawn.RemoveAt(i);
                            x -= 1;
                            break;
                        }
                        else if (_enemiesToSpawn[i].isAlone)
                        {
                            enemiesSpawning.Add(_enemiesToSpawn[i].enemy);
                            amountsOfEnemies = 1;
                            break;
                        }
                        if (_enemiesToSpawn[i].onlyOne)
                        {
                            enemiesSpawning.Add(_enemiesToSpawn[i].enemy);
                            _enemiesToSpawn.RemoveAt(i);
                            break;
                        }
                        else
                        {
                            enemiesSpawning.Add(_enemiesToSpawn[i].enemy);
                            break;
                        }
                    }
                }
            }
            if (amountsOfEnemies == enemiesSpawning.Count)
            {
                Debug.Log("Correct amount of enemies");
            }
            else Debug.Log("Incorrent amount of enemies");
        }

        public void SpawnEnemies()
        {
            Debug.Log("Spawning Enemies");
            foreach (var item in BattleManager.a.enemiesGameObject)
            {
                Destroy(item);
            }

            BattleManager.a.enemiesGameObject.Clear();
            
            foreach (var enemy in enemiesSpawning)
            {
                var enemyClone = Instantiate(enemy);
                enemyClone.SetActive(false);
                DontDestroyOnLoad(enemyClone);
                BattleManager.a.enemiesGameObject.Add(enemyClone);
            }
            GameEventMessage.SendEvent("Battle");
        }
    }
}