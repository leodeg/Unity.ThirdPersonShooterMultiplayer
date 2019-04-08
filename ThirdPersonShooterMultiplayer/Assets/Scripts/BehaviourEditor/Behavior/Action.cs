using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateAction
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Execute();
    }
}
