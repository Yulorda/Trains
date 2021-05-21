using System.Collections.Generic;
using UniRx;

namespace Inventory
{
    public class InventoryModel
    {
        public ReactiveCollection<InventoryElement> inventoryElements = new ReactiveCollection<InventoryElement>();

        public InventoryModel(List<InventoryElement> inventoryElements)
        {
            this.inventoryElements = inventoryElements.ToReactiveCollection();
        }

        public void Add(InventoryElement inventoryElement)
        {
            inventoryElements.Add(inventoryElement);
        }

        public void Remove(InventoryElement inventoryElement)
        {
            inventoryElements.Remove(inventoryElement);
        }
    }
}