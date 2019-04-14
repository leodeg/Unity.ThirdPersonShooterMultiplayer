using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Multiplayer/Room Variable")]
    public class RoomVariable : ScriptableObject
    {
        public Room value;
        public RoomButtonVariable roomButtonVariable;

        public void JoinGame ()
        {
            if (roomButtonVariable.value == null)
                return;

            SetRoom (roomButtonVariable.value);
        }

        public void SetRoom (Room room)
        {
            value = room;
        }

        public void SetRoom (RoomButton button)
        {
            if (button.isRoomCreated)
            {
                SetRoom (button.room);
            }
            else
            {
                MultiplayerLauncher.singleton.CreateRoom (button);
            }
        }

        public void SetRoomButton (RoomButton button)
        {
            roomButtonVariable.value = button;
        }
    }
}