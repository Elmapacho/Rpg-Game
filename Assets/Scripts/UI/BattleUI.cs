using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Doozy.Engine;
using Doozy.Engine.UI;

namespace ElMapacho
{
    public class BattleUI : MonoBehaviour
    {
        [Header("Choose View")]
        public Button attackButton;
        public Button inventoryButton;
        public Button escapeButton;

        [Header("Attack View")]
        public List<TMP_Text> attacksLabels = new List<TMP_Text>();

        [Space]
        public TMP_Text abilityDescriptionText;


        #region SINGLETON PATTERN
        private static BattleUI _instance;
        public static BattleUI a { get { return _instance; } }
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

        void Update()
        {

        }

        public void xAttack()
        {
            Debug.Log("attacks option");
            var fighter = BattleManager.a.currentFighter;

            for (int i = 0; i < fighter.abilities.Count; i++)
            {
                attacksLabels[i].text = fighter.abilities[i].abilityName;
            }

            for (int i = fighter.abilities.Count; i < attacksLabels.Count; i++)
            {
                attacksLabels[i].text = "EMPTY";
            }
        }

        public void xInventory()
        {
            Debug.Log("items option");
        }

        public void xEscape()
        {
            Debug.Log("escape option");
        }
    }
}