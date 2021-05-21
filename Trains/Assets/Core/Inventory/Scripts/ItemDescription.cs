using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu]
    public class ItemDescription : ScriptableObject
    {
        public Sprite icon;
        public string itemName;
        public string description;
    }
}