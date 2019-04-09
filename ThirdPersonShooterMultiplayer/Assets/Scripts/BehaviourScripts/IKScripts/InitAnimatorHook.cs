using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu(menuName = "Actions/Initialize/Animator Hook")]
    public class InitAnimatorHook : StateActions
    {
        public override void Execute(StateManager states)
        {
            GameObject model = states.animatorInstance.gameObject;
            states.animatorController = model.AddComponent<AnimatorController> ();
            states.animatorController.Initialize (states);
        }
    }
}