using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Multiplayer/Room")]
    public class Room : ScriptableObject
    {
        public string sceneName;
        public string roomName;
    }
}