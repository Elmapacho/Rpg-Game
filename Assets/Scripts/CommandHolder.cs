using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using UnityEngine.EventSystems;

namespace ElMapacho
{
	public class CommandHolder : MonoBehaviour
	{
		static Vector3 resets;
		private EventSystem _eventSystem;
		void Start()
		{
			_eventSystem = FindObjectOfType<EventSystem>();
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.L))
			{
				GameEventMessage.SendEvent("Battle");
			}
			if (Input.GetKeyDown(KeyCode.K))
			{
				if (_eventSystem == null)
				{
					Debug.LogError("Missing event system");

				}
				else Debug.Log(_eventSystem.currentSelectedGameObject.name);
			}
		}
	}
}
