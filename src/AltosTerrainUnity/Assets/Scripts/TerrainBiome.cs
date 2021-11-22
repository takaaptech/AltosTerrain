using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Terrain/Biome", order = 501)]
public class TerrainBiome : ScriptableObject
{
	[Serializable]
	public class TerrainBiomeLayer
	{
		[Header("Gameplay")]
		public bool IsCollidable = false;

		[Header("Parallaxing")]
		public float Offset = 0.0f;
		public float Scale = 1.0f;
		public Vector2 MovementScale = new(1.0f, 1.0f);

		[Header("Style")]
		public Color TerrainColour = Color.white;

		[Space]
		public Color FogColour = new(0.75f, 0.75f, 0.75f, 1.0f);
		public float FogOffset = -0.01f;
		public float FogDensity = 0.1f;

		[Header("Generation")]
		public TerrainSurfacePattern Surface;
		public TerrainLandscapePattern Landscape;
		public TerrainStructurePattern[] Structures;
	}

	public Color SkyColour;
	public Color SunColour;

	[Header("Terrain")]
	public TerrainBiomeLayer[] Layers;
}
