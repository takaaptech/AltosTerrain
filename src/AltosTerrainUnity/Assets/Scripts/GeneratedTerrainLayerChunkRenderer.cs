using UnityEngine;
using UnityEngine.U2D;

namespace AltosTerrain
{
	public class GeneratedTerrainLayerChunkRenderer : MonoBehaviour
	{
		public SpriteShapeRenderer spriteShapeRenderer;
		public SpriteShapeController spriteShapeController;
		public PolygonCollider2D polygonCollider;

		public GeneratedTerrainLayerChunk generatedTerrainLayerChunk;

		private void Update()
		{
			var materials = spriteShapeRenderer.materials;

			for (int i = 0; i < materials.Length; i++)
			{
				var material = materials[i];

				material.SetColor("_FogColor", generatedTerrainLayerChunk.Layer.BiomeLayer.FogColour);
				material.SetColor("_TerrainColor", generatedTerrainLayerChunk.Layer.BiomeLayer.TerrainColour);
				material.SetFloat("_FogOffset", generatedTerrainLayerChunk.Layer.BiomeLayer.FogOffset);
				material.SetFloat("_FogDensity", generatedTerrainLayerChunk.Layer.BiomeLayer.FogDensity);
			}
			spriteShapeRenderer.materials = materials;
		}

		public void Render(GeneratedTerrainLayerChunk generatedTerrainLayerChunk)
		{
			this.generatedTerrainLayerChunk = generatedTerrainLayerChunk;

			var materials = spriteShapeRenderer.materials;

			for (int i = 0; i < materials.Length; i++)
			{
				var material = materials[i];

				material.SetColor("_FogColor", generatedTerrainLayerChunk.Layer.BiomeLayer.FogColour);
				material.SetColor("_TerrainColor", generatedTerrainLayerChunk.Layer.BiomeLayer.TerrainColour);
				material.SetFloat("_FogOffset", generatedTerrainLayerChunk.Layer.BiomeLayer.FogOffset);
				material.SetFloat("_FogDensity", generatedTerrainLayerChunk.Layer.BiomeLayer.FogDensity);
			}
			spriteShapeRenderer.materials = materials;

			spriteShapeRenderer.sortingOrder = generatedTerrainLayerChunk.Layer.BaseOrderInLayer;

			transform.localPosition = new Vector3(generatedTerrainLayerChunk.Offset, 0.0f);

			polygonCollider.enabled = generatedTerrainLayerChunk.Layer.BiomeLayer.IsCollidable;

			var spline = spriteShapeController.spline;
			spline.Clear();

			int offset = 0;

			for (int i = 0; i < generatedTerrainLayerChunk.Nodes.Length; i++)
			{
				var surfacePoint = generatedTerrainLayerChunk.Nodes[i];

				if (i == 0)
				{
					spline.InsertPointAt(offset, new(surfacePoint.Position, surfacePoint.Height - 50.0f));
					offset++;
				}

				spline.InsertPointAt(offset, new(surfacePoint.Position, surfacePoint.Height));
				spline.SetTangentMode(offset, ShapeTangentMode.Broken);
				if (i != 0)
				{
					spline.SetLeftTangent(offset, new Vector3(-1.0f, surfacePoint.InTangent, 0.0f).normalized * surfacePoint.InWeight);
				}
				if (i != generatedTerrainLayerChunk.Nodes.Length - 1)
				{
					spline.SetRightTangent(offset, new Vector3(1.0f, surfacePoint.OutTangent, 0.0f).normalized * surfacePoint.OutWeight);
				}
				offset++;

				if (i == generatedTerrainLayerChunk.Nodes.Length - 1)
				{
					spline.InsertPointAt(offset, new(surfacePoint.Position, surfacePoint.Height - 50.0f));
					offset++;
				}
			}

			spriteShapeController.BakeMesh().Complete();
		}
	}
}