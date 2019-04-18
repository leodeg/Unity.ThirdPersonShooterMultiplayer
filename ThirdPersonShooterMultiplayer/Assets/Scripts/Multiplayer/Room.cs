using UnityEngine;
using System.Collections;

namespace Multiplayer
{
    [CreateAssetMenu (menuName = "Multiplayer/Room")]
    public class Room : ScriptableObject
    {
        public string sceneName;
        public string roomName;
    }
}