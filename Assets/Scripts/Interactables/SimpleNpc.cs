using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

namespace ElMapacho
{
    public class SimpleNpc : MonoBehaviour, IInteractable
	{
		private Usable _usable;

		void Start()
		{
			_usable = GetComponent<Usable>();
			
		}
		public void Interact()
		{
			_usable.events.onUse?.Invoke();
		}
	}
}