using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
	public class FakeWallHelper : MonoBehaviour
	{
		public BoxCollider2D boxCollider;
		private void OnTriggerEnter2D(Collider2D collision)
		{
			boxCollider.enabled = true;
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			boxCollider.enabled = false;
		}
	}
}
