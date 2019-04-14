using System.Collections;
using UnityEngine;
using UnityEngine.Events;


namespace StateObject
{
    public class OnEnableExecuteEvent : MonoBehaviour
    {
        public UnityEvent onEnable;

        private void OnEnable ()
        {
            onEnable.Invoke();
        }
    }
}