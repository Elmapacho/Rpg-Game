using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyToSpawn
{
    public GameObject enemy;
    public int spawnChance;
    public bool isAlone;
    public bool onlyOne;
    [HideInInspector] public int indexer;
}
