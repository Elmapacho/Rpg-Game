using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using System;
using PixelCrushers.DialogueSystem;

namespace ElMapacho
{
	public class TypeWriterSpeed : MonoBehaviour
	{
		private UnityUITypewriterEffect _typewriterEffect;

		void Start()
		{
			
		}

		void Update()
		{
			
		}

		private void OnEnable()
		{
			_typewriterEffect = GetComponent<UnityUITypewriterEffect>();
			_typewriterEffect.charactersPerSecond = SettingsManager.a.normalTextSpeed;
			Message.AddListener<GameEventMessage>(OnMessage);
		}

		private void OnDisable()
		{
			Message.RemoveListener<GameEventMessage>(OnMessage);
		}

		private void OnMessage(GameEventMessage obj)
		{
			if (obj.EventName == "TypeNormal")
			{
				Debug.Log("MessageReceived");
				_typewriterEffect.charactersPerSecond = SettingsManager.a.normalTextSpeed;
			}
			if (obj.EventName == "TypeFast")
			{
				_typewriterEffect.charactersPerSecond = SettingsManager.a.fastTextSpeed;
			}
		}
	}
}
