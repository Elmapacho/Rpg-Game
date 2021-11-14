using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
	public class ColliderGizmo : MonoBehaviour
	{
		private void OnDrawGizmos()
		{
			Gizmos.color = new Color(1,0,0,0.25f);
			Gizmos.DrawCube(transform.position, transform.lossyScale);
		}
	}
}
