using System;
using UnityEngine;

namespace AltosTerrain
{
	[Serializable]
	public class TerrainLandscapeLayer
	{
		public enum LayerPatternType
		{
			Uniform,
			Perlin,
			Descending
		}

		public LayerPatternType Type = LayerPatternType.Perlin;
		public float Frequency = 0.025f;
		public float Scale = 1.0f;

		public float Sample(float time)
		{
			switch (Type)
			{
				default:
				case LayerPatternType.Uniform:
				{
					return Scale;
				}
				case LayerPatternType.Perlin:
				{
					return Mathf.PerlinNoise(time * Frequency, 0.5f) * Scale;
				}
				case LayerPatternType.Descending:
				{
					return time * -Scale;
				}
			}
		}
	}
}