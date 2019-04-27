using UnityEngine;
using System.Collections;

namespace Managers
{

    [CreateAssetMenu (menuName = "Managers/PlayerProfile")]
    public class PlayerProfile : ScriptableObject
    {
        public string[] itemIds;
    }
}