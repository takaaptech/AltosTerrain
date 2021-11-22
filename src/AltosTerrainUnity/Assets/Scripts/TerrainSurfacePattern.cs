﻿using UnityEngine;

[CreateAssetMenu(menuName = "Terrain/Surface", order = 1001)]
public class TerrainSurfacePattern : ScriptableObject
{
	public TerrainSurfacePatternNode[] Nodes;
}
