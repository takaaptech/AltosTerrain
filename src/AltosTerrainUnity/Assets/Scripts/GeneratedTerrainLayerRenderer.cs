using UnityEngine;

namespace AltosTerrain
{
	public class GeneratedTerrainLayerRenderer : MonoBehaviour
	{
		private GeneratedTerrainLayer generatedTerrainLayer;

		public GeneratedTerrainLayerChunkRendererPrefabPool ChunkRendererPrefab;

		public void Render(GeneratedTerrainLayer generatedTerrainLayer)
		{
			ChunkRendererPrefab.ReturnAll();
			this.generatedTerrainLayer = generatedTerrainLayer;

			transform.position = new Vector3(0.0f, 0.0f);
		}

		private void Update()
		{
			var cameraPosition = Camera.main.transform.position.x;
			var layerPosition = cameraPosition * generatedTerrainLayer.BiomeLayer.MovementScale.x;

			foreach (var newChunk in generatedTerrainLayer.GenerateNewChunks(layerPosition + 40.0f))
			{
				var rendererClone = ChunkRendererPrefab.Grab(transform);

				rendererClone.Render(newChunk);
			}

			foreach (var currentChunk in ChunkRendererPrefab.Pool.ToArray())
			{
				if (currentChunk.isActiveAndEnabled
					&& currentChunk.generatedTerrainLayerChunk.MaxX < layerPosition - 40.0f)
				{
					ChunkRendererPrefab.Return(currentChunk);
				}
			}
		}

		private void OnEnable()
		{
			ParallaxManager.PostUpdateCallback += UpdatePosition;
		}

		private void OnDisable()
		{
			ParallaxManager.PostUpdateCallback -= UpdatePosition;
		}

		private void UpdatePosition()
		{
			if (generatedTerrainLayer == null)
			{
				return;
			}

			var parallax = generatedTerrainLayer.BiomeLayer.MovementScale - new Vector2(1.0f, 1.0f);

			transform.position = new Vector3(
				(Camera.main.transform.position.x * -parallax.x),
				(Camera.main.transform.position.y * -parallax.y) - generatedTerrainLayer.BiomeLayer.Offset,
				transform.position.z);
		}
	}
}