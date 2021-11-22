using UnityEngine;

namespace AltosTerrain
{
	[CreateAssetMenu(menuName = "Terrain/Structure", order = 1002)]
	public class TerrainStructurePattern : ScriptableObject
	{
		public Sprite Graphic;
		public float MinimumDistance = 0.1f;
		public float MaximumDistance = 0.5f;
		public AnimationCurve DistanceDistribution = new AnimationCurve(
			new Keyframe(0.0f, 0.0f),
			new Keyframe(1.0f, 1.0f));
	}
}