using BehaviorDesigner.Runtime;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviorDesigner.Editor
{
	public class GlobalVariablesWindow : EditorWindow
	{
		private string mVariableName = string.Empty;

		private int mVariableTypeIndex;

		private Vector2 mScrollPosition = Vector2.zero;

		private bool mFocusNameField;

		[SerializeField]
		private float mVariableStartPosition = -1f;

		[SerializeField]
		private List<float> mVariablePosition;

		[SerializeField]
		private int mSelectedVariableIndex = -1;

		[SerializeField]
		private string mSelectedVariableName;

		[SerializeField]
		private int mSelectedVariableTypeIndex;

		private GlobalVariables mVariableSource;

		public static GlobalVariablesWindow instance;

		[MenuItem("Tools/Behavior Designer/Global Variables", false, 1)]
		public static void ShowWindow()
		{
			GlobalVariablesWindow window = EditorWindow.GetWindow<GlobalVariablesWindow>(false, "Global Variables");
			window.minSize=(new Vector2(300f, 410f));
			window.maxSize=(new Vector2(300f, 3.40282347E+38f));
			window.wantsMouseMove=(true);
		}

		public void OnFocus()
		{
			GlobalVariablesWindow.instance = this;
			this.mVariableSource = GlobalVariables.Instance;
			if (this.mVariableSource != null)
			{
				this.mVariableSource.CheckForSerialization(!Application.isPlaying);
			}
			FieldInspector.Init();
		}

		public void OnGUI()
		{
			if (mVariableSource == null)
			{
				mVariableSource = GlobalVariables.Instance;
			}
			if (VariableInspector.DrawVariables(mVariableSource, true, null, ref mVariableName, ref mFocusNameField, ref mVariableTypeIndex, ref mScrollPosition, ref mVariablePosition, ref mVariableStartPosition, ref this.mSelectedVariableIndex, ref this.mSelectedVariableName, ref this.mSelectedVariableTypeIndex))
			{
				SerializeVariables();
			}
			if (Event.current.type == EventType.MouseDown && VariableInspector.LeftMouseDown(mVariableSource, null, Event.current.mousePosition, mVariablePosition, mVariableStartPosition, mScrollPosition, ref mSelectedVariableIndex, ref this.mSelectedVariableName, ref this.mSelectedVariableTypeIndex))
			{
				Event.current.Use();
				Repaint();
			}
		}

		private void SerializeVariables()
		{
			if (mVariableSource == null)
			{
				mVariableSource = GlobalVariables.Instance;
			}
			if (BehaviorDesignerPreferences.GetBool(BDPreferences.BinarySerialization))
			{
				BinarySerialization.Save(mVariableSource);
			}
			else
			{
				SerializeJSON.Save(mVariableSource);
			}
		}
	}
}