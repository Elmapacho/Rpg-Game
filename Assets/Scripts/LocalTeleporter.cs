using Doozy.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
	public class LocalTeleporter : MonoBehaviour
	{
		public bool IsReadyToTeleport = false;
		public Vector2 destination;
        public TransitionAnimations transitionAnimation;
        public static bool isTeleporting = false;

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

            if (message.EventName == GameEvents.TeleportingLocal.ToString() && IsReadyToTeleport)
            {
                StartCoroutine(TeleportPlayer());
                IsReadyToTeleport = false;
            }
        }

        private IEnumerator TeleportPlayer()
        {
            GameManager.a.MovePlayerTo(destination);
            yield return new WaitForSeconds(0.1f);
            isTeleporting = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Player" && !isTeleporting)
			{
                Debug.Log("Teleporting");
				IsReadyToTeleport = true;
                StartCoroutine(StartTeleporting());
			}
		}

        private IEnumerator StartTeleporting()
        {
            GameEventMessage.SendEvent(GameEvents.TeleportLocal.ToString());
            isTeleporting = true;
            yield return new WaitForSecondsRealtime(0.2f);
            GameEventMessage.SendEvent(transitionAnimation.ToString());
        }
	}
}
