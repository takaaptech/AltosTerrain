using Cinemachine;
using System;
using UnityEngine;

[RequireComponent(typeof(CinemachineBrain))]
public class ParallaxManager : MonoBehaviour
{
	public static Action PostUpdateCallback;

	private CinemachineBrain brain;

	private void Awake()
	{
		brain = GetComponent<CinemachineBrain>();
	}

	private void LateUpdate()
	{
		brain.ManualUpdate();

		PostUpdateCallback?.Invoke();
	}
}
