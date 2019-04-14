using UnityEngine;
using System.Collections;

namespace StateAction
{
    public class RoomButton : MonoBehaviour
    {
        public bool isRoomCreated;
        public Room room;
        public string sceneName = "level1";

        public void OnClick ()
        {
            GameManagers.GetResourcesManager ().currentRoom.SetRoomButton (this);
        }
    }
}