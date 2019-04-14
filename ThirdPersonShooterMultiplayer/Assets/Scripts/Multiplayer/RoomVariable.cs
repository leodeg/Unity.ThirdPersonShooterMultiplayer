using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu(menuName = "Multiplayer/Room Variable")]
    public class RoomVariable : ScriptableObject
    {
        public Room value;

        public void SetRoom (Room room)
        {
            value = room;
        }
    }
}