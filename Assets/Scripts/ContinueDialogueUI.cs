using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using Doozy.Engine;

namespace ElMapacho
{
	public class ContinueDialogueUI : MonoBehaviour
	{
		private StandardUIContinueButtonFastForward standard;
		public bool hasLineStarted { get; set; }

		private void OnEnable()
		{
			standard = GetComponent<StandardUIContinueButtonFastForward>();
			Message.AddListener<GameEventMessage>(OnMessage);
		}

		private void OnDisable()
		{
			Message.RemoveListener<GameEventMessage>(OnMessage);
		}

		private void OnMessage(GameEventMessage obj)
		{
			if (obj.EventName == "Continue" && hasLineStarted)
			{
				standard.OnFastForward();
			}

			if (obj.EventName == "LineStart")
			{
				StartCoroutine(ChangeBool());
			}
		}

		private IEnumerator ChangeBool()
		{
			yield return new WaitForSeconds(SettingsManager.a.continueWaitTime);
			hasLineStarted = true;
		}
	}
}
