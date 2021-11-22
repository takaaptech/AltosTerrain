using UnityEngine;

namespace AltosTerrain
{
	[CreateAssetMenu(menuName = "Terrain/Landscape", order = 1000)]
	public class TerrainLandscapePattern : ScriptableObject
	{
		public TerrainLandscapeLayer[] Layers;

		public float Sample(float time)
		{
			float value = 0.0f;
			foreach (var layer in Layers)
			{
				value += layer.Sample(time);
			}
			return value;
		}
	}
}