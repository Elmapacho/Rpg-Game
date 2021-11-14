using ElMapacho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Usable", menuName = "Item/Usable")]
public class BattleItem : Item // 600-799
{
    public int pDamage;
    public int mDamage;
    public bool sleep;
    public bool poison;
}
