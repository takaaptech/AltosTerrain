using System;

namespace AltosTerrain
{
	[Serializable]
	public class GeneratedTerrainLayerChunk
	{
		public float Offset;
		public GeneratedTerrainNode[] Nodes;

		public float Width => Nodes[^1].Position;

		public float MaxX => Offset + Width;

		public GeneratedTerrainLayer Layer { get; }

		public GeneratedTerrainLayerChunk(GeneratedTerrainLayer layer)
		{
			Layer = layer;
		}
	}
}