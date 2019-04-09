using StateObject;
using UnityEngine;

namespace StateAction
{
    public class AssignAnimator : MonoBehaviour
    {
        public GameObjectVariable variable;

        private void OnEnable ()
        {
            variable.value = this.gameObject;
            Destroy (this);
        }

    }
}