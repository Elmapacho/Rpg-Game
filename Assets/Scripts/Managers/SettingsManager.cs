using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
	public class SettingsManager : MonoBehaviour
	{
        [Header("UI Settings")]
        public float continueWaitTime=0.5f ;
        public float normalTextSpeed = 3;
        public float fastTextSpeed = 5;
        [Space]
        [Header("OverWorld Settings")]

        public float playerNormalSpeed;
        public float playerRunSpeed;
        public float npcSpeed;

        [Header("Combat System")]

        [Range(0, 100)]
        public int defenseDeminisher = 60;

        #region SINGLETON PATTERN
        private static SettingsManager _instance;
        public static SettingsManager a { get { return _instance; } }
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
            //Awake2();
        }
        #endregion
        void Start()
		{
			
		}

		void Update()
		{
			
		}
	}
}
