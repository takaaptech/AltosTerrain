using UnityEngine;

namespace AltosTerrain
{
	[CreateAssetMenu(menuName = "Terrain/Pattern", order = 500)]
	public class TerrainPattern : ScriptableObject
	{
		public TerrainBiome Biome;
	}
}