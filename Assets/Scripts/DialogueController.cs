using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;

namespace ElMapacho
{
	public class DialogueController : MonoBehaviour
	{
		private DialogueSystemController _dialogueSystemController;
		public float normalTextSpeed;
		public float fastTextSpeed;
		
		public bool IsConversationDone { get; set; }

		private void Start()
		{
			_dialogueSystemController = FindObjectOfType<DialogueSystemController>();
		}

		void Update()
		{
			if (Input.GetButtonDown("Continue"))
			{
				IncreaseConversationSpeed();
			}
			if (Input.GetButtonUp("Continue"))
			{
				DecreaseConversationSpeed();
			}
			if (Input.GetButtonDown("Interact"))
			{
				ContinueConversation();
			}
		}

		void ContinueConversation()
		{
			GameEventMessage.SendEvent("Continue");
		}

		void IncreaseConversationSpeed()
		{
			GameEventMessage.SendEvent("TypeFast");
		}
		void DecreaseConversationSpeed()
		{
			GameEventMessage.SendEvent("TypeNormal");
		}
	}
}
