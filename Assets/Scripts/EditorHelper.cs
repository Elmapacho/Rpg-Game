using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
	[ExecuteAlways]
	public class EditorHelper : MonoBehaviour
	{
		Vector3 worldPosition;
		void Update()
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = Camera.main.nearClipPlane;
			worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
			Debug.Log(worldPosition);
		}
	}
}
