using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Multiplayer
{
    public class MatchMakingManager : MonoBehaviour
    {
        public Transform spawnParent;
        public Transform matchesParent;
        public GameObject matchPrefab;

        [HideInInspector]
        public static MatchMakingManager Singleton;
        private List<MatchSpawnPosition> spawnPositions;
        private Dictionary<string, RoomButton> roomsDictionary = new Dictionary<string, RoomButton> ();


        private void Awake ()
        {
            Singleton = this;
        }

        private void Start ()
        {
            Transform[] positions = spawnParent.GetComponentsInChildren<Transform> ();
            foreach (Transform item in positions)
            {
                if (item != spawnParent)
                {
                    MatchSpawnPosition spawnPosition = new MatchSpawnPosition ();
                    spawnPosition.transform = item;
                    spawnPositions.Add (spawnPosition);
                }
            }
        }

        private RoomButton GetRoomFromDictionary (string key)
        {
            RoomButton result = null;
            roomsDictionary.TryGetValue (key, out result);
            return result;
        }

        public void AddMatches (RoomInfo[] rooms)
        {
            SetDirtyRooms ();
            foreach (RoomInfo roomInfo in rooms)
            {
                RoomButton createdRoom = GetRoomFromDictionary (roomInfo.Name);
                if (createdRoom == null) AddMatch (roomInfo);
                else createdRoom.isValid = true;
            }

            ClearNonValidRooms ();
        }

        private void ClearNonValidRooms ()
        {
            foreach (RoomButton room in roomsDictionary.Values.ToList ())
            {
                if (!room.isValid)
                {
                    roomsDictionary.Remove (room.roomInfo.Name);
                    Destroy (room.gameObject);
                }
            }
        }

        private void SetDirtyRooms ()
        {
            foreach (RoomButton room in roomsDictionary.Values.ToList ())
                room.isValid = false;
        }

        public void AddMatch (RoomInfo roomInfo)
        {
            GameObject matchPrefabClone = Instantiate (matchPrefab);
            matchPrefabClone.transform.SetParent (matchesParent);

            MatchSpawnPosition randomSpawnPosition = GetSpawnPosition ();

            randomSpawnPosition.isUsed = true;
            matchPrefabClone.transform.position = randomSpawnPosition.transform.position;
            matchPrefabClone.transform.localScale = Vector3.one;

            RoomButton roomButton = matchPrefabClone.GetComponent<RoomButton> ();
            roomButton.roomInfo = roomInfo;
            roomButton.isRoomCreated = true;
            roomButton.isValid = true;

            roomsDictionary.Add (roomInfo.Name, roomButton);
        }

        public MatchSpawnPosition GetSpawnPosition ()
        {
            List<MatchSpawnPosition> unusedPositions = GetUnusedPositions ();
            int randomPosition = Random.Range (0, unusedPositions.Count);
            return unusedPositions[randomPosition];
        }

        public List<MatchSpawnPosition> GetUnusedPositions ()
        {
            List<MatchSpawnPosition> unusedPositions = new List<MatchSpawnPosition> ();
            foreach (MatchSpawnPosition position in spawnPositions)
                if (!position.isUsed)
                    unusedPositions.Add (position);
            return unusedPositions;
        }
    }

    public class MatchSpawnPosition
    {
        public Transform transform;
        public bool isUsed;
    }
}