using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Managers/Resources Manager")]
    public class ResourcesManager : ScriptableObject
    {
        public List<Item> itemList = new List<Item> ();
        public RoomVariable currentRoom;

        private Dictionary<string, Item> itemDictionary = new Dictionary<string, Item> ();

        public void Initialize ()
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (!itemDictionary.ContainsKey(itemList[i].name))
                {
                    itemDictionary.Add (itemList [i].name, itemList [i]);
                }
                else
                {
                    Debug.Log ("There's at least two items with the same name: [" + itemList[i].name + "]");
                }
            }
        }

        public Item GetItemInstance (string name)
        {
            Item defaultItem = GetItem (name);
            Item newItem = Instantiate (defaultItem);
            newItem.name = defaultItem.name;
            return newItem;
        }

        public Item GetItem (string name)
        {
            Item item = null;
            itemDictionary.TryGetValue (name, out item);
            return item;
        }
    }
}