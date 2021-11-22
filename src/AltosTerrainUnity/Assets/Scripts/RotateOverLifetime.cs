using UnityEngine;

namespace AltosTerrain
{
	public class RotateOverLifetime : MonoBehaviour
	{
		public float rotationSpeed = 1.5f;

		void Update()
		{
			transform.Rotate(0.0f, 0.0f, rotationSpeed * Time.deltaTime);
		}
	}
}