using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace StateAction.BehaviorEditor
{
	[CreateAssetMenu(menuName = "Editor/Nodes/Portal Node")]
	public class PortalNode : DrawNode
	{

		public override void DrawCurve(BaseNode b)
		{

		}

		public override void DrawWindow(BaseNode b)
		{
			b.stateReference.currentState = (State)EditorGUILayout.ObjectField(b.stateReference.currentState, typeof(State), false);
			b.isAssigned = b.stateReference.currentState != null;

			if (b.stateReference.previousState != b.stateReference.currentState)
			{
				b.stateReference.previousState = b.stateReference.currentState;
				BehaviorEditor.forceSetDirty = true;
			}
		}
	}
}
