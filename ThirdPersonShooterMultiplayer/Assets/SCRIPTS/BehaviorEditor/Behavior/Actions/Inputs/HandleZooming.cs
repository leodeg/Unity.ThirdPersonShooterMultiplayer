using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateAction
{
	[CreateAssetMenu (menuName = "Inputs/Zoom")]
	public class HandleZooming : Action
	{
		public StateObject.TransformVariable viewCameraTransfrom;
		public InputButton aimInput;
		public float defaultZPosition = 6f;
		public float zoomedZPosition = 4f;
		public float zoomSpeed = 9f;

		private float currentZPosition;

		public override void Execute ()
		{
			if (viewCameraTransfrom == null)
			{
				Debug.LogError ("HandleZooming:: camera transform is null.");
				return;
			}

			float targetZPosition = defaultZPosition;

			if (aimInput.isPressed)
			{
				targetZPosition = zoomedZPosition;
			}

			currentZPosition = Mathf.Lerp (currentZPosition, targetZPosition, zoomSpeed * Time.deltaTime);
			viewCameraTransfrom.transform.localPosition = Vector3.forward * currentZPosition;
		}
	}
}
