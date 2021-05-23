using Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Invnetory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        private InventoryPresenter inventroyPresenter;

        [SerializeField]
        private List<InventoryElement> inventoryElements = new List<InventoryElement>();

        private void Awake()
        {
            var model = new InventoryModel(inventoryElements);
            inventroyPresenter.InjectModel(model);
        }
    }
}