using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class InventoryElement : MonoBehaviour
    {
        [SerializeField]
        private ItemDescription itemDescription;

        public ItemDescription ItemDescription => itemDescription;

        public UnityEvent OnElementClick;

        public void Click()
        {
            OnElementClick?.Invoke();
        }
    }
}