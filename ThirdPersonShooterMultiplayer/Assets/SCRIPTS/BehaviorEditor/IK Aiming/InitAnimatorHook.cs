using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu(menuName = "Actions/Init Actions/Init Animator Hook")]
    public class InitAnimatorHook : StateActions
    {
        public override void Execute(StateManager states)
        {
            GameObject model = states.animatorInstance.gameObject;
            states.animatorHook = model.AddComponent<AnimatorHook> ();
            states.animatorHook.Initialize (states);
        }
    }
}