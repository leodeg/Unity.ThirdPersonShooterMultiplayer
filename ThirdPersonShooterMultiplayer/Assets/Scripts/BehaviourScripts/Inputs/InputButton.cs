using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Inputs/Button")]
    public class InputButton : Action
    {
        public enum KeyState
        {
            onDown, onCurrent, onUp
        }

        [Header ("Base")]
        public string targetInput;
        public bool isPressed;
        public KeyState keyState;

        [Header ("Pressed State")]
        public bool updateBoolVariable = true;
        public StateObject.BoolVariable boolVariable;

        public override void Execute ()
        {
            switch (keyState)
            {
                case KeyState.onDown:
                    isPressed = Input.GetButtonDown (targetInput);
                    break;
                case KeyState.onCurrent:
                    isPressed = Input.GetButton (targetInput);
                    break;
                case KeyState.onUp:
                    isPressed = Input.GetButtonUp (targetInput);
                    break;
                default:
                    break;
            }

            if (updateBoolVariable)
            {
                if (boolVariable != null)
                {
                    boolVariable.value = isPressed;
                }
            }
        }
    }
}
