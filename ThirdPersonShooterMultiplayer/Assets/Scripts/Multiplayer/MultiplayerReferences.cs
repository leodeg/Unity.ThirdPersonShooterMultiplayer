using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Multiplayer
{
    public class MultiplayerReferences
    {
        public PlayerHolder localPlayer;
        public Transform referencesTransform;

        private List<PlayerHolder> players = new List<PlayerHolder> ();

        public MultiplayerReferences ()
        {
            referencesTransform = new GameObject ("References").transform;
        }

        public int GetPlayersCount ()
        {
            return players.Count;
        }

        public List<PlayerHolder> GetPlayers ()
        {
            return players;
        }

        public PlayerHolder AddNewPlayer (NetworkPrint networkPrint)
        {
            if (!IsUniquePlayer (networkPrint.photonId))
            {
                return null;
            }

            PlayerHolder playerHolder = new PlayerHolder ()
            {
                photonID = networkPrint.photonId,
                networkPrint = networkPrint
            };

            players.Add (playerHolder);
            return playerHolder;
        }

        public PlayerHolder GetPlayer (int photonId)
        {
            foreach (PlayerHolder player in players)
                if (player.photonID == photonId)
                    return player;

            return null;
        }

        public bool IsUniquePlayer (int id)
        {
            foreach (PlayerHolder holder in players)
                if (holder.photonID == id)
                    return false;
            return true;
        }
    }
}