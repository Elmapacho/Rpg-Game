using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using UnityEngine.SceneManagement;
using Doozy.Engine.Nody.Models;
using Cinemachine;


namespace ElMapacho
{
    public class GameManager : MonoBehaviour
    {
        public PlayerController playerController;
        public CinemachineVirtualCamera overworldCamera;
        public Vector2 PlayerPositionToSpawn = new Vector2(0, 0);
        public Vector2 lastPosition = new Vector2(0, 0);
        public bool IsGamePaused;

        public FighterStats player1;
        public FighterStats player2;
        public FighterStats player3;

        #region SINGLETON PATTERN
        private static GameManager _instance;
        public static GameManager a { get { return _instance; } }
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
            Awake2();
        }
        #endregion

        void Awake2()
        {
            /*QualitySettings.vSyncCount = 0;
            var frameRate = 60;
            //Application.targetFrameRate = frameRate;
            Debug.Log("Frame rate at: " + frameRate);*/
        }

        private void OnEnable()
        {
            Message.AddListener<GameEventMessage>(OnMessage);
        }

        private void OnDisable()
        {
            Message.RemoveListener<GameEventMessage>(OnMessage);
        }

        private void OnMessage(GameEventMessage message)
        {
            if (message == null) return;

        }

        public void MovePlayerTo(Vector2 destination) // move camera too
        {
            playerController.transform.position = destination;
            playerController.Reset();
            if (overworldCamera == null)
            {
                overworldCamera = FindObjectOfType<CinemachineVirtualCamera>();
            }
            overworldCamera.ForceCameraPosition(destination, Quaternion.Euler(Vector3.zero));
        }
    }
}