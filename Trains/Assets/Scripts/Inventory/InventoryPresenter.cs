namespace Inventory
{
    public class InventoryPresenter : PresenterBehaviour<InventoryModel>
    {
        public InventoryElemenetPresenterFactory itemPresenterFactory;
        protected override void OnInjectModel()
        {
            base.OnInjectModel();
            itemPresenterFactory.Create(model.inventoryElements);
        }
    }
}