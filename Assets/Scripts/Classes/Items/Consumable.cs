using ElMapacho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Item/Consumable")]
public class Consumable : Item // id 0-99
{
    public int hpHeal;
    public int manaHeal;
    public bool curePoison;
    public bool cureSleep;
    public int boostMDamage;
    public int boostPDamage;
    public int boostMDefense;
    public int boostPDefense;
    public int addMDamage;
    public int addPDamage;
    public int addMDefense;
    public int addPDefense;
    public bool poison;
    public bool sleep;
}
