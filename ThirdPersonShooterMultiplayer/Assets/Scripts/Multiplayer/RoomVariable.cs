using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Multiplayer
{
    [CreateAssetMenu (menuName = "Multiplayer/Room Variable")]
    public class RoomVariable : ScriptableObject
    {
        public Room value;
        public RoomButtonVariable roomButtonVariable;

        public void JoinGame ()
        {
            if (roomButtonVariable.value == null)
            {
                Debug.LogError ("RoomVariable::ERROR::Room button variable is null!");
                return;
            }

            SetRoom (roomButtonVariable.value);
        }

        public void SetRoom (Room room)
        {
            value = room;
        }

        public void SetRoom (RoomButton roomButton)
        {
            if (roomButton.isRoomCreated)
            {
                SetRoom (roomButton.room);
                Managers.MultiplayerLauncher.Singleton.JoinRoom (roomButton.roomInfo);
            }
            else
            {
                Managers.MultiplayerLauncher.Singleton.CreateRoom (roomButton);
            }
        }

        public void SetRoomButton (RoomButton button)
        {
            roomButtonVariable.value = button;
        }
    }
}