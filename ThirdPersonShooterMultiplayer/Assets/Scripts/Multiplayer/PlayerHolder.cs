using UnityEngine;
using System.Collections;

namespace Multiplayer
{
    public class PlayerHolder
    {
        public int photonID;
        public string userName;
        public float spawnTimer;
        public int spawnPosition;
        public NetworkPrint networkPrint;
        public StateAction.StateManager states;
    }
}