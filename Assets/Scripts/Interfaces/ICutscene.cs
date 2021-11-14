using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
	public interface ICutscene 
	{
		void StartCutscene(PlayerController player, Look lookDirection);
	}
}
