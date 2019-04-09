using System.Collections;
using UnityEngine;

namespace StateAction
{
    public abstract class Ballistics : ScriptableObject
    {
        public abstract void Execute (StateManager states, Weapon weapon);
    }
}