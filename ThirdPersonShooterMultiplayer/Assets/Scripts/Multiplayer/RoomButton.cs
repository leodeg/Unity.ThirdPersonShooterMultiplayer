using UnityEngine;
using System.Collections;

namespace Multiplayer
{
    public class RoomButton : MonoBehaviour
    {
        public bool isRoomCreated;
        public Room room;
        public string sceneName = "level1";

        public void OnClick ()
        {
            Managers.GameManagers.GetResourcesManager ().currentRoom.SetRoomButton (this);
        }
    }
}