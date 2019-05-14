using BehaviorDesigner.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BehaviorDesigner.Editor
{
	[InitializeOnLoad]
	public class GizmoManager
	{
		private static string currentScene;

		static GizmoManager()
		{
			currentScene = SceneManager.GetActiveScene().name;
            
			EditorApplication.hierarchyChanged += HierarchyChange;
			if (!Application.isPlaying)
			{
				UpdateAllGizmos();
				EditorApplication.playModeStateChanged += UpdateAllGizmos;
			}
		}

		public static void UpdateAllGizmos(PlayModeStateChange playModeStateChange = PlayModeStateChange.EnteredEditMode)
		{
			Behavior[] array = Object.FindObjectsOfType<Behavior>();
			for (int i = 0; i < array.Length; i++)
			{
				UpdateGizmo(array[i]);
			}
		}

		public static void UpdateGizmo(Behavior behavior)
		{
			behavior.gizmoViewMode = (Behavior.GizmoViewMode)BehaviorDesignerPreferences.GetInt(BDPreferences.GizmosViewMode);
			behavior.showBehaviorDesignerGizmo = BehaviorDesignerPreferences.GetBool(BDPreferences.ShowSceneIcon);
		}

		public static void HierarchyChange()
		{
			BehaviorManager instance = BehaviorManager.instance;
			if (Application.isPlaying)
			{
				if (instance != null)
				{
					instance.onEnableBehavior = new BehaviorManager.BehaviorManagerHandler(UpdateBehaviorManagerGizmos);
				}
			}
			else if (currentScene != SceneManager.GetActiveScene().name)
			{
				currentScene = SceneManager.GetActiveScene().name;
				UpdateAllGizmos();
			}
		}

		private static void UpdateBehaviorManagerGizmos()
		{
			BehaviorManager instance = BehaviorManager.instance;
			if (instance != null)
			{
				for (int i = 0; i < instance.BehaviorTrees.Count; i++)
				{
					UpdateGizmo(instance.BehaviorTrees[i].behavior);
				}
			}
		}
	}
}