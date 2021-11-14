using Doozy.Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
	public class AnimatedDoor : MonoBehaviour ,ICutscene
	{
		public Animator leftDoorAnimator;
		public Animator rightDoorAnimator;
		void Start()
		{
			
		}

		void Update()
		{
			


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

			if (message.EventName == "Open") 
			{ 
			}
		}

		[ContextMenu("Open Left door")]
		public void OpenLeftDoor()
		{
			leftDoorAnimator.SetTrigger("Open");

		}

		[ContextMenu("Close Left door")]
		public void CloseLeftDoor()
		{
			leftDoorAnimator.SetTrigger("Close");

		}

		public void StartCutscene(PlayerController player, Look lookDirection)
		{
			if (player.transform.position.x < transform.position.x)
			{
				leftDoorAnimator.SetTrigger("Open");
			}
			else
			{
				rightDoorAnimator.SetTrigger("Closed");
				
			}
		}
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Player")
			{
			}
		}
		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.tag == "Player")
			{
			}
		}
		public void OnOpenDoor()
		{
			Debug.Log("Doorhasbeenopened");
		}
	}
}
