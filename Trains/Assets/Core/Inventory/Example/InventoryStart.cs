using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invnetory
{
    namespace Example
    {
        public class InventoryStart : MonoBehaviour
        {
            [SerializeField]
            InventoryPresenter inventroyPresenter;

            [SerializeField]
            List<InventoryElement> inventoryElements = new List<InventoryElement>();

            private void Awake()
            {
                var model = new InventoryModel(inventoryElements);
                inventroyPresenter.InjectModel(model);
            }
        }
    }
}