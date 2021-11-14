using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
    public class SpawnArea : MonoBehaviour
    {
        public List<EnemyToSpawn> enemiesToSpawn = new List<EnemyToSpawn>();
        public int chancesToSpawn;
        [Header("The sum should be 100")]
        public int probability1Enemy;
        public int probability2Enemies;
        public int probability3Enemies;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;

            Spawner.a.enemiesPoll.Clear();
            Spawner.a.AddEnemies(enemiesToSpawn);
            Spawner.a.battleChance = chancesToSpawn;
            Spawner.a.probability1Enemy = probability1Enemy;
            Spawner.a.probability2Enemies = probability2Enemies;
            Spawner.a.probability3Enemies = probability3Enemies;
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;
            Spawner.a.ClearEnemiesPoll();
            Spawner.a.battleChance = 0;
        }
    }
}