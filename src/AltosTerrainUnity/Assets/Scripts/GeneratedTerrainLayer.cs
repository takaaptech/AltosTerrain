using System;
using System.Collections.Generic;
using UnityEngine;

namespace AltosTerrain
{
	[Serializable]
	public class GeneratedTerrainLayer
	{
		private static readonly float targetChunkSize = 50.0f;

		public TerrainBiome.TerrainBiomeLayer BiomeLayer;
		public int BaseOrderInLayer;
		public List<GeneratedTerrainLayerChunk> Chunks = new();

		private int selector = 0;

		public GeneratedTerrainLayer(TerrainBiome.TerrainBiomeLayer biomeLayer, int baseOrderInLayer)
		{
			BiomeLayer = biomeLayer;
			BaseOrderInLayer = baseOrderInLayer;
		}

		public IEnumerable<GeneratedTerrainLayerChunk> GenerateNewChunks(float furthestPosition)
		{
			if (Chunks.Count == 0)
			{
				var newChunk = GenerateChunk(null);
				Chunks.Add(newChunk);
				yield return newChunk;
			}

			while (Chunks[^1].Offset < furthestPosition)
			{
				var lastChunk = Chunks[^1];

				var newChunk = GenerateChunk(lastChunk);
				Chunks.Add(newChunk);
				yield return newChunk;
			}
		}

		private GeneratedTerrainLayerChunk GenerateChunk(GeneratedTerrainLayerChunk lastChunk)
		{
			float xMin = 0.0f;
			if (lastChunk != null)
			{
				xMin = lastChunk.MaxX;
			}

			float distanceGenerated = 0.0f;

			var generatedTerrainNodes = new List<GeneratedTerrainNode>();

			// Inherit tangents values from the previous chunk
			if (lastChunk != null)
			{
				var inheritNode = lastChunk.Nodes[^1];

				float outTangent = inheritNode.Pattern.FlatTangents
					? -inheritNode.InTangent
					: UnityEngine.Random.Range(inheritNode.Pattern.RightSide.MinimumAngle, inheritNode.Pattern.RightSide.MaximumAngle);

				float outWeight = inheritNode.Pattern.MirrorTangentStrength
					? inheritNode.InWeight
					: UnityEngine.Random.Range(inheritNode.Pattern.RightSide.MinimumStrength, inheritNode.Pattern.RightSide.MaximumStrength);

				var generatedTerrainNode = new GeneratedTerrainNode()
				{
					Pattern = inheritNode.Pattern,
					Position = distanceGenerated,
					Height = inheritNode.Height,
					InTangent = inheritNode.InTangent,
					InWeight = inheritNode.InWeight * BiomeLayer.Scale,
					OutTangent = outTangent,
					OutWeight = outWeight * BiomeLayer.Scale
				};

				generatedTerrainNodes.Add(generatedTerrainNode);

				distanceGenerated += UnityEngine.Random.Range(
					inheritNode.Pattern.RightSide.MinimumDistance,
					inheritNode.Pattern.RightSide.MaximumDistance) * BiomeLayer.Scale;
			}

			while (distanceGenerated < targetChunkSize)
			{
				var newNodePattern = BiomeLayer.Surface.Nodes[selector];

				var randomHeightWeight = newNodePattern.HeightDistribution.Evaluate(UnityEngine.Random.Range(0.0f, 1.0f));
				var randomHeight = Mathf.Lerp(newNodePattern.MinimumHeightOffset, newNodePattern.MaximumHeightOffset, randomHeightWeight) * BiomeLayer.Scale;

				randomHeight += BiomeLayer.Landscape.Sample(xMin + distanceGenerated);

				float inTangent = UnityEngine.Random.Range(newNodePattern.LeftSide.MinimumAngle, newNodePattern.LeftSide.MaximumAngle);
				float outTangent = newNodePattern.FlatTangents
					? -inTangent
					: UnityEngine.Random.Range(newNodePattern.RightSide.MinimumAngle, newNodePattern.RightSide.MaximumAngle);

				float inWeight = UnityEngine.Random.Range(newNodePattern.LeftSide.MinimumStrength, newNodePattern.LeftSide.MaximumStrength);
				float outWeight = newNodePattern.MirrorTangentStrength
					? inWeight
					: UnityEngine.Random.Range(newNodePattern.RightSide.MinimumStrength, newNodePattern.RightSide.MaximumStrength);

				var generatedTerrainNode = new GeneratedTerrainNode()
				{
					Pattern = newNodePattern,
					Position = distanceGenerated,
					Height = randomHeight,
					InTangent = inTangent,
					InWeight = inWeight * BiomeLayer.Scale,
					OutTangent = outTangent,
					OutWeight = outWeight * BiomeLayer.Scale
				};

				generatedTerrainNodes.Add(generatedTerrainNode);

				distanceGenerated += UnityEngine.Random.Range(newNodePattern.RightSide.MinimumDistance, newNodePattern.RightSide.MaximumDistance) * BiomeLayer.Scale;

				selector++;
				if (selector >= BiomeLayer.Surface.Nodes.Length)
				{
					selector = 0;
				}
			}

			var terrainChunk = new GeneratedTerrainLayerChunk(this)
			{
				Offset = xMin,
				Nodes = generatedTerrainNodes.ToArray()
			};
			Chunks.Add(terrainChunk);

			return terrainChunk;
		}
	}
}