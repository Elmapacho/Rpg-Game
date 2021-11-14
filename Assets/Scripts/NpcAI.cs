using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using System;

namespace ElMapacho
{
	public class NpcAI : MonoBehaviour
	{
		public Rigidbody2D rigidBody;
		public Animator animator;
		public DialogueSystemTrigger dialogueSystem;
		public List<Transform> positionsToGo = new List<Transform>();
		private List<Vector2> _positionsToGo = new List<Vector2>();
		public int _index = 0;
		public bool randomOrder;
		public float timeToWait;
		private float _timeWaiting;
		private Vector2 _positionMovingTo;
		public bool isWalking = true;
		public bool isWaiting = false;
		public bool negative = false;

		void Start()
		{
			foreach (var item in positionsToGo)
			{
				Vector2 a = new Vector2(item.position.x,item.position.y);
				_positionsToGo.Add(a);
			}
		}

		void Update()
		{
			if (isWalking)
			{
				CheckPositionToStop();
			}
			else
			{
				if (isWaiting)
				{
					if (_timeWaiting > timeToWait)
					{
						isWaiting = false;
					}
					else _timeWaiting += Time.deltaTime;
				}
				else CheckWhereToMove();
			}
		}

		private void CheckWhereToMove()
		{
			if (_index >= _positionsToGo.Count)
			{
				negative = true;
				_index -= 2;
			}
			if (_index <0 )
			{
				negative = false;
				_index = 1;
			}

			Vector2 trans = _positionsToGo[_index];
			Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

			if (transform.position.x > trans.x)
			{
				if (Physics2D.OverlapCircle(currentPosition + Vector2.left, .45f))
				{
					isWaiting = true;
					animator.SetTrigger("LookLeft");
					return;
				}

				MoveDirection(Vector2.left, "LookLeft");
				return;
			}
			if (transform.position.x < trans.x)
			{
				if (Physics2D.OverlapCircle(currentPosition + Vector2.right, .45f))
				{
					isWaiting = true;
					animator.SetTrigger("LookRight");
					return;
				}
				MoveDirection(Vector2.right, "LookRight");
				return;
			}
			if (transform.position.y < trans.y)
			{
				if (Physics2D.OverlapCircle(currentPosition + Vector2.up, .45f))
				{
					isWaiting = true;
					animator.SetTrigger("LookUp");
					return;
				}
				MoveDirection(Vector2.up, "LookUp");
				return;
			}
			if (transform.position.y > trans.y)
			{
				if (Physics2D.OverlapCircle(currentPosition + Vector2.down, .45f))
				{
					isWaiting = true;
					animator.SetTrigger("LookDown");
					return;
				}
				MoveDirection(Vector2.down, "LookDown");
				return;
			}
			// reach the goal position
			if (negative)
				_index -= 1;
			else _index += 1;

			isWaiting = true;
			dialogueSystem.enabled = true;
		}

		private void CheckPositionToStop()
		{
			if (Mathf.Abs(transform.position.x - _positionMovingTo.x) <0.05f && transform.position.x - _positionMovingTo.x !=0)
			{
				StopWalking();
			}
			if (Mathf.Abs(transform.position.y - _positionMovingTo.y) < 0.05f && transform.position.y - _positionMovingTo.y != 0)
			{
				StopWalking();
			}
		}

		private void MoveDirection(Vector2 direction, string animation)
		{
			rigidBody.AddForce(new Vector2(direction.x * SettingsManager.a.npcSpeed, direction.y * SettingsManager.a.npcSpeed), ForceMode2D.Impulse);
			_positionMovingTo = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
			animator.SetTrigger(animation);
			isWalking = true;
			dialogueSystem.enabled = false;
		}

		void StopWalking()
		{
			rigidBody.velocity = Vector2.zero;
			transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
			_timeWaiting = 0;
			isWalking = false;
		}
	}
}
