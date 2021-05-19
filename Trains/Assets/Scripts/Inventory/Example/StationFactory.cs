using Inventory;
using UnityEngine;

namespace Invnetory
{
    namespace Example
    {
        public class StationFactory : PrefabComponentFactory<Component>, IInventoryElement
        {
            [SerializeField]
            ItemDescription itemDescription;

            ItemDescription IInventoryElement.ItemDescription => itemDescription;

            void IInventoryElement.StartAction()
            {
                Debug.Log("CreateAction");
            }
        }
    }
}