using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/HandleCursor")]
    public class HandleCursor : Action
    {
        public CursorLockMode lockMode;
        public bool isVisible;

        public override void Execute ()
        {
            Cursor.lockState = lockMode;
            Cursor.visible = isVisible;
        }
    }
}