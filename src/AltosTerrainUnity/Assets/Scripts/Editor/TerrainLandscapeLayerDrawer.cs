using UnityEditor;
using UnityEngine;

namespace AltosTerrain
{
	[CustomPropertyDrawer(typeof(TerrainLandscapeLayer))]
	public class TerrainLandscapeLayerDrawer : PropertyDrawer
	{
		private const int graphSamples = 64;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return 96;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var layerTypeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

			var layerFrequencyRect = new Rect(position.x, layerTypeRect.yMax + EditorGUIUtility.standardVerticalSpacing,
				position.width, EditorGUIUtility.singleLineHeight);

			var layerScaleRect = new Rect(position.x, layerFrequencyRect.yMax + EditorGUIUtility.standardVerticalSpacing,
				position.width, EditorGUIUtility.singleLineHeight);

			var previewGraphRect = new Rect(position.x, layerScaleRect.yMax + EditorGUIUtility.standardVerticalSpacing,
				position.width, position.yMax - layerScaleRect.yMax - (EditorGUIUtility.standardVerticalSpacing * 2));

			var layerTypeProperty = property.FindPropertyRelative(nameof(TerrainLandscapeLayer.Type));
			var layerFrequencyProperty = property.FindPropertyRelative(nameof(TerrainLandscapeLayer.Frequency));
			var layerScaleProperty = property.FindPropertyRelative(nameof(TerrainLandscapeLayer.Scale));

			EditorGUI.PropertyField(layerTypeRect, layerTypeProperty);
			EditorGUI.PropertyField(layerFrequencyRect, layerFrequencyProperty);
			EditorGUI.PropertyField(layerScaleRect, layerScaleProperty);

			if (Event.current.type == EventType.Repaint)
			{
				EditorGUI.DrawRect(previewGraphRect, new Color(0.1f, 0.1f, 0.1f, 1.0f));

				var spring = new TerrainLandscapeLayer
				{
					Type = (TerrainLandscapeLayer.LayerPatternType)layerTypeProperty.enumValueIndex,
					Frequency = layerFrequencyProperty.floatValue,
					Scale = layerScaleProperty.floatValue
				};

				var samples = new float[graphSamples];
				for (int i = 0; i < graphSamples; i++)
				{
					float time = (float)i / graphSamples - 1;

					samples[i] = spring.Sample(time);
				}

				EditorGUI.LabelField(previewGraphRect, samples.Length.ToString());

				Handles.BeginGUI();

				Vector3[] graphPolyLine = new Vector3[samples.Length];
				for (int i = 0; i < samples.Length; i++)
				{
					float time = ((float)i) / (samples.Length - 1);
					float value = samples[i];

					graphPolyLine[i] = new Vector3(
						Mathf.Lerp(previewGraphRect.xMin, previewGraphRect.xMax, time),
						Mathf.Lerp(previewGraphRect.yMax, previewGraphRect.yMin, value), 0);

					if (i != 0)
					{
						Handles.DrawLine(graphPolyLine[i - 1], graphPolyLine[i]);
					}
				}

				Handles.color = Color.green;
				Handles.DrawPolyLine(graphPolyLine);

				Handles.EndGUI();
			}
		}
	}
}
