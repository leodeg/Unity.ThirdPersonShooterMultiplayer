using UnityEngine;
using System.Collections;

namespace StateAction
{
	[CreateAssetMenu(menuName = "Inputs/Input Updater")]
	public class InputUpdater : Action
	{
		public Action[] inputs;

		public override void Execute ()
		{
			if (inputs == null) return;

			for (int i = 0; i < inputs.Length; i++)
			{
				inputs[i].Execute ();
			}
		}
	}
}