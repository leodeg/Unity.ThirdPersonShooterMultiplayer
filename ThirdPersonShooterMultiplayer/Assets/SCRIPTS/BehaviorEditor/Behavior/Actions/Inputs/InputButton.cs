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
        public bool updatePressedState = true;
        public StateObject.BoolVariable currentPressedState;

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

            if (updatePressedState)
            {
                if (currentPressedState != null)
                {
                    currentPressedState.value = isPressed;
                }
            }
        }
    }
}
