using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
    public class Item : ScriptableObject
    {
        public string itemName;
        public int itemID;
        public Sprite icon;
        [TextArea]
        public string description;
        public bool unique;
        public int amount=1;
        public int amountStackable = 1;
    }
}