using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Managers
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int budget = 15;
        [HideInInspector] public int currentIndex = 0;
        [HideInInspector] public List<GameObject> createdObjects = new List<GameObject> ();
    }

    [CreateAssetMenu (menuName = "Managers/Object Pool")]
    public class ObjectPooler : ScriptableObject
    {
        public List<Pool> pool = new List<Pool> ();
        private Dictionary<string, int> objectDictionary = new Dictionary<string, int> ();
        GameObject poolParent;

        public void Initialize  ()
        {
            if (poolParent)
            {
                Destroy (poolParent);
            }

            poolParent = new GameObject ();
            poolParent.name = "Object Pool";
            objectDictionary.Clear ();

            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].budget < 1)
                {
                    pool[i].budget = 1;
                }

                pool[i].currentIndex = 0;
                pool[i].createdObjects.Clear ();

                if (objectDictionary.ContainsKey(pool[i].prefab.name))
                {
                    Debug.Log ("Entry with id " + pool[i].prefab.name + " is a duplicate");
                    continue;
                }
                else
                {
                    objectDictionary.Add (pool[i].prefab.name, i);
                }
            }
        }

        public GameObject GetObject (string id)
        {
            GameObject obj = null;
            int index = 0;

            if (objectDictionary.TryGetValue (id, out index))
            {
                Pool currentPool = pool[index];

                if (currentPool.createdObjects.Count - 1 < currentPool.budget)
                {
                    obj = Instantiate (currentPool.prefab);
                    obj.transform.parent = poolParent.transform;
                    currentPool.createdObjects.Add (obj);
                }
                else
                {
                    currentPool.currentIndex = (currentPool.currentIndex < currentPool.createdObjects.Count) ? currentPool.currentIndex + 1 : 0;
                    obj = currentPool.createdObjects[currentPool.currentIndex];
                    obj.SetActive (false);
                    obj.SetActive (true);
                }
            }

            return obj;
        }
    }
}