using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
    [CreateAssetMenu(fileName = "New Armor", menuName = "Item/Armor")]
    public class Armor : Item
    {
        public int addPDefense;
        public int addMDefense;
        public bool inmuneConfusion;

        public ArmorType armorType;
    }
}