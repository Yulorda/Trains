using System.Collections.Generic;

namespace Inventory
{
    public class InventoryModel
    {
        public List<IInventoryElement> inventoryElements = new List<IInventoryElement>();

        //��� ������������� interface ? ������ odin ? � ��� ����� �� ������ ������� ������ ?
        //�������� ��� ���������� + GetComponent ? 
        public InventoryModel(List<IInventoryElement> inventoryElements)
        {
            this.inventoryElements = inventoryElements;
        }
    }
}