using System;
using System.Collections.Generic;

[Serializable]
public class GeneratedTerrain
{
	public TerrainPattern TerrainPattern;
	public List<GeneratedTerrainLayer> Layers = new();

	public GeneratedTerrain(TerrainPattern terrainPattern)
	{
		TerrainPattern = terrainPattern;

		for (int i = 0; i < terrainPattern.Biome.Layers.Length; i++)
		{
			var layer = terrainPattern.Biome.Layers[i];
			Layers.Add(new GeneratedTerrainLayer(layer, -i * 100));
		}
	}
}
