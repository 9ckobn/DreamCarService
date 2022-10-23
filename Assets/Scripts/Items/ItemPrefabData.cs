using System.Collections.Generic;
using UnityEngine;
    
    [CreateAssetMenu(fileName = "Item data", menuName = "Dream Car Service/ItemPrefabData")]
    public class ItemPrefabData : ScriptableObject
    {
        public List<GameObject> prefab;
    }
