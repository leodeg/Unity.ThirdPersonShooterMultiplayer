using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using StateAction;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace StateAction.BehaviorEditor
{
	[CreateAssetMenu (menuName = "Editor/Nodes/State Node")]
	public class StateNode : DrawNode
	{
		public override void DrawWindow (BaseNode baseNode)
		{
			if (baseNode.stateReference.currentState == null)
			{
				EditorGUILayout.LabelField ("Add state to modify:");
			}
			else
			{
				if (!baseNode.collapse)
				{

				}
				else
				{
					baseNode.windowRect.height = 100;
				}

				baseNode.collapse = EditorGUILayout.Toggle (" ", baseNode.collapse);
			}

			baseNode.stateReference.currentState = (State)EditorGUILayout.ObjectField (baseNode.stateReference.currentState, typeof(State), false);

			if (baseNode.previousCollapse != baseNode.collapse)
			{
				baseNode.previousCollapse = baseNode.collapse;
			}

			if (baseNode.stateReference.previousState != baseNode.stateReference.currentState)
			{
				//b.serializedState = null;
				baseNode.isDuplicate = BehaviorEditor.settings.currentGraph.IsStateDuplicate (baseNode);
				baseNode.stateReference.previousState = baseNode.stateReference.currentState;

				if (!baseNode.isDuplicate)
				{
					Vector3 pos = new Vector3 (baseNode.windowRect.x, baseNode.windowRect.y, 0);
					pos.x += baseNode.windowRect.width * 2;

					SetupReordableLists (baseNode);

					//Load transitions
					for (int i = 0; i < baseNode.stateReference.currentState.transitions.Count; i++)
					{
						pos.y += i * 100;
						BehaviorEditor.AddTransitionNodeFromTransition (baseNode.stateReference.currentState.transitions[i], baseNode, pos);
					}
					BehaviorEditor.forceSetDirty = true;
				}
			}

			if (baseNode.isDuplicate)
			{
				EditorGUILayout.LabelField ("State is a duplicate!");
				baseNode.windowRect.height = 100;
				return;
			}

			if (baseNode.stateReference.currentState != null)
			{
				baseNode.isAssigned = true;

				if (!baseNode.collapse)
				{
					if (baseNode.stateReference.serializedState == null)
					{
						SetupReordableLists (baseNode);

						//	SerializedObject serializedState = new SerializedObject(b.stateRef.currentState);
					}

					float standard = 150;
					baseNode.stateReference.serializedState.Update ();
					baseNode.showActions = EditorGUILayout.Toggle ("Show Actions ", baseNode.showActions);
					if (baseNode.showActions)
					{
						EditorGUILayout.LabelField ("");
						baseNode.stateReference.onFixedList.DoLayoutList ();
						EditorGUILayout.LabelField ("");
						baseNode.stateReference.onUpdateList.DoLayoutList ();
						standard += 100 + 40 + ( baseNode.stateReference.onUpdateList.count + baseNode.stateReference.onFixedList.count ) * 20;
					}
					baseNode.showEnterExit = EditorGUILayout.Toggle ("Show Enter/Exit ", baseNode.showEnterExit);
					if (baseNode.showEnterExit)
					{
						EditorGUILayout.LabelField ("");
						baseNode.stateReference.onEnterList.DoLayoutList ();
						EditorGUILayout.LabelField ("");
						baseNode.stateReference.onExitList.DoLayoutList ();
						standard += 100 + 40 + ( baseNode.stateReference.onEnterList.count + baseNode.stateReference.onExitList.count ) * 20;
					}

					baseNode.stateReference.serializedState.ApplyModifiedProperties ();
					baseNode.windowRect.height = standard;
				}
			}
			else
			{
				baseNode.isAssigned = false;
			}
		}

		private void SetupReordableLists (BaseNode b)
		{

			b.stateReference.serializedState = new SerializedObject (b.stateReference.currentState);
			b.stateReference.onFixedList = new ReorderableList (b.stateReference.serializedState, b.stateReference.serializedState.FindProperty ("onFixed"), true, true, true, true);
			b.stateReference.onUpdateList = new ReorderableList (b.stateReference.serializedState, b.stateReference.serializedState.FindProperty ("onUpdate"), true, true, true, true);
			b.stateReference.onEnterList = new ReorderableList (b.stateReference.serializedState, b.stateReference.serializedState.FindProperty ("onEnter"), true, true, true, true);
			b.stateReference.onExitList = new ReorderableList (b.stateReference.serializedState, b.stateReference.serializedState.FindProperty ("onExit"), true, true, true, true);

			HandleReordableList (b.stateReference.onFixedList, "On Fixed");
			HandleReordableList (b.stateReference.onUpdateList, "On Update");
			HandleReordableList (b.stateReference.onEnterList, "On Enter");
			HandleReordableList (b.stateReference.onExitList, "On Exit");
		}

		private void HandleReordableList (ReorderableList list, string targetName)
		{
			list.drawHeaderCallback = (Rect rect) =>
			{
				EditorGUI.LabelField (rect, targetName);
			};

			list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
			 {
				 var element = list.serializedProperty.GetArrayElementAtIndex (index);
				 EditorGUI.ObjectField (new Rect (rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
			 };
		}

		public override void DrawCurve (BaseNode b)
		{

		}

		public Transition AddTransition (BaseNode b)
		{
			return b.stateReference.currentState.AddTransition ();
		}

		public void ClearReferences ()
		{
			//      BehaviorEditor.ClearWindowsFromList(dependencies);
			//    dependencies.Clear();
		}

	}
}
