using System;
using UnityEngine;

[Serializable]
public class TerrainSurfacePatternNode
{
	public string Name;

	[Space]
	public float Weight = 1.0f;
	public bool CanBeMirrored;

	[Header("Tangents")]
	public bool MirrorTangents;
	public bool MirrorTangentStrength;
	public bool FlatTangents;

	[SerializeField] private TerrainSurfacePatternNodeSide leftSide;
	[SerializeField] private TerrainSurfacePatternNodeSide rightSide;

	[Header("Height")]
	public AnimationCurve HeightDistribution = new AnimationCurve(
		new Keyframe(0.0f, 0.0f),
		new Keyframe(1.0f, 1.0f));

	[Space]
	public float MinimumHeightOffset = -1.5f;
	public float MaximumHeightOffset = 1.5f;

	public TerrainSurfacePatternNodeSide LeftSide => leftSide;
	public TerrainSurfacePatternNodeSide RightSide
	{
		get
		{
			if (MirrorTangents)
			{
				return leftSide;
			}
			return rightSide;
		}
	}
}
