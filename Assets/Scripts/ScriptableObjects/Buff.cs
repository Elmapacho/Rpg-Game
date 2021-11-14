using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
    public class Buff : ScriptableObject
    {
        public Sprite icon;
        public int duration;
        public StatType type;
        public int porcentageAmount;
        public int flatAmount;
    }
}