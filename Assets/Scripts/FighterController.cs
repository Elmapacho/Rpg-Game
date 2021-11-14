using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
	public class FighterController : MonoBehaviour
	{
		public FighterStats playerStats;


		private void Awake()
		{
			if (playerStats == null)
			{
				playerStats = GetComponent<FighterStats>();
			}
		}
	}
}
