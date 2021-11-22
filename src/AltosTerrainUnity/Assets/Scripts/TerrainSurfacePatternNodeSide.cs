using System;
using UnityEngine;

[Serializable]
public class TerrainSurfacePatternNodeSide
{
	[Header("Shape")]
	[Range(-10.0f, 10.0f)]
	public float MinimumAngle = 0.0f;
	[Range(-10.0f, 10.0f)]
	public float MaximumAngle = 0.0f;
	[Min(0.0f)]
	public float MinimumStrength = 1.0f;
	[Min(0.0f)]
	public float MaximumStrength = 2.0f;

	[Header("Spacing")]
	[Min(0.05f)]
	public float MinimumDistance = 0.5f;
	[Min(0.05f)]
	public float MaximumDistance = 1.0f;
}
