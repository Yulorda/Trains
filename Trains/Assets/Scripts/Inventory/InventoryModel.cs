using System.Collections.Generic;

namespace Inventory
{
    public class InventoryModel
    {
        public List<IInventoryElement> inventoryElements = new List<IInventoryElement>();

        //Как сериализовать interface ? купить odin ? А без одина он сможет открыть проект ?
        //Оберунть как геймобжект + GetComponent ? 
        public InventoryModel(List<IInventoryElement> inventoryElements)
        {
            this.inventoryElements = inventoryElements;
        }
    }
}