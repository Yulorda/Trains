namespace Inventory
{
    public interface IInventoryElement
    {
        ItemDescription ItemDescription { get; }
        void StartAction();
    }
}