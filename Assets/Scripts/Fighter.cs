using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
	public class Fighter : MonoBehaviour
	{
		public FighterStats fighterStats;
		private AIState aiState = null;

		public List<Ability> abilities = new List<Ability>();
		public Dictionary<int, Ability> bestScoresOfEachAbility = new Dictionary<int, Ability>();

		void Awake()
		{
			if (fighterStats != null)
			{
				fighterStats = GetComponent<FighterStats>();
			}
		}

		void Update()
		{
			if (aiState != null)
			{
				aiState.OnUpdate();
			}
		}

		public void StartTurn()
		{

		}

		public void SetToWait()
		{
			aiState = new WaitState(this);
			aiState.OnEnter();
		}

		public void SetToIdle()
		{
			aiState = new IdleState(this);
			aiState.OnEnter();
		}
	}
}
