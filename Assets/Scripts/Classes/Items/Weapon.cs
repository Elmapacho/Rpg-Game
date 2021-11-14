using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
    public class Weapon : Item //id 100-199 p weapons id 200-299 magic weapons
    {
        public int addPDamage;
        public int addMDamage;
        public int addHealth;
        public int addMana;
    }
}