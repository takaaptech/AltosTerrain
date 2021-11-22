using UnityEngine;

namespace AltosTerrain
{
	public class GeneratedTerrainRenderer : MonoBehaviour
	{
		public TerrainPattern TerrainPattern;
		public GeneratedTerrainLayerRendererPrefabPool LayerRendererPrefab;

		[Space]
		public SpriteRenderer SkyboxGraphic;
		public SpriteRenderer SunGlowGraphic;

		public GeneratedTerrain GeneratedTerrain;

		private void Start()
		{
			RegenerateTerrain();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				RegenerateTerrain();
			}
		}

		void RegenerateTerrain()
		{
			SkyboxGraphic.color = TerrainPattern.Biome.SkyColour;
			SunGlowGraphic.color = TerrainPattern.Biome.SunColour;

			LayerRendererPrefab.ReturnAll();

			GeneratedTerrain = new GeneratedTerrain(TerrainPattern);

			foreach (var layer in GeneratedTerrain.Layers)
			{
				var renderer = LayerRendererPrefab.Grab(transform);

				renderer.Render(layer);
			}
		}
	}
}