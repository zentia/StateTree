using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace BehaviorDesigner.Editor
{
	public static class BehaviorDesignerUtility
	{
		public const string Version = "1.5.5";

		public const int ToolBarHeight = 18;

		public const int PropertyBoxWidth = 300;

		public const int ScrollBarSize = 15;

		public const int EditorWindowTabHeight = 21;

		public const int PreferencesPaneWidth = 290;

		public const int PreferencesPaneHeight = 348;

		public const float GraphZoomMax = 1f;

		public const float GraphZoomMin = 0.4f;

		public const float GraphZoomSensitivity = 150f;

		public const float GraphAutoScrollEdgeDistance = 15f;

		public const float GraphAutoScrollEdgeSpeed = 3f;

		public const int LineSelectionThreshold = 7;

		public const int TaskBackgroundShadowSize = 3;

		public const int TitleHeight = 20;

		public const int TitleCompactHeight = 28;

		public const int IconAreaHeight = 52;

		public const int IconSize = 44;

		public const int IconBorderSize = 46;

		public const int CompactAreaHeight = 22;

		public const int ConnectionWidth = 42;

		public const int TopConnectionHeight = 14;

		public const int BottomConnectionHeight = 16;

		public const int TaskConnectionCollapsedWidth = 26;

		public const int TaskConnectionCollapsedHeight = 6;

		public const int MinWidth = 100;

		public const int MaxWidth = 220;

		public const int MaxCommentHeight = 100;

		public const int TextPadding = 20;

		public const float NodeFadeDuration = 0.5f;

		public const int IdentifyUpdateFadeTime = 500;

		public const int MaxIdentifyUpdateCount = 2000;

		public const float InterruptTaskHighlightDuration = 0.75f;

		public const int TaskPropertiesLabelWidth = 150;

		public const int MaxTaskDescriptionBoxWidth = 400;

		public const int MaxTaskDescriptionBoxHeight = 300;

		public const int MinorGridTickSpacing = 10;

		public const int MajorGridTickSpacing = 50;

		public const int RepaintGUICount = 1;

		public const float UpdateCheckInterval = 1f;

		private static GUIStyle graphStatusGUIStyle = null;

		private static GUIStyle taskFoldoutGUIStyle = null;

		private static GUIStyle taskTitleGUIStyle = null;

		private static GUIStyle[] taskGUIStyle = new GUIStyle[9];

		private static GUIStyle[] taskCompactGUIStyle = new GUIStyle[9];

		private static GUIStyle[] taskSelectedGUIStyle = new GUIStyle[9];

		private static GUIStyle[] taskSelectedCompactGUIStyle = new GUIStyle[9];

		private static GUIStyle taskRunningGUIStyle = null;

		private static GUIStyle taskRunningCompactGUIStyle = null;

		private static GUIStyle taskRunningSelectedGUIStyle = null;

		private static GUIStyle taskRunningSelectedCompactGUIStyle = null;

		private static GUIStyle taskIdentifyGUIStyle = null;

		private static GUIStyle taskIdentifyCompactGUIStyle = null;

		private static GUIStyle taskIdentifySelectedGUIStyle = null;

		private static GUIStyle taskIdentifySelectedCompactGUIStyle = null;

		private static GUIStyle taskHighlightGUIStyle = null;

		private static GUIStyle taskHighlightCompactGUIStyle = null;

		private static GUIStyle taskCommentGUIStyle = null;

		private static GUIStyle taskCommentLeftAlignGUIStyle = null;

		private static GUIStyle taskCommentRightAlignGUIStyle = null;

		private static GUIStyle taskDescriptionGUIStyle = null;

		private static GUIStyle graphBackgroundGUIStyle = null;

		private static GUIStyle selectionGUIStyle = null;

		private static GUIStyle sharedVariableToolbarPopup = null;

		private static GUIStyle labelWrapGUIStyle = null;

		private static GUIStyle tolbarButtonLeftAlignGUIStyle = null;

		private static GUIStyle toolbarLabelGUIStyle = null;

		private static GUIStyle taskInspectorCommentGUIStyle = null;

		private static GUIStyle taskInspectorGUIStyle = null;

		private static GUIStyle toolbarButtonSelectionGUIStyle = null;

		private static GUIStyle propertyBoxGUIStyle = null;

		private static GUIStyle preferencesPaneGUIStyle = null;

		private static GUIStyle plainButtonGUIStyle = null;

		private static GUIStyle transparentButtonGUIStyle = null;

		private static GUIStyle transparentButtonOffsetGUIStyle = null;

		private static GUIStyle buttonGUIStyle = null;

		private static GUIStyle plainTextureGUIStyle = null;

		private static GUIStyle arrowSeparatorGUIStyle = null;

		private static GUIStyle selectedBackgroundGUIStyle = null;

		private static GUIStyle errorListDarkBackground = null;

		private static GUIStyle errorListLightBackground = null;

		private static GUIStyle welcomeScreenIntroGUIStyle = null;

		private static GUIStyle welcomeScreenTextHeaderGUIStyle = null;

		private static GUIStyle welcomeScreenTextDescriptionGUIStyle = null;

		private static Texture2D[] taskBorderTexture = new Texture2D[9];

		private static Texture2D taskBorderRunningTexture = null;

		private static Texture2D taskBorderIdentifyTexture = null;

		private static Texture2D[] taskConnectionTopTexture = new Texture2D[9];

		private static Texture2D[] taskConnectionBottomTexture = new Texture2D[9];

		private static Texture2D taskConnectionRunningTopTexture = null;

		private static Texture2D taskConnectionRunningBottomTexture = null;

		private static Texture2D taskConnectionIdentifyTopTexture = null;

		private static Texture2D taskConnectionIdentifyBottomTexture = null;

		private static Texture2D taskConnectionCollapsedTexture = null;

		private static Texture2D contentSeparatorTexture = null;

		private static Texture2D docTexture = null;

		private static Texture2D gearTexture = null;

		private static Texture2D[] colorSelectorTexture = new Texture2D[9];

		private static Texture2D variableButtonTexture = null;

		private static Texture2D variableButtonSelectedTexture = null;

		private static Texture2D variableWatchButtonTexture = null;

		private static Texture2D variableWatchButtonSelectedTexture = null;

		private static Texture2D referencedTexture = null;

		private static Texture2D conditionalAbortSelfTexture = null;

		private static Texture2D conditionalAbortLowerPriorityTexture = null;

		private static Texture2D conditionalAbortBothTexture = null;

		private static Texture2D deleteButtonTexture = null;

		private static Texture2D variableDeleteButtonTexture = null;

		private static Texture2D downArrowButtonTexture = null;

		private static Texture2D upArrowButtonTexture = null;

		private static Texture2D variableMapButtonTexture = null;

		private static Texture2D identifyButtonTexture = null;

		private static Texture2D breakpointTexture = null;

		private static Texture2D errorIconTexture = null;

		private static Texture2D smallErrorIconTexture = null;

		private static Texture2D enableTaskTexture = null;

		private static Texture2D disableTaskTexture = null;

		private static Texture2D expandTaskTexture = null;

		private static Texture2D collapseTaskTexture = null;

		private static Texture2D executionSuccessTexture = null;

		private static Texture2D executionFailureTexture = null;

		private static Texture2D executionSuccessRepeatTexture = null;

		private static Texture2D executionFailureRepeatTexture = null;

		public static Texture2D historyBackwardTexture = null;

		public static Texture2D historyForwardTexture = null;

		private static Texture2D playTexture = null;

		private static Texture2D pauseTexture = null;

		private static Texture2D stepTexture = null;

        private static Texture2D playHeadTexture = null;

		private static Texture2D screenshotBackgroundTexture = null;

		private static Regex camelCaseRegex = new Regex("(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

		private static Dictionary<string, string> camelCaseSplit = new Dictionary<string, string>();

		[NonSerialized]
		private static Dictionary<Type, Dictionary<FieldInfo, bool>> attributeFieldCache = new Dictionary<Type, Dictionary<FieldInfo, bool>>();

		private static Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();

		private static Dictionary<string, Texture2D> iconCache = new Dictionary<string, Texture2D>();

		public static GUIStyle GraphStatusGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.graphStatusGUIStyle == null)
				{
					BehaviorDesignerUtility.InitGraphStatusGUIStyle();
				}
				return BehaviorDesignerUtility.graphStatusGUIStyle;
			}
		}

		public static GUIStyle TaskFoldoutGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskFoldoutGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskFoldoutGUIStyle();
				}
				return BehaviorDesignerUtility.taskFoldoutGUIStyle;
			}
		}

		public static GUIStyle TaskTitleGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskTitleGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskTitleGUIStyle();
				}
				return BehaviorDesignerUtility.taskTitleGUIStyle;
			}
		}

		public static GUIStyle TaskRunningGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskRunningGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskRunningGUIStyle();
				}
				return BehaviorDesignerUtility.taskRunningGUIStyle;
			}
		}

		public static GUIStyle TaskRunningCompactGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskRunningCompactGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskRunningCompactGUIStyle();
				}
				return BehaviorDesignerUtility.taskRunningCompactGUIStyle;
			}
		}

		public static GUIStyle TaskRunningSelectedGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskRunningSelectedGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskRunningSelectedGUIStyle();
				}
				return BehaviorDesignerUtility.taskRunningSelectedGUIStyle;
			}
		}

		public static GUIStyle TaskRunningSelectedCompactGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskRunningSelectedCompactGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskRunningSelectedCompactGUIStyle();
				}
				return BehaviorDesignerUtility.taskRunningSelectedCompactGUIStyle;
			}
		}

		public static GUIStyle TaskIdentifyGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskIdentifyGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskIdentifyGUIStyle();
				}
				return BehaviorDesignerUtility.taskIdentifyGUIStyle;
			}
		}

		public static GUIStyle TaskIdentifyCompactGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskIdentifyCompactGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskIdentifyCompactGUIStyle();
				}
				return BehaviorDesignerUtility.taskIdentifyCompactGUIStyle;
			}
		}

		public static GUIStyle TaskIdentifySelectedGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskIdentifySelectedGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskIdentifySelectedGUIStyle();
				}
				return BehaviorDesignerUtility.taskIdentifySelectedGUIStyle;
			}
		}

		public static GUIStyle TaskIdentifySelectedCompactGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskIdentifySelectedCompactGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskIdentifySelectedCompactGUIStyle();
				}
				return BehaviorDesignerUtility.taskIdentifySelectedCompactGUIStyle;
			}
		}

		public static GUIStyle TaskHighlightGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskHighlightGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskHighlightGUIStyle();
				}
				return BehaviorDesignerUtility.taskHighlightGUIStyle;
			}
		}

		public static GUIStyle TaskHighlightCompactGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskHighlightCompactGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskHighlightCompactGUIStyle();
				}
				return BehaviorDesignerUtility.taskHighlightCompactGUIStyle;
			}
		}

		public static GUIStyle TaskCommentGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskCommentGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskCommentGUIStyle();
				}
				return BehaviorDesignerUtility.taskCommentGUIStyle;
			}
		}

		public static GUIStyle TaskCommentLeftAlignGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskCommentLeftAlignGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskCommentLeftAlignGUIStyle();
				}
				return BehaviorDesignerUtility.taskCommentLeftAlignGUIStyle;
			}
		}

		public static GUIStyle TaskCommentRightAlignGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskCommentRightAlignGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskCommentRightAlignGUIStyle();
				}
				return BehaviorDesignerUtility.taskCommentRightAlignGUIStyle;
			}
		}

		public static GUIStyle TaskDescriptionGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskDescriptionGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskDescriptionGUIStyle();
				}
				return BehaviorDesignerUtility.taskDescriptionGUIStyle;
			}
		}

		public static GUIStyle GraphBackgroundGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.graphBackgroundGUIStyle == null)
				{
					BehaviorDesignerUtility.InitGraphBackgroundGUIStyle();
				}
				return BehaviorDesignerUtility.graphBackgroundGUIStyle;
			}
		}

		public static GUIStyle SelectionGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.selectionGUIStyle == null)
				{
					BehaviorDesignerUtility.InitSelectionGUIStyle();
				}
				return BehaviorDesignerUtility.selectionGUIStyle;
			}
		}

		public static GUIStyle SharedVariableToolbarPopup
		{
			get
			{
				if (BehaviorDesignerUtility.sharedVariableToolbarPopup == null)
				{
					BehaviorDesignerUtility.InitSharedVariableToolbarPopup();
				}
				return BehaviorDesignerUtility.sharedVariableToolbarPopup;
			}
		}

		public static GUIStyle LabelWrapGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.labelWrapGUIStyle == null)
				{
					BehaviorDesignerUtility.InitLabelWrapGUIStyle();
				}
				return BehaviorDesignerUtility.labelWrapGUIStyle;
			}
		}

		public static GUIStyle ToolbarButtonLeftAlignGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.tolbarButtonLeftAlignGUIStyle == null)
				{
					BehaviorDesignerUtility.InitToolbarButtonLeftAlignGUIStyle();
				}
				return BehaviorDesignerUtility.tolbarButtonLeftAlignGUIStyle;
			}
		}

		public static GUIStyle ToolbarLabelGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.toolbarLabelGUIStyle == null)
				{
					BehaviorDesignerUtility.InitToolbarLabelGUIStyle();
				}
				return BehaviorDesignerUtility.toolbarLabelGUIStyle;
			}
		}

		public static GUIStyle TaskInspectorCommentGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskInspectorCommentGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskInspectorCommentGUIStyle();
				}
				return BehaviorDesignerUtility.taskInspectorCommentGUIStyle;
			}
		}

		public static GUIStyle TaskInspectorGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.taskInspectorGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTaskInspectorGUIStyle();
				}
				return BehaviorDesignerUtility.taskInspectorGUIStyle;
			}
		}

		public static GUIStyle ToolbarButtonSelectionGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.toolbarButtonSelectionGUIStyle == null)
				{
					BehaviorDesignerUtility.InitToolbarButtonSelectionGUIStyle();
				}
				return BehaviorDesignerUtility.toolbarButtonSelectionGUIStyle;
			}
		}

		public static GUIStyle PropertyBoxGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.propertyBoxGUIStyle == null)
				{
					BehaviorDesignerUtility.InitPropertyBoxGUIStyle();
				}
				return BehaviorDesignerUtility.propertyBoxGUIStyle;
			}
		}

		public static GUIStyle PreferencesPaneGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.preferencesPaneGUIStyle == null)
				{
					BehaviorDesignerUtility.InitPreferencesPaneGUIStyle();
				}
				return BehaviorDesignerUtility.preferencesPaneGUIStyle;
			}
		}

		public static GUIStyle PlainButtonGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.plainButtonGUIStyle == null)
				{
					BehaviorDesignerUtility.InitPlainButtonGUIStyle();
				}
				return BehaviorDesignerUtility.plainButtonGUIStyle;
			}
		}

		public static GUIStyle TransparentButtonGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.transparentButtonGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTransparentButtonGUIStyle();
				}
				return BehaviorDesignerUtility.transparentButtonGUIStyle;
			}
		}

		public static GUIStyle TransparentButtonOffsetGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.transparentButtonOffsetGUIStyle == null)
				{
					BehaviorDesignerUtility.InitTransparentButtonOffsetGUIStyle();
				}
				return BehaviorDesignerUtility.transparentButtonOffsetGUIStyle;
			}
		}

		public static GUIStyle ButtonGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.buttonGUIStyle == null)
				{
					BehaviorDesignerUtility.InitButtonGUIStyle();
				}
				return BehaviorDesignerUtility.buttonGUIStyle;
			}
		}

		public static GUIStyle PlainTextureGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.plainTextureGUIStyle == null)
				{
					BehaviorDesignerUtility.InitPlainTextureGUIStyle();
				}
				return BehaviorDesignerUtility.plainTextureGUIStyle;
			}
		}

		public static GUIStyle ArrowSeparatorGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.arrowSeparatorGUIStyle == null)
				{
					BehaviorDesignerUtility.InitArrowSeparatorGUIStyle();
				}
				return BehaviorDesignerUtility.arrowSeparatorGUIStyle;
			}
		}

		public static GUIStyle SelectedBackgroundGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.selectedBackgroundGUIStyle == null)
				{
					BehaviorDesignerUtility.InitSelectedBackgroundGUIStyle();
				}
				return BehaviorDesignerUtility.selectedBackgroundGUIStyle;
			}
		}

		public static GUIStyle ErrorListDarkBackground
		{
			get
			{
				if (BehaviorDesignerUtility.errorListDarkBackground == null)
				{
					BehaviorDesignerUtility.InitErrorListDarkBackground();
				}
				return BehaviorDesignerUtility.errorListDarkBackground;
			}
		}

		public static GUIStyle ErrorListLightBackground
		{
			get
			{
				if (BehaviorDesignerUtility.errorListLightBackground == null)
				{
					BehaviorDesignerUtility.InitErrorListLightBackground();
				}
				return BehaviorDesignerUtility.errorListLightBackground;
			}
		}

		public static GUIStyle WelcomeScreenIntroGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.welcomeScreenIntroGUIStyle == null)
				{
					BehaviorDesignerUtility.InitWelcomeScreenIntroGUIStyle();
				}
				return BehaviorDesignerUtility.welcomeScreenIntroGUIStyle;
			}
		}

		public static GUIStyle WelcomeScreenTextHeaderGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.welcomeScreenTextHeaderGUIStyle == null)
				{
					BehaviorDesignerUtility.InitWelcomeScreenTextHeaderGUIStyle();
				}
				return BehaviorDesignerUtility.welcomeScreenTextHeaderGUIStyle;
			}
		}

		public static GUIStyle WelcomeScreenTextDescriptionGUIStyle
		{
			get
			{
				if (BehaviorDesignerUtility.welcomeScreenTextDescriptionGUIStyle == null)
				{
					BehaviorDesignerUtility.InitWelcomeScreenTextDescriptionGUIStyle();
				}
				return BehaviorDesignerUtility.welcomeScreenTextDescriptionGUIStyle;
			}
		}

		public static Texture2D TaskBorderRunningTexture
		{
			get
			{
				if (BehaviorDesignerUtility.taskBorderRunningTexture == null)
				{
					BehaviorDesignerUtility.InitTaskBorderRunningTexture();
				}
				return BehaviorDesignerUtility.taskBorderRunningTexture;
			}
		}

		public static Texture2D TaskBorderIdentifyTexture
		{
			get
			{
				if (BehaviorDesignerUtility.taskBorderIdentifyTexture == null)
				{
					BehaviorDesignerUtility.InitTaskBorderIdentifyTexture();
				}
				return BehaviorDesignerUtility.taskBorderIdentifyTexture;
			}
		}

		public static Texture2D TaskConnectionRunningTopTexture
		{
			get
			{
				if (BehaviorDesignerUtility.taskConnectionRunningTopTexture == null)
				{
					BehaviorDesignerUtility.InitTaskConnectionRunningTopTexture();
				}
				return BehaviorDesignerUtility.taskConnectionRunningTopTexture;
			}
		}

		public static Texture2D TaskConnectionRunningBottomTexture
		{
			get
			{
				if (BehaviorDesignerUtility.taskConnectionRunningBottomTexture == null)
				{
					BehaviorDesignerUtility.InitTaskConnectionRunningBottomTexture();
				}
				return BehaviorDesignerUtility.taskConnectionRunningBottomTexture;
			}
		}

		public static Texture2D TaskConnectionIdentifyTopTexture
		{
			get
			{
				if (BehaviorDesignerUtility.taskConnectionIdentifyTopTexture == null)
				{
					BehaviorDesignerUtility.InitTaskConnectionIdentifyTopTexture();
				}
				return BehaviorDesignerUtility.taskConnectionIdentifyTopTexture;
			}
		}

		public static Texture2D TaskConnectionIdentifyBottomTexture
		{
			get
			{
				if (BehaviorDesignerUtility.taskConnectionIdentifyBottomTexture == null)
				{
					BehaviorDesignerUtility.InitTaskConnectionIdentifyBottomTexture();
				}
				return BehaviorDesignerUtility.taskConnectionIdentifyBottomTexture;
			}
		}

		public static Texture2D TaskConnectionCollapsedTexture
		{
			get
			{
				if (BehaviorDesignerUtility.taskConnectionCollapsedTexture == null)
				{
					BehaviorDesignerUtility.InitTaskConnectionCollapsedTexture();
				}
				return BehaviorDesignerUtility.taskConnectionCollapsedTexture;
			}
		}

		public static Texture2D ContentSeparatorTexture
		{
			get
			{
				if (contentSeparatorTexture == null)
				{
					InitContentSeparatorTexture();
				}
				return contentSeparatorTexture;
			}
		}

		public static Texture2D DocTexture
		{
			get
			{
				if (BehaviorDesignerUtility.docTexture == null)
				{
					BehaviorDesignerUtility.InitDocTexture();
				}
				return BehaviorDesignerUtility.docTexture;
			}
		}

		public static Texture2D GearTexture
		{
			get
			{
				if (BehaviorDesignerUtility.gearTexture == null)
				{
					BehaviorDesignerUtility.InitGearTexture();
				}
				return BehaviorDesignerUtility.gearTexture;
			}
		}

		public static Texture2D VariableButtonTexture
		{
			get
			{
				if (BehaviorDesignerUtility.variableButtonTexture == null)
				{
					BehaviorDesignerUtility.InitVariableButtonTexture();
				}
				return BehaviorDesignerUtility.variableButtonTexture;
			}
		}

		public static Texture2D VariableButtonSelectedTexture
		{
			get
			{
				if (BehaviorDesignerUtility.variableButtonSelectedTexture == null)
				{
					BehaviorDesignerUtility.InitVariableButtonSelectedTexture();
				}
				return BehaviorDesignerUtility.variableButtonSelectedTexture;
			}
		}

		public static Texture2D VariableWatchButtonTexture
		{
			get
			{
				if (BehaviorDesignerUtility.variableWatchButtonTexture == null)
				{
					BehaviorDesignerUtility.InitVariableWatchButtonTexture();
				}
				return BehaviorDesignerUtility.variableWatchButtonTexture;
			}
		}

		public static Texture2D VariableWatchButtonSelectedTexture
		{
			get
			{
				if (BehaviorDesignerUtility.variableWatchButtonSelectedTexture == null)
				{
					BehaviorDesignerUtility.InitVariableWatchButtonSelectedTexture();
				}
				return BehaviorDesignerUtility.variableWatchButtonSelectedTexture;
			}
		}

		public static Texture2D ReferencedTexture
		{
			get
			{
				if (BehaviorDesignerUtility.referencedTexture == null)
				{
					BehaviorDesignerUtility.InitReferencedTexture();
				}
				return BehaviorDesignerUtility.referencedTexture;
			}
		}

		public static Texture2D ConditionalAbortSelfTexture
		{
			get
			{
				if (BehaviorDesignerUtility.conditionalAbortSelfTexture == null)
				{
					BehaviorDesignerUtility.InitConditionalAbortSelfTexture();
				}
				return BehaviorDesignerUtility.conditionalAbortSelfTexture;
			}
		}

		public static Texture2D ConditionalAbortLowerPriorityTexture
		{
			get
			{
				if (BehaviorDesignerUtility.conditionalAbortLowerPriorityTexture == null)
				{
					BehaviorDesignerUtility.InitConditionalAbortLowerPriorityTexture();
				}
				return BehaviorDesignerUtility.conditionalAbortLowerPriorityTexture;
			}
		}

		public static Texture2D ConditionalAbortBothTexture
		{
			get
			{
				if (BehaviorDesignerUtility.conditionalAbortBothTexture == null)
				{
					BehaviorDesignerUtility.InitConditionalAbortBothTexture();
				}
				return BehaviorDesignerUtility.conditionalAbortBothTexture;
			}
		}

		public static Texture2D DeleteButtonTexture
		{
			get
			{
				if (BehaviorDesignerUtility.deleteButtonTexture == null)
				{
					BehaviorDesignerUtility.InitDeleteButtonTexture();
				}
				return BehaviorDesignerUtility.deleteButtonTexture;
			}
		}

		public static Texture2D VariableDeleteButtonTexture
		{
			get
			{
				if (BehaviorDesignerUtility.variableDeleteButtonTexture == null)
				{
					BehaviorDesignerUtility.InitVariableDeleteButtonTexture();
				}
				return BehaviorDesignerUtility.variableDeleteButtonTexture;
			}
		}

		public static Texture2D DownArrowButtonTexture
		{
			get
			{
				if (BehaviorDesignerUtility.downArrowButtonTexture == null)
				{
					BehaviorDesignerUtility.InitDownArrowButtonTexture();
				}
				return BehaviorDesignerUtility.downArrowButtonTexture;
			}
		}

		public static Texture2D UpArrowButtonTexture
		{
			get
			{
				if (BehaviorDesignerUtility.upArrowButtonTexture == null)
				{
					BehaviorDesignerUtility.InitUpArrowButtonTexture();
				}
				return BehaviorDesignerUtility.upArrowButtonTexture;
			}
		}

		public static Texture2D VariableMapButtonTexture
		{
			get
			{
				if (BehaviorDesignerUtility.variableMapButtonTexture == null)
				{
					BehaviorDesignerUtility.InitVariableMapButtonTexture();
				}
				return BehaviorDesignerUtility.variableMapButtonTexture;
			}
		}

		public static Texture2D IdentifyButtonTexture
		{
			get
			{
				if (BehaviorDesignerUtility.identifyButtonTexture == null)
				{
					BehaviorDesignerUtility.InitIdentifyButtonTexture();
				}
				return BehaviorDesignerUtility.identifyButtonTexture;
			}
		}

		public static Texture2D BreakpointTexture
		{
			get
			{
				if (BehaviorDesignerUtility.breakpointTexture == null)
				{
					BehaviorDesignerUtility.InitBreakpointTexture();
				}
				return BehaviorDesignerUtility.breakpointTexture;
			}
		}

		public static Texture2D ErrorIconTexture
		{
			get
			{
				if (BehaviorDesignerUtility.errorIconTexture == null)
				{
					BehaviorDesignerUtility.InitErrorIconTexture();
				}
				return BehaviorDesignerUtility.errorIconTexture;
			}
		}

		public static Texture2D SmallErrorIconTexture
		{
			get
			{
				if (BehaviorDesignerUtility.smallErrorIconTexture == null)
				{
					BehaviorDesignerUtility.InitSmallErrorIconTexture();
				}
				return BehaviorDesignerUtility.smallErrorIconTexture;
			}
		}

		public static Texture2D EnableTaskTexture
		{
			get
			{
				if (BehaviorDesignerUtility.enableTaskTexture == null)
				{
					BehaviorDesignerUtility.InitEnableTaskTexture();
				}
				return BehaviorDesignerUtility.enableTaskTexture;
			}
		}

		public static Texture2D DisableTaskTexture
		{
			get
			{
				if (BehaviorDesignerUtility.disableTaskTexture == null)
				{
					BehaviorDesignerUtility.InitDisableTaskTexture();
				}
				return BehaviorDesignerUtility.disableTaskTexture;
			}
		}

		public static Texture2D ExpandTaskTexture
		{
			get
			{
				if (BehaviorDesignerUtility.expandTaskTexture == null)
				{
					BehaviorDesignerUtility.InitExpandTaskTexture();
				}
				return BehaviorDesignerUtility.expandTaskTexture;
			}
		}

		public static Texture2D CollapseTaskTexture
		{
			get
			{
				if (BehaviorDesignerUtility.collapseTaskTexture == null)
				{
					BehaviorDesignerUtility.InitCollapseTaskTexture();
				}
				return BehaviorDesignerUtility.collapseTaskTexture;
			}
		}

		public static Texture2D ExecutionSuccessTexture
		{
			get
			{
				if (BehaviorDesignerUtility.executionSuccessTexture == null)
				{
					BehaviorDesignerUtility.InitExecutionSuccessTexture();
				}
				return BehaviorDesignerUtility.executionSuccessTexture;
			}
		}

		public static Texture2D ExecutionFailureTexture
		{
			get
			{
				if (BehaviorDesignerUtility.executionFailureTexture == null)
				{
					BehaviorDesignerUtility.InitExecutionFailureTexture();
				}
				return BehaviorDesignerUtility.executionFailureTexture;
			}
		}

		public static Texture2D ExecutionSuccessRepeatTexture
		{
			get
			{
				if (BehaviorDesignerUtility.executionSuccessRepeatTexture == null)
				{
					BehaviorDesignerUtility.InitExecutionSuccessRepeatTexture();
				}
				return BehaviorDesignerUtility.executionSuccessRepeatTexture;
			}
		}

		public static Texture2D ExecutionFailureRepeatTexture
		{
			get
			{
				if (BehaviorDesignerUtility.executionFailureRepeatTexture == null)
				{
					BehaviorDesignerUtility.InitExecutionFailureRepeatTexture();
				}
				return BehaviorDesignerUtility.executionFailureRepeatTexture;
			}
		}

		public static Texture2D HistoryBackwardTexture
		{
			get
			{
				if (BehaviorDesignerUtility.historyBackwardTexture == null)
				{
					BehaviorDesignerUtility.InitHistoryBackwardTexture();
				}
				return BehaviorDesignerUtility.historyBackwardTexture;
			}
		}

		public static Texture2D HistoryForwardTexture
		{
			get
			{
				if (historyForwardTexture == null)
				{
					InitHistoryForwardTexture();
				}
				return historyForwardTexture;
			}
		}

		public static Texture2D PlayTexture
		{
			get
			{
				if (playTexture == null)
				{
					InitPlayTexture();
				}
				return playTexture;
			}
		}

        public static Texture2D PlayHeadTexture
        {
            get
            {
                if (playHeadTexture == null)
                {
                    InitPlayHeadTexture();
                }
                return playHeadTexture;
            }
        }

		public static Texture2D PauseTexture
		{
			get
			{
				if (pauseTexture == null)
				{
					InitPauseTexture();
				}
				return pauseTexture;
			}
		}

		public static Texture2D StepTexture
		{
			get
			{
				if (stepTexture == null)
				{
					InitStepTexture();
				}
				return stepTexture;
			}
		}

		public static Texture2D ScreenshotBackgroundTexture
		{
			get
			{
				if (screenshotBackgroundTexture == null)
				{
					InitScreenshotBackgroundTexture();
				}
				return screenshotBackgroundTexture;
			}
		}

		public static GUIStyle GetTaskGUIStyle(int colorIndex)
		{
			if (taskGUIStyle[colorIndex] == null)
			{
				InitTaskGUIStyle(colorIndex);
			}
			return taskGUIStyle[colorIndex];
		}

		public static GUIStyle GetTaskCompactGUIStyle(int colorIndex)
		{
			if (BehaviorDesignerUtility.taskCompactGUIStyle[colorIndex] == null)
			{
				BehaviorDesignerUtility.InitTaskCompactGUIStyle(colorIndex);
			}
			return BehaviorDesignerUtility.taskCompactGUIStyle[colorIndex];
		}

		public static GUIStyle GetTaskSelectedGUIStyle(int colorIndex)
		{
			if (BehaviorDesignerUtility.taskSelectedGUIStyle[colorIndex] == null)
			{
				BehaviorDesignerUtility.InitTaskSelectedGUIStyle(colorIndex);
			}
			return BehaviorDesignerUtility.taskSelectedGUIStyle[colorIndex];
		}

		public static GUIStyle GetTaskSelectedCompactGUIStyle(int colorIndex)
		{
			if (BehaviorDesignerUtility.taskSelectedCompactGUIStyle[colorIndex] == null)
			{
				BehaviorDesignerUtility.InitTaskSelectedCompactGUIStyle(colorIndex);
			}
			return BehaviorDesignerUtility.taskSelectedCompactGUIStyle[colorIndex];
		}

		public static Texture2D GetTaskBorderTexture(int colorIndex)
		{
			if (BehaviorDesignerUtility.taskBorderTexture[colorIndex] == null)
			{
				BehaviorDesignerUtility.InitTaskBorderTexture(colorIndex);
			}
			return BehaviorDesignerUtility.taskBorderTexture[colorIndex];
		}

		public static Texture2D GetTaskConnectionTopTexture(int colorIndex)
		{
			if (BehaviorDesignerUtility.taskConnectionTopTexture[colorIndex] == null)
			{
				BehaviorDesignerUtility.InitTaskConnectionTopTexture(colorIndex);
			}
			return BehaviorDesignerUtility.taskConnectionTopTexture[colorIndex];
		}

		public static Texture2D GetTaskConnectionBottomTexture(int colorIndex)
		{
			if (BehaviorDesignerUtility.taskConnectionBottomTexture[colorIndex] == null)
			{
				BehaviorDesignerUtility.InitTaskConnectionBottomTexture(colorIndex);
			}
			return BehaviorDesignerUtility.taskConnectionBottomTexture[colorIndex];
		}

		public static Texture2D ColorSelectorTexture(int colorIndex)
		{
			if (BehaviorDesignerUtility.colorSelectorTexture[colorIndex] == null)
			{
				BehaviorDesignerUtility.InitColorSelectorTexture(colorIndex);
			}
			return BehaviorDesignerUtility.colorSelectorTexture[colorIndex];
		}

		public static string SplitCamelCase(string s)
		{
			if (s.Equals(string.Empty))
			{
				return s;
			}
			if (BehaviorDesignerUtility.camelCaseSplit.ContainsKey(s))
			{
				return BehaviorDesignerUtility.camelCaseSplit[s];
			}
			string key = s;
			s = s.Replace("_uScript", "uScript");
			s = s.Replace("_PlayMaker", "PlayMaker");
			if (s.Length > 2 && s.Substring(0, 2).CompareTo("m_") == 0)
			{
				s = s.Substring(2, s.Length - 2);
			}
			s = BehaviorDesignerUtility.camelCaseRegex.Replace(s, " ");
			s = s.Replace("_", " ");
			s = s.Replace("u Script", " uScript");
			s = s.Replace("Play Maker", "PlayMaker");
			s = (char.ToUpper(s[0]) + s.Substring(1)).Trim();
			BehaviorDesignerUtility.camelCaseSplit.Add(key, s);
			return s;
		}

		public static bool HasAttribute(FieldInfo field, Type attributeType)
		{
			Dictionary<FieldInfo, bool> dictionary = null;
			if (BehaviorDesignerUtility.attributeFieldCache.ContainsKey(attributeType))
			{
				dictionary = BehaviorDesignerUtility.attributeFieldCache[attributeType];
			}
			if (dictionary == null)
			{
				dictionary = new Dictionary<FieldInfo, bool>();
			}
			if (dictionary.ContainsKey(field))
			{
				return dictionary[field];
			}
			bool flag = field.GetCustomAttributes(attributeType, false).Length > 0;
			dictionary.Add(field, flag);
			if (!BehaviorDesignerUtility.attributeFieldCache.ContainsKey(attributeType))
			{
				BehaviorDesignerUtility.attributeFieldCache.Add(attributeType, dictionary);
			}
			return flag;
		}

		public static List<Task> GetAllTasks(BehaviorSource behaviorSource)
		{
			List<Task> result = new List<Task>();
			if (behaviorSource.RootTask != null)
			{
				BehaviorDesignerUtility.GetAllTasks(behaviorSource.RootTask, ref result);
			}
			if (behaviorSource.DetachedTasks != null)
			{
				for (int i = 0; i < behaviorSource.DetachedTasks.Count; i++)
				{
					BehaviorDesignerUtility.GetAllTasks(behaviorSource.DetachedTasks[i], ref result);
				}
			}
			return result;
		}

		private static void GetAllTasks(Task task, ref List<Task> taskList)
		{
			taskList.Add(task);
			ParentTask parentTask;
			if ((parentTask = (task as ParentTask)) != null && parentTask.Children != null)
			{
				for (int i = 0; i < parentTask.Children.Count; i++)
				{
					BehaviorDesignerUtility.GetAllTasks(parentTask.Children[i], ref taskList);
				}
			}
		}

		public static bool AnyNullTasks(BehaviorSource behaviorSource)
		{
			if (behaviorSource.RootTask != null && BehaviorDesignerUtility.AnyNullTasks(behaviorSource.RootTask))
			{
				return true;
			}
			if (behaviorSource.DetachedTasks != null)
			{
				for (int i = 0; i < behaviorSource.DetachedTasks.Count; i++)
				{
					if (BehaviorDesignerUtility.AnyNullTasks(behaviorSource.DetachedTasks[i]))
					{
						return true;
					}
				}
			}
			return false;
		}

		private static bool AnyNullTasks(Task task)
		{
			if (task == null)
			{
				return true;
			}
			ParentTask parentTask;
			if ((parentTask = (task as ParentTask)) != null && parentTask.Children != null)
			{
				for (int i = 0; i < parentTask.Children.Count; i++)
				{
					if (BehaviorDesignerUtility.AnyNullTasks(parentTask.Children[i]))
					{
						return true;
					}
				}
			}
			return false;
		}

		public static bool HasRootTask(string serialization)
		{
			if (string.IsNullOrEmpty(serialization))
			{
				return false;
			}
            return MiniJSON.Json.Deserialize(serialization) is Dictionary<string, object> dictionary && dictionary.ContainsKey("RootTask");
        }

		public static string GetEditorBaseDirectory(UnityEngine.Object obj = null)
		{
			string codeBase = Assembly.GetExecutingAssembly().CodeBase;
			string text = Uri.UnescapeDataString(new UriBuilder(codeBase).Path);
			return Path.GetDirectoryName(text.Substring(Application.dataPath.Length - 6));
		}

		public static Texture2D LoadTexture(string imageName, bool useSkinColor = true)
		{
		    if (textureCache.TryGetValue(imageName, out Texture2D tex))
		    {
		        if (tex == null)
		        {
		            textureCache.Remove(imageName);
		        }
		        else
		        {
		            return tex;
		        }
		    }
			
			Texture2D texture2D = null;
			string name = string.Format("{0}{1}", !useSkinColor ? string.Empty : ((!EditorGUIUtility.isProSkin) ? "Light" : "Dark"), imageName);
			texture2D = Resources.Load<Texture2D>(name);
            if (texture2D != null)
			    texture2D.hideFlags = HideFlags.HideAndDontSave;
            else
            {
                return null;
            }
			textureCache.Add(imageName, texture2D);
			return texture2D;
		}

		private static Texture2D LoadTaskTexture(string imageName, bool useSkinColor = true, ScriptableObject obj = null)
		{
			if (textureCache.ContainsKey(imageName))
			{
				return textureCache[imageName];
			}
			Texture2D texture2D = null;
			string name = string.Format("{0}{1}", (!useSkinColor) ? string.Empty : ((!EditorGUIUtility.isProSkin) ? "Light" : "Dark"), imageName);
			texture2D = Resources.Load<Texture2D>(name);
			if (texture2D == null)
			{
				Debug.Log(string.Format("{0}/Images/Task Backgrounds/{1}{2}", GetEditorBaseDirectory(obj), (!useSkinColor) ? string.Empty : ((!EditorGUIUtility.isProSkin) ? "Light" : "Dark"), imageName));
			    return null;
			}
			texture2D.hideFlags= HideFlags.HideAndDontSave;
			textureCache.Add(imageName, texture2D);
			return texture2D;
		}

		public static Texture2D LoadIcon(string iconName)
		{
			if (iconCache.ContainsKey(iconName))
			{
				return iconCache[iconName];
			}
			Texture2D texture2D = null;
			string name = iconName.Replace("{SkinColor}", (!EditorGUIUtility.isProSkin) ? "Light" : "Dark");
			texture2D = Resources.Load<Texture2D>(name);
			if (texture2D == null)
			{
				texture2D = Resources.Load<Texture2D>(iconName.Replace("{SkinColor}", !EditorGUIUtility.isProSkin ? "Light" : "Dark"));
			}
			if (texture2D != null)
			{
				texture2D.hideFlags = HideFlags.HideAndDontSave;
			}
			else
			{
			    return null;
			}
			iconCache.Add(iconName, texture2D);
			return texture2D;
		}

		private static byte[] ReadToEnd(Stream stream)
		{
			byte[] array = new byte[16384];
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int count;
				while ((count = stream.Read(array, 0, array.Length)) > 0)
				{
					memoryStream.Write(array, 0, count);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		public static void DrawContentSeperator(int yOffset)
		{
			DrawContentSeperator(yOffset, 0);
		}

		public static void DrawContentSeperator(int yOffset, int widthExtension)
		{
			Rect lastRect = GUILayoutUtility.GetLastRect();
			lastRect.x=(-5f);
			lastRect.y=(lastRect.y + (lastRect.height + (float)yOffset));
			lastRect.height = 2f;
			lastRect.width = lastRect.width + 10 + widthExtension;
			GUI.DrawTexture(lastRect, ContentSeparatorTexture);
		}

		public static float RoundToNearest(float num, float baseNum)
		{
			return (float)((int)Math.Round((double)(num / baseNum), MidpointRounding.AwayFromZero)) * baseNum;
		}

		private static void InitGraphStatusGUIStyle()
		{
            graphStatusGUIStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleLeft,
                fontSize = 20,
                fontStyle = FontStyle.Normal
            };
            if (EditorGUIUtility.isProSkin)
			{
				graphStatusGUIStyle.normal.textColor=new Color(0.7058f, 0.7058f, 0.7058f);
			}
			else
			{
				graphStatusGUIStyle.normal.textColor=(new Color(0.8058f, 0.8058f, 0.8058f));
			}
		}

		private static void InitTaskFoldoutGUIStyle()
		{
            taskFoldoutGUIStyle = new GUIStyle(EditorStyles.foldout)
            {
                alignment = TextAnchor.MiddleLeft,
                fontSize = 13,
                fontStyle = FontStyle.Normal
            };
        }

		private static void InitTaskTitleGUIStyle()
		{
            taskTitleGUIStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.UpperCenter,
                fontSize = 12,
                fontStyle = FontStyle.Normal
            };
        }

		private static void InitTaskGUIStyle(int colorIndex)
		{
			taskGUIStyle[colorIndex] = InitTaskGUIStyle(LoadTaskTexture("Task" + ColorIndexToColorString(colorIndex) + ".png", true, null), new RectOffset(5, 3, 3, 5));
		}

		private static void InitTaskCompactGUIStyle(int colorIndex)
		{
			taskCompactGUIStyle[colorIndex] = InitTaskGUIStyle(LoadTaskTexture("TaskCompact" + ColorIndexToColorString(colorIndex) + ".png", true, null), new RectOffset(5, 4, 4, 5));
		}

		private static void InitTaskSelectedGUIStyle(int colorIndex)
		{
			taskSelectedGUIStyle[colorIndex] = InitTaskGUIStyle(LoadTaskTexture("TaskSelected" + ColorIndexToColorString(colorIndex) + ".png", true, null), new RectOffset(5, 4, 4, 4));
		}

		private static void InitTaskSelectedCompactGUIStyle(int colorIndex)
		{
			taskSelectedCompactGUIStyle[colorIndex] = InitTaskGUIStyle(LoadTaskTexture("TaskSelectedCompact" + ColorIndexToColorString(colorIndex) + ".png", true, null), new RectOffset(5, 4, 4, 4));
		}

		private static string ColorIndexToColorString(int index)
		{
			if (index == 0)
			{
				return string.Empty;
			}
			if (index == 1)
			{
				return "Red";
			}
			if (index == 2)
			{
				return "Pink";
			}
			if (index == 3)
			{
				return "Brown";
			}
			if (index == 4)
			{
				return "RedOrange";
			}
			if (index == 5)
			{
				return "Turquoise";
			}
			if (index == 6)
			{
				return "Cyan";
			}
			if (index == 7)
			{
				return "Blue";
			}
			if (index == 8)
			{
				return "Purple";
			}
			return string.Empty;
		}

		private static void InitTaskRunningGUIStyle()
		{
			taskRunningGUIStyle = InitTaskGUIStyle(LoadTaskTexture("TaskRunning.png", true, null), new RectOffset(5, 3, 3, 5));
		}

		private static void InitTaskRunningCompactGUIStyle()
		{
			taskRunningCompactGUIStyle = InitTaskGUIStyle(LoadTaskTexture("TaskRunningCompact.png", true, null), new RectOffset(5, 4, 4, 5));
		}

		private static void InitTaskRunningSelectedGUIStyle()
		{
			taskRunningSelectedGUIStyle = InitTaskGUIStyle(LoadTaskTexture("TaskRunningSelected.png", true, null), new RectOffset(5, 4, 4, 4));
		}

		private static void InitTaskRunningSelectedCompactGUIStyle()
		{
			taskRunningSelectedCompactGUIStyle = InitTaskGUIStyle(LoadTaskTexture("TaskRunningSelectedCompact.png", true, null), new RectOffset(5, 4, 4, 4));
		}

		private static void InitTaskIdentifyGUIStyle()
		{
			taskIdentifyGUIStyle = InitTaskGUIStyle(LoadTaskTexture("TaskIdentify.png", true, null), new RectOffset(5, 3, 3, 5));
		}

		private static void InitTaskIdentifyCompactGUIStyle()
		{
			taskIdentifyCompactGUIStyle = InitTaskGUIStyle(LoadTaskTexture("TaskIdentifyCompact.png", true, null), new RectOffset(5, 4, 4, 5));
		}

		private static void InitTaskIdentifySelectedGUIStyle()
		{
			taskIdentifySelectedGUIStyle = InitTaskGUIStyle(LoadTaskTexture("TaskIdentifySelected.png", true, null), new RectOffset(5, 4, 4, 4));
		}

		private static void InitTaskIdentifySelectedCompactGUIStyle()
		{
			taskIdentifySelectedCompactGUIStyle = InitTaskGUIStyle(LoadTaskTexture("TaskIdentifySelectedCompact.png", true, null), new RectOffset(5, 4, 4, 4));
		}

		private static void InitTaskHighlightGUIStyle()
		{
			taskHighlightGUIStyle = InitTaskGUIStyle(LoadTaskTexture("TaskHighlight.png", true, null), new RectOffset(5, 4, 4, 4));
		}

		private static void InitTaskHighlightCompactGUIStyle()
		{
			taskHighlightCompactGUIStyle = InitTaskGUIStyle(LoadTaskTexture("TaskHighlightCompact.png", true, null), new RectOffset(5, 4, 4, 4));
		}

		private static GUIStyle InitTaskGUIStyle(Texture2D texture, RectOffset overflow)
		{
            GUIStyle gUIStyle = new GUIStyle(GUI.skin.box)
            {
                border = (new RectOffset(10, 10, 10, 10)),
                overflow = (overflow)
            };
            gUIStyle.normal.background=(texture);
			gUIStyle.active.background=(texture);
			gUIStyle.hover.background=(texture);
			gUIStyle.focused.background=(texture);
			gUIStyle.normal.textColor=(Color.white);
			gUIStyle.active.textColor=(Color.white);
			gUIStyle.hover.textColor=(Color.white);
			gUIStyle.focused.textColor=(Color.white);
			gUIStyle.stretchHeight=(true);
			gUIStyle.stretchWidth=true;
			return gUIStyle;
		}

		private static void InitTaskCommentGUIStyle()
		{
            taskCommentGUIStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = (TextAnchor)(1),
                fontSize = (12),
                fontStyle = (0),
                wordWrap = true
            };
        }

		private static void InitTaskCommentLeftAlignGUIStyle()
		{
            taskCommentLeftAlignGUIStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = (0),
                fontSize = (12),
                fontStyle = (0),
                wordWrap = (false)
            };
        }

		private static void InitTaskCommentRightAlignGUIStyle()
		{
            taskCommentRightAlignGUIStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = (TextAnchor)(2),
                fontSize = (12),
                fontStyle = (0),
                wordWrap = (false)
            };
        }

		private static void InitTaskDescriptionGUIStyle()
		{
			Texture2D texture2D = new Texture2D(1, 1, (TextureFormat)4, false, true);
			if (EditorGUIUtility.isProSkin)
			{
				texture2D.SetPixel(1, 1, new Color(0.1647f, 0.1647f, 0.1647f));
			}
			else
			{
				texture2D.SetPixel(1, 1, new Color(0.75f, 0.75f, 0.75f));
			}
			texture2D.hideFlags= (HideFlags)(61);
			texture2D.Apply();
			taskDescriptionGUIStyle = new GUIStyle(GUI.skin.box);
			taskDescriptionGUIStyle.normal.background=(texture2D);
			taskDescriptionGUIStyle.active.background=(texture2D);
			taskDescriptionGUIStyle.hover.background=(texture2D);
			taskDescriptionGUIStyle.focused.background=(texture2D);
		}

		private static void InitGraphBackgroundGUIStyle()
		{
			Texture2D texture2D = new Texture2D(1, 1, (TextureFormat)4, false, true);
			if (EditorGUIUtility.isProSkin)
			{
				texture2D.SetPixel(1, 1, new Color(0.1647f, 0.1647f, 0.1647f));
			}
			else
			{
				texture2D.SetPixel(1, 1, new Color(0.3647f, 0.3647f, 0.3647f));
			}
			texture2D.hideFlags= (HideFlags)(61);
			texture2D.Apply();
			graphBackgroundGUIStyle = new GUIStyle(GUI.skin.box);
			graphBackgroundGUIStyle.normal.background=(texture2D);
			graphBackgroundGUIStyle.active.background=(texture2D);
			graphBackgroundGUIStyle.hover.background=(texture2D);
			graphBackgroundGUIStyle.focused.background=(texture2D);
			graphBackgroundGUIStyle.normal.textColor=(Color.white);
			graphBackgroundGUIStyle.active.textColor=(Color.white);
			graphBackgroundGUIStyle.hover.textColor=(Color.white);
			graphBackgroundGUIStyle.focused.textColor=(Color.white);
		}

		private static void InitSelectionGUIStyle()
		{
			Texture2D texture2D = new Texture2D(1, 1, (TextureFormat)4, false, true);
			Color color = (!EditorGUIUtility.isProSkin) ? new Color(0.243f, 0.5686f, 0.839f, 0.5f) : new Color(0.188f, 0.4588f, 0.6862f, 0.5f);
			texture2D.SetPixel(1, 1, color);
			texture2D.hideFlags=(HideFlags)(61);
			texture2D.Apply();
			selectionGUIStyle = new GUIStyle(GUI.skin.box);
			selectionGUIStyle.normal.background=(texture2D);
			selectionGUIStyle.active.background=(texture2D);
			selectionGUIStyle.hover.background=(texture2D);
			selectionGUIStyle.focused.background=(texture2D);
			selectionGUIStyle.normal.textColor=(Color.white);
			selectionGUIStyle.active.textColor=(Color.white);
			selectionGUIStyle.hover.textColor=(Color.white);
			selectionGUIStyle.focused.textColor=(Color.white);
		}

		private static void InitSharedVariableToolbarPopup()
		{
			sharedVariableToolbarPopup = new GUIStyle(EditorStyles.toolbarPopup);
			sharedVariableToolbarPopup.margin=(new RectOffset(4, 4, 0, 0));
		}

		private static void InitLabelWrapGUIStyle()
		{
			labelWrapGUIStyle = new GUIStyle(GUI.skin.label);
			labelWrapGUIStyle.wordWrap=(true);
			labelWrapGUIStyle.alignment= (TextAnchor)(4);
		}

		private static void InitToolbarButtonLeftAlignGUIStyle()
		{
			tolbarButtonLeftAlignGUIStyle = new GUIStyle(EditorStyles.toolbarButton);
			tolbarButtonLeftAlignGUIStyle.alignment=(TextAnchor)(3);
		}

		private static void InitToolbarLabelGUIStyle()
		{
			toolbarLabelGUIStyle = new GUIStyle(EditorStyles.label);
			toolbarLabelGUIStyle.normal.textColor=((!EditorGUIUtility.isProSkin) ? new Color(0f, 0.5f, 0f) : new Color(0f, 0.7f, 0f));
		}

		private static void InitTaskInspectorCommentGUIStyle()
		{
			taskInspectorCommentGUIStyle = new GUIStyle(GUI.skin.textArea);
			taskInspectorCommentGUIStyle.wordWrap=(true);
		}

		private static void InitTaskInspectorGUIStyle()
		{
			taskInspectorGUIStyle = new GUIStyle(GUI.skin.label);
			taskInspectorGUIStyle.alignment=(TextAnchor)(3);
			taskInspectorGUIStyle.fontSize=(11);
			taskInspectorGUIStyle.fontStyle=(0);
		}

		private static void InitToolbarButtonSelectionGUIStyle()
		{
			toolbarButtonSelectionGUIStyle = new GUIStyle(EditorStyles.toolbarButton);
			toolbarButtonSelectionGUIStyle.normal.background=(toolbarButtonSelectionGUIStyle.active.background);
		}

		private static void InitPreferencesPaneGUIStyle()
		{
			preferencesPaneGUIStyle = new GUIStyle(GUI.skin.box);
			preferencesPaneGUIStyle.normal.background=(EditorStyles.toolbarButton.normal.background);
		}

		private static void InitPropertyBoxGUIStyle()
		{
			propertyBoxGUIStyle = new GUIStyle();
			propertyBoxGUIStyle.padding=(new RectOffset(2, 2, 0, 0));
		}

		private static void InitPlainButtonGUIStyle()
		{
			plainButtonGUIStyle = new GUIStyle(GUI.skin.button);
			plainButtonGUIStyle.border=(new RectOffset(0, 0, 0, 0));
			plainButtonGUIStyle.margin=(new RectOffset(0, 0, 2, 2));
			plainButtonGUIStyle.padding=(new RectOffset(0, 0, 1, 0));
			plainButtonGUIStyle.normal.background=(null);
			plainButtonGUIStyle.active.background=(null);
			plainButtonGUIStyle.hover.background=(null);
			plainButtonGUIStyle.focused.background=(null);
			plainButtonGUIStyle.normal.textColor=(Color.white);
			plainButtonGUIStyle.active.textColor=(Color.white);
			plainButtonGUIStyle.hover.textColor=(Color.white);
			plainButtonGUIStyle.focused.textColor=(Color.white);
		}

		private static void InitTransparentButtonGUIStyle()
		{
            transparentButtonGUIStyle = new GUIStyle(GUI.skin.button)
            {
                border = (new RectOffset(0, 0, 0, 0)),
                margin = (new RectOffset(4, 4, 2, 2)),
                padding = (new RectOffset(2, 2, 1, 0))
            };
            transparentButtonGUIStyle.normal.background=(null);
			transparentButtonGUIStyle.active.background=(null);
			transparentButtonGUIStyle.hover.background=(null);
			transparentButtonGUIStyle.focused.background=(null);
			transparentButtonGUIStyle.normal.textColor=(Color.white);
			transparentButtonGUIStyle.active.textColor=(Color.white);
			transparentButtonGUIStyle.hover.textColor=(Color.white);
			transparentButtonGUIStyle.focused.textColor=(Color.white);
		}

		private static void InitTransparentButtonOffsetGUIStyle()
		{
            transparentButtonOffsetGUIStyle = new GUIStyle(GUI.skin.button)
            {
                border = (new RectOffset(0, 0, 0, 0)),
                margin = (new RectOffset(4, 4, 4, 2)),
                padding = (new RectOffset(2, 2, 1, 0))
            };
            transparentButtonOffsetGUIStyle.normal.background=(null);
			transparentButtonOffsetGUIStyle.active.background=(null);
			transparentButtonOffsetGUIStyle.hover.background=(null);
			transparentButtonOffsetGUIStyle.focused.background=(null);
			transparentButtonOffsetGUIStyle.normal.textColor=(Color.white);
			transparentButtonOffsetGUIStyle.active.textColor=(Color.white);
			transparentButtonOffsetGUIStyle.hover.textColor=(Color.white);
			transparentButtonOffsetGUIStyle.focused.textColor=(Color.white);
		}

		private static void InitButtonGUIStyle()
		{
            buttonGUIStyle = new GUIStyle(GUI.skin.button)
            {
                margin = (new RectOffset(0, 0, 2, 2)),
                padding = (new RectOffset(0, 0, 1, 1))
            };
        }

		private static void InitPlainTextureGUIStyle()
		{
            plainTextureGUIStyle = new GUIStyle
            {
                border = (new RectOffset(0, 0, 0, 0)),
                margin = (new RectOffset(0, 0, 0, 0)),
                padding = (new RectOffset(0, 0, 0, 0))
            };
            plainTextureGUIStyle.normal.background=(null);
			plainTextureGUIStyle.active.background=(null);
			plainTextureGUIStyle.hover.background=(null);
			plainTextureGUIStyle.focused.background=(null);
		}

		private static void InitArrowSeparatorGUIStyle()
		{
            arrowSeparatorGUIStyle = new GUIStyle
            {
                border = (new RectOffset(0, 0, 0, 0)),
                margin = (new RectOffset(0, 0, 3, 0)),
                padding = (new RectOffset(0, 0, 0, 0))
            };
            Texture2D background = LoadTexture("ArrowSeparator", true);
			arrowSeparatorGUIStyle.normal.background=(background);
			arrowSeparatorGUIStyle.active.background=(background);
			arrowSeparatorGUIStyle.hover.background=(background);
			arrowSeparatorGUIStyle.focused.background=(background);
		}

		private static void InitSelectedBackgroundGUIStyle()
		{
			Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false, true);
			Color color = (!EditorGUIUtility.isProSkin) ? new Color(0.243f, 0.5686f, 0.839f, 0.5f) : new Color(0.188f, 0.4588f, 0.6862f, 0.5f);
			texture2D.SetPixel(1, 1, color);
			texture2D.hideFlags=(HideFlags)(61);
			texture2D.Apply();
			selectedBackgroundGUIStyle = new GUIStyle();
			selectedBackgroundGUIStyle.border=(new RectOffset(0, 0, 0, 0));
			selectedBackgroundGUIStyle.margin=(new RectOffset(0, 0, -2, 2));
			selectedBackgroundGUIStyle.normal.background=(texture2D);
			selectedBackgroundGUIStyle.active.background=(texture2D);
			selectedBackgroundGUIStyle.hover.background=(texture2D);
			selectedBackgroundGUIStyle.focused.background=(texture2D);
		}

		private static void InitErrorListDarkBackground()
		{
			Texture2D texture2D = new Texture2D(1, 1, (TextureFormat)4, false, true);
			Color color = (!EditorGUIUtility.isProSkin) ? new Color(0.706f, 0.706f, 0.706f) : new Color(0.2f, 0.2f, 0.2f, 1f);
			texture2D.SetPixel(1, 1, color);
			texture2D.hideFlags=(HideFlags)(61);
			texture2D.Apply();
			errorListDarkBackground = new GUIStyle();
			errorListDarkBackground.padding=(new RectOffset(2, 0, 2, 0));
			errorListDarkBackground.normal.background=(texture2D);
			errorListDarkBackground.active.background=(texture2D);
			errorListDarkBackground.hover.background=(texture2D);
			errorListDarkBackground.focused.background=(texture2D);
			errorListDarkBackground.normal.textColor=((!EditorGUIUtility.isProSkin) ? new Color(0.206f, 0.206f, 0.206f) : new Color(0.706f, 0.706f, 0.706f));
			errorListDarkBackground.alignment=(0);
			errorListDarkBackground.wordWrap=(false);
		}

		private static void InitErrorListLightBackground()
		{
			errorListLightBackground = new GUIStyle();
			errorListLightBackground.padding=(new RectOffset(2, 0, 2, 0));
			errorListLightBackground.normal.textColor=((!EditorGUIUtility.isProSkin) ? new Color(0.106f, 0.106f, 0.106f) : new Color(0.706f, 0.706f, 0.706f));
			errorListLightBackground.alignment=(0);
			errorListLightBackground.wordWrap=(false);
		}

		private static void InitWelcomeScreenIntroGUIStyle()
		{
			welcomeScreenIntroGUIStyle = new GUIStyle(GUI.skin.label);
			welcomeScreenIntroGUIStyle.fontSize=(16);
			welcomeScreenIntroGUIStyle.fontStyle=(FontStyle)(1);
			welcomeScreenIntroGUIStyle.normal.textColor=(new Color(0.706f, 0.706f, 0.706f));
		}

		private static void InitWelcomeScreenTextHeaderGUIStyle()
		{
			welcomeScreenTextHeaderGUIStyle = new GUIStyle(GUI.skin.label);
			welcomeScreenTextHeaderGUIStyle.alignment=(TextAnchor)(3);
			welcomeScreenTextHeaderGUIStyle.fontSize=(14);
			welcomeScreenTextHeaderGUIStyle.fontStyle=(FontStyle)(1);
		}

		private static void InitWelcomeScreenTextDescriptionGUIStyle()
		{
			welcomeScreenTextDescriptionGUIStyle = new GUIStyle(GUI.skin.label);
			welcomeScreenTextDescriptionGUIStyle.wordWrap=(true);
		}

		private static void InitTaskBorderTexture(int colorIndex)
		{
			taskBorderTexture[colorIndex] = LoadTaskTexture("TaskBorder" + ColorIndexToColorString(colorIndex), true, null);
		}

		private static void InitTaskBorderRunningTexture()
		{
			taskBorderRunningTexture = LoadTaskTexture("TaskBorderRunning", true, null);
		}

		private static void InitTaskBorderIdentifyTexture()
		{
			taskBorderIdentifyTexture = LoadTaskTexture("TaskBorderIdentify", true, null);
		}

		private static void InitTaskConnectionTopTexture(int colorIndex)
		{
			taskConnectionTopTexture[colorIndex] = LoadTaskTexture("TaskConnectionTop" + ColorIndexToColorString(colorIndex), true, null);
		}

		private static void InitTaskConnectionBottomTexture(int colorIndex)
		{
			taskConnectionBottomTexture[colorIndex] = LoadTaskTexture("TaskConnectionBottom" + ColorIndexToColorString(colorIndex), true, null);
		}

		private static void InitTaskConnectionRunningTopTexture()
		{
			taskConnectionRunningTopTexture = LoadTaskTexture("TaskConnectionRunningTop", true, null);
		}

		private static void InitTaskConnectionRunningBottomTexture()
		{
			taskConnectionRunningBottomTexture = LoadTaskTexture("TaskConnectionRunningBottom", true, null);
		}

		private static void InitTaskConnectionIdentifyTopTexture()
		{
			taskConnectionIdentifyTopTexture = LoadTaskTexture("TaskConnectionIdentifyTop", true, null);
		}

		private static void InitTaskConnectionIdentifyBottomTexture()
		{
			taskConnectionIdentifyBottomTexture = LoadTaskTexture("TaskConnectionIdentifyBottom", true, null);
		}

		private static void InitTaskConnectionCollapsedTexture()
		{
			taskConnectionCollapsedTexture = LoadTexture("TaskConnectionCollapsed", true);
		}

		private static void InitContentSeparatorTexture()
		{
			contentSeparatorTexture = LoadTexture("ContentSeparator");
		}

		private static void InitDocTexture()
		{
			docTexture = LoadTexture("DocIcon", true);
		}

		private static void InitGearTexture()
		{
			gearTexture = LoadTexture("GearIcon", true);
		}

		private static void InitColorSelectorTexture(int colorIndex)
		{
			colorSelectorTexture[colorIndex] = LoadTexture("ColorSelector" + ColorIndexToColorString(colorIndex), true);
		}

		private static void InitVariableButtonTexture()
		{
			variableButtonTexture = LoadTexture("VariableButton", true);
		}

		private static void InitVariableButtonSelectedTexture()
		{
			variableButtonSelectedTexture = LoadTexture("VariableButtonSelected", true);
		}

		private static void InitVariableWatchButtonTexture()
		{
			variableWatchButtonTexture = LoadTexture("VariableWatchButton", true);
		}

		private static void InitVariableWatchButtonSelectedTexture()
		{
			variableWatchButtonSelectedTexture = LoadTexture("VariableWatchButtonSelected", true);
		}

		private static void InitReferencedTexture()
		{
			referencedTexture = LoadTexture("LinkedIcon", true);
		}

		private static void InitConditionalAbortSelfTexture()
		{
			conditionalAbortSelfTexture = LoadTexture("ConditionalAbortSelfIcon", true);
		}

		private static void InitConditionalAbortLowerPriorityTexture()
		{
			conditionalAbortLowerPriorityTexture = LoadTexture("ConditionalAbortLowerPriorityIcon", true);
		}

		private static void InitConditionalAbortBothTexture()
		{
			conditionalAbortBothTexture = LoadTexture("ConditionalAbortBothIcon", true);
		}

		private static void InitDeleteButtonTexture()
		{
			deleteButtonTexture = LoadTexture("DeleteButton", true);
		}

		private static void InitVariableDeleteButtonTexture()
		{
			variableDeleteButtonTexture = LoadTexture("VariableDeleteButton", true);
		}

		private static void InitDownArrowButtonTexture()
		{
			downArrowButtonTexture = LoadTexture("DownArrowButton", true);
		}

		private static void InitUpArrowButtonTexture()
		{
			upArrowButtonTexture = LoadTexture("UpArrowButton", true);
		}

		private static void InitVariableMapButtonTexture()
		{
			variableMapButtonTexture = LoadTexture("VariableMapButton", true);
		}

		private static void InitIdentifyButtonTexture()
		{
			identifyButtonTexture = LoadTexture("IdentifyButton", true);
		}

		private static void InitBreakpointTexture()
		{
			breakpointTexture = LoadTexture("BreakpointIcon", false);
		}

		private static void InitErrorIconTexture()
		{
			errorIconTexture = LoadTexture("ErrorIcon", true);
		}

		private static void InitSmallErrorIconTexture()
		{
			smallErrorIconTexture = LoadTexture("SmallErrorIcon", true);
		}

		private static void InitEnableTaskTexture()
		{
			enableTaskTexture = LoadTexture("TaskEnableIcon", false);
		}

		private static void InitDisableTaskTexture()
		{
			disableTaskTexture = LoadTexture("TaskDisableIcon", false);
		}

		private static void InitExpandTaskTexture()
		{
			expandTaskTexture = LoadTexture("TaskExpandIcon", false);
		}

		private static void InitCollapseTaskTexture()
		{
			collapseTaskTexture = LoadTexture("TaskCollapseIcon", false);
		}

		private static void InitExecutionSuccessTexture()
		{
			executionSuccessTexture = LoadTexture("ExecutionSuccess", false);
		}

		private static void InitExecutionFailureTexture()
		{
			executionFailureTexture = LoadTexture("ExecutionFailure", false);
		}

		private static void InitExecutionSuccessRepeatTexture()
		{
			executionSuccessRepeatTexture = LoadTexture("ExecutionSuccessRepeat", false);
		}

		private static void InitExecutionFailureRepeatTexture()
		{
			executionFailureRepeatTexture = LoadTexture("ExecutionFailureRepeat", false);
		}

		private static void InitHistoryBackwardTexture()
		{
			historyBackwardTexture = LoadTexture("HistoryBackward", true);
		}

		private static void InitHistoryForwardTexture()
		{
			historyForwardTexture = LoadTexture("HistoryForward", true);
		}

		private static void InitPlayTexture()
		{
			playTexture = LoadTexture("Play", true);
		}

        private static void InitPlayHeadTexture()
        {
            playHeadTexture = LoadTexture("Director_Playhead", false);
        }

		private static void InitPauseTexture()
		{
			pauseTexture = LoadTexture("Pause", true);
		}

		private static void InitStepTexture()
		{
			stepTexture = LoadTexture("Step", true);
		}

		private static void InitScreenshotBackgroundTexture()
		{
			screenshotBackgroundTexture = new Texture2D(1, 1, (TextureFormat)3, false, true);
			if (EditorGUIUtility.isProSkin)
			{
				screenshotBackgroundTexture.SetPixel(1, 1, new Color(0.1647f, 0.1647f, 0.1647f));
			}
			else
			{
				screenshotBackgroundTexture.SetPixel(1, 1, new Color(0.3647f, 0.3647f, 0.3647f));
			}
			screenshotBackgroundTexture.Apply();
		}
	}
}