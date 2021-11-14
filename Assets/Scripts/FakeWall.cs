using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
	public class FakeWall : MonoBehaviour
	{
		public GameObject boxCollider;
		public GameObject wallsConteiner;
		private int times = 6;
		int index = 0;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag("Player"))
			{
				StopAllCoroutines();
				StartCoroutine(HideWall());
			}
			boxCollider.SetActive(true);
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.CompareTag("Player"))
			{
				StopAllCoroutines();
				StartCoroutine(ShowWall());
			}
			boxCollider.SetActive(false);
		}

		public IEnumerator HideWall()
		{
			index = 0;
			var a = GetComponentInChildren<SpriteRenderer>();
			float alpha = a.color.a;
			float x = (alpha - 0.5f) /(times -1);
			for (int i = 0; i < times; i++)
			{
				Color color = new Color();
				color.r = 255;
				color.b = 255;
				color.g = 255;
				color.a = alpha - (x * index);

				foreach (Transform child in wallsConteiner.transform)
				{
					var sprite = child.GetComponent<SpriteRenderer>();
					sprite.color = color;
					sprite.sortingOrder = 1;
				}
				index += 1;
				yield return new WaitForSeconds(0.1f);
			}
		}

		public IEnumerator ShowWall()
		{
			index = 0;
			for (int i = 0; i < times; i++)
			{
				Color color = new Color();
				color.r = 255;
				color.b = 255;
				color.g = 255;
				color.a = 0.5f + (0.15f * index);

				foreach (Transform child in wallsConteiner.transform)
				{
					var x = child.GetComponent<SpriteRenderer>();
					x.color = color;

				}
				index += 1;
				yield return new WaitForSeconds(0.1f);
			}
			foreach (Transform child in wallsConteiner.transform)
			{
				var x = child.GetComponent<SpriteRenderer>();
				x.sortingOrder = 0;

			}
		}
	}
}
